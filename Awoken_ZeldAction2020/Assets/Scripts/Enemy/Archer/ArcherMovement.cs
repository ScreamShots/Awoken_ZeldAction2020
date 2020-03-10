using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve movement of archer and facing direction 
/// </summary>

public class ArcherMovement : MonoBehaviour
{
    #region Variables
    [Header("Archer Speed")]

    [SerializeField] [Min(0)]
    private float chaseSpeed = 0;

    [SerializeField] [Min(0)]
    private float retreatSpeed = 0;

    [Header("Archer Movement")]
    public float retreatDistance;
    public float chaseDistance;
    public float attackDistance;

    private GameObject player;

    private Rigidbody2D rb;

    [HideInInspector]
    public bool archerCanAttack;
    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {                                                
        int distance = (int) Vector2.Distance(transform.position, player.transform.position);                                           //Calculate distance between archer && player

        Vector2 direction = (player.transform.position - transform.position).normalized;                                                //Calculate direction between archer && player


        if (distance < chaseDistance && distance > attackDistance && !GetComponent<ArcherAttack>().archerIsAttacking)                   //Move to player if archer isn't attack
        {
            rb.velocity = direction * chaseSpeed * Time.fixedDeltaTime;
            GetComponent<ArcherAttack>().archerCanAttack = false;
        }
        else if (distance < retreatDistance && !gameObject.GetComponent<ArcherAttack>().archerIsAttacking)                              //Escape from player if archer isn't attack
        {
            rb.velocity = direction * -retreatSpeed * Time.fixedDeltaTime;
            GetComponent<ArcherAttack>().archerCanAttack = false;
        }
        else if (distance < chaseDistance && distance > retreatDistance)                                                                //Stop at his position
        {
            rb.velocity = Vector2.zero;
            GetComponent<ArcherAttack>().archerCanAttack = true;
        }
    }
}
