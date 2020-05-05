using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// This script takes the pressure plate behavior szcript and applies the on triggerStay of it
/// </summary>
public class ActionLever : PressurePlateBehavior
{
    protected bool playerHere;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            playerHere = true;
        }
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            playerHere = false;
        }
    }
    protected override void OnTriggerStay2D(Collider2D other)
    {
        //base.OnTriggerStay2D(other);
    }

    private void Update()
    {
        if(playerHere && Input.GetButtonDown("Interraction"))
        {
            isPressed = true;
        }
    }
}
