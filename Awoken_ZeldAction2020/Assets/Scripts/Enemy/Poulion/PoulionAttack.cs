using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulionAttack : MonoBehaviour
{
    #region Variables
    private GameObject player;

    private Rigidbody2D rb;

    [Header("Attack Settings")]
    public float timeBeforeAttack;

    [SerializeField]
    [Min(0)]
    private float attackSpeed = 0;

    public float maxChargeDistance;

    [HideInInspector]
    public bool poulionIsAttacking;

    [HideInInspector]
    public bool poulionCanAttack;

    private bool attackInProgress = false;

    private Vector2 startPosition;
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

        float distanceOfCharge = Vector2.Distance(startPosition, transform.position);

        if(distanceOfCharge >= maxChargeDistance)       
        {
            Stun();
        }
    }

    void Attack()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        rb.velocity = direction * attackSpeed * Time.fixedDeltaTime;

        startPosition = transform.position;                                                                             //Distance of Charge start 
    }

    void Stun()                                                                                                         //For the stun state
    {
        rb.velocity = Vector2.zero;
        //poulionIsAttacking = false;                                           
        //attackInProgress = false;
        //Poulion can't take dmg
        //Stop the stun on wall / player / distance max
    }

    IEnumerator PrepareToAttack()
    {
        //Vector2 direction = (player.transform.position - transform.position).normalized;                              //If we want more let the Player to escape
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(timeBeforeAttack);
        Attack();
    }
}
