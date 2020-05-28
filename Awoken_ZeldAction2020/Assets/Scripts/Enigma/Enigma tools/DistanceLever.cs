using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLever : PressurePlateBehavior
{
    [SerializeField]
    private BoxCollider2D hitbox = null;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) || other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            isPressed = true;
            Destroy(other.gameObject);
            hitbox.enabled = false;
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {

    }

    protected override void OnTriggerExit2D(Collider2D other)
    {

    }
}
