using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLever : PressurePlateBehavior
{
    // Start is called before the first frame update
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")))
        {
            isPressed = true;
            elementsOnPlate.Add(other.transform.root.gameObject);
            Destroy(other.gameObject);
        }
    }
}
