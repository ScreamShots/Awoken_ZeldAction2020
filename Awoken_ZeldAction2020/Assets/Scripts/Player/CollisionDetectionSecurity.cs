using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionSecurity : MonoBehaviour
{
    PlayerCharge playerChargeScript;

    private void Start()
    {
        playerChargeScript = GetComponentInParent<PlayerCharge>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HitBox") && collision.transform.root.CompareTag("Enemy") && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            playerChargeScript.KnockBackEnemy(collision.transform.root.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default") || collision.gameObject.layer == LayerMask.NameToLayer("GameElements"))
        {
            if (!collision.isTrigger)
            {
                playerChargeScript.FastEndCharge();
            }
        }
    }
}
