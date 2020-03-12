using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// This script takes the pressure plate behavior szcript and applies the on TriggerEnter of it
/// </summary>
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
