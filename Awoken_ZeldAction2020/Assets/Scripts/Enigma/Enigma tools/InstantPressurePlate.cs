using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPressurePlate : PressurePlateBehavior
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {

    }

    protected override void OnTriggerStay2D(Collider2D other)
    {

    }
}
