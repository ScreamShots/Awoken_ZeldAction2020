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

    [HideInInspector]
    public bool poulionIsAttacking;

    [HideInInspector]
    public bool poulionCanAttack;

    private bool attackInProgress;

    private bool chargeOn;

    private GameObject player;

    private Rigidbody2D rb;
    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        rb = gameObject.GetComponent<Rigidbody2D>();
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

    void Attack()
    {
        chargeOn = true;

        Vector2 direction = (player.transform.position - transform.position).normalized;                                //Get the position of Player at the end of charge animation
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
        if (other.gameObject.CompareTag("Player") && chargeOn)
        {
            Stun();
            player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
        }
    }

    //Insert OnCollisionEnter2D with a wall = Stunt without dmg to Player

    IEnumerator PrepareToAttack()
    {
        //Vector2 direction = (player.transform.position - transform.position).normalized;                              //Get the position of Player before the charge animation
        rb.velocity = Vector2.zero;
        GetComponent<BasicHealthSystem>().canTakeDmg = false;

        yield return new WaitForSeconds(timeBeforeCharge);
        Attack();

        yield return new WaitForSeconds(timeCharge);
        Stun();
    }

    IEnumerator DeStun()
    {
        yield return new WaitForSeconds(timeStun);
        poulionIsAttacking = false;
        attackInProgress = false;
    }
}
