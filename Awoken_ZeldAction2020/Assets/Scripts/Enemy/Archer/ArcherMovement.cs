using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve movement of archer and facing direction 
/// </summary>

public class ArcherMovement : MonoBehaviour
{
    [SerializeField] [Min(0)]
    private float speed;
    public float retreatDistance;
    public float chaseDistance;
    public float attackDistance;

    private GameObject player;

    private Rigidbody2D rb;

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
        float distance = Vector2.Distance(transform.position, player.transform.position);           //Calculate distance between archer && player
        Debug.Log(distance);

        Vector2 direction = (player.transform.position - transform.position).normalized;            //Calculate direction between archer && player

        if (distance < chaseDistance && distance > attackDistance)                                  //Move to player
        {
            rb.velocity = direction * speed * Time.fixedDeltaTime;
        }
        else if (distance < retreatDistance)                                                        //Escape from player
        {
            rb.velocity = direction * -speed * Time.fixedDeltaTime;
        }
        else if (distance < attackDistance && distance > retreatDistance)                           //Stop at his position
        {
            rb.velocity = Vector2.zero;
        }
    }
}
