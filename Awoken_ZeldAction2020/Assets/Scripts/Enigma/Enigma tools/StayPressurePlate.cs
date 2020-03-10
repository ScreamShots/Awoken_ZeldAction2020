using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayPressurePlate : PressurePlateBehavior
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        /*if(other.tag == "CollisionDetection" && other.transform.root.tag == "Player" && elementsOnPlate != null)
        {
            isPressed = true;
        }
        if (other.gameObject.tag == "ObjectToMove" && isPressed == false && elementsOnPlate != null)
        {
            isPressed = true;
        }*/
    }
}
