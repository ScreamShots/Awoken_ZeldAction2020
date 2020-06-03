using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// This script takes the pressure plate behavior szcript and applies the on TriggerEnter of it
/// </summary>
public class InstantPressurePlate : PressurePlateBehavior
{
    [Header("Particle")]
    public Transform particleSpawn = null;
    public GameObject particlePouf = null;
    private bool particleExist = false;

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

    void Update()
    {
        if (isPressed)
        {
            if (!particleExist)
            {
                particleExist = true;

                GameObject particleInstance = Instantiate(particlePouf, particleSpawn.position, particlePouf.transform.rotation);
                Destroy(particleInstance, 0.5f);
            }
        }
        else
        {
            particleExist = false;
        }
    }
}
