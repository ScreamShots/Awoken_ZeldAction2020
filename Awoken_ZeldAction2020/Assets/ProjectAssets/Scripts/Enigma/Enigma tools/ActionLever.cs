using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// This script takes the pressure plate behavior szcript and applies the on triggerStay of it
/// </summary>
public class ActionLever : PressurePlateBehavior
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {

    }
    protected override void OnTriggerExit2D(Collider2D other)
    {

    }
    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
    }
}
