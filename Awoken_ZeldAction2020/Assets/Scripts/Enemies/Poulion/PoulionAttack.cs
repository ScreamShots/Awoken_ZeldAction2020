using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulionAttack : MonoBehaviour
{
    #region Variables
    [Header("Attack Settings")]
    public float timeBeforeCharge;

    public float timeStun;

    public float timeCharge;

    [Space]

    [SerializeField]
    [Min(0)]
    private float attackSpeed = 0;

    [Min(0)]
    [SerializeField] private float dmg = 0;

    [SerializeField] private float staminaLoseOnBlock = 0;

    private GameObject player;

    private Rigidbody2D rb;

    private Vector2 direction;

    [HideInInspector]
    public bool poulionIsAttacking;

    [HideInInspector]
    public bool poulionCanAttack;

    private bool attackInProgress;

    [HideInInspector]
    public bool chargeOn;

    [HideInInspector]
    public bool isStun;

    private BlockHandler blockHandle;

    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;
    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        rb = gameObject.GetComponent<Rigidbody2D>();

        blockHandle = GetComponent<BlockHandler>();
    }

    private void FixedUpdate()
    {
        if (poulionCanAttack && !attackInProgress)
        {
            attackInProgress = true;
            poulionIsAttacking = true;
            StartCoroutine(PrepareToAttack());
        }
    }

    private void Update()
    {
        if (blockHandle.isBlocked)          //Testin if the Poulion has been blocked
        {
            OnBlocked();                    //Apply behaviour design for the Poulion when it's blocked
        }
    }

    void OnBlocked()                        //What happen when the Poulion is blocked
    {
        PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLoseOnBlock);        //cause player lost stamina
        Stun();
    }

    void SetDirectionAttack()
    {
        direction = (player.transform.position - transform.position).normalized;

        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    watchDirection = Direction.right;
                }
                else
                {
                    watchDirection = Direction.left;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    watchDirection = Direction.up;
                }
                else
                {
                    watchDirection = Direction.down;
                }
            }
        }
    }

    void Attack()
    {
        chargeOn = true;

        direction = (player.transform.position - transform.position).normalized;                                //Get the position of Player at the end of charge animation
        rb.velocity = direction * attackSpeed * Time.fixedDeltaTime;
    }

    void Stun()
    {
        chargeOn = false;
        GetComponent<BasicHealthSystem>().canTakeDmg = true;

        rb.velocity = Vector2.zero;
        StartCoroutine(DeStun());
    }

    private void OnCollisionEnter2D(Collision2D other)                                                                  //When collide with player = Stun Poulion
    {
        if (other.gameObject.CompareTag("Player") && chargeOn && !blockHandle.isBlocked)
        {
            Stun();
            player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
        }
    }

    //Insert OnCollisionEnter2D with a wall = Stunt without dmg to Player

    IEnumerator PrepareToAttack()
    {
        //direction = (player.transform.position - transform.position).normalized;                              //Get the position of Player before the charge animation
        
        SetDirectionAttack();
        rb.velocity = Vector2.zero;
        GetComponent<BasicHealthSystem>().canTakeDmg = false;

        yield return new WaitForSeconds(timeBeforeCharge);
        Attack();

        yield return new WaitForSeconds(timeCharge);
        Stun();
    }

    IEnumerator DeStun()
    {
        isStun = true;
        yield return new WaitForSeconds(timeStun);
        poulionIsAttacking = false;
        attackInProgress = false;
        isStun = false;
    }
}
