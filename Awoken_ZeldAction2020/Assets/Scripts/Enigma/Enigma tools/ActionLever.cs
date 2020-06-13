using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// This script takes the pressure plate behavior szcript and applies the on triggerStay of it
/// </summary>
public class ActionLever : PressurePlateBehavior
{
    protected bool playerHereAttack;
    protected bool playerHereCollision;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "AttackZone" && other.transform.root.tag == "Player")
        {
            playerHereAttack = true;
        }
        if(other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            playerHereCollision = true;
        }
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "AttackZone" && other.transform.root.tag == "Player")
        {
            playerHereAttack = false;
        }

        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            playerHereCollision = true;
        }
    }
    protected override void OnTriggerStay2D(Collider2D other)
    {
        //base.OnTriggerStay2D(other);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Attack") && playerHereAttack)
        {
            isPressed = true;
        }

        if(Input.GetButtonDown("Interraction") && playerHereCollision)
        {
            isPressed = true;
        }
    }
}
