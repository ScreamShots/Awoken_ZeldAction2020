using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeHitZone : ShieldHitZone
{
    PlayerCharge playerChargeScript;
    private void Start()
    {
        playerChargeScript = GetComponentInParent<PlayerCharge>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Default") || collision.gameObject.layer == LayerMask.NameToLayer("GameElements"))
        {
            if (!collision.isTrigger)
            {
                playerChargeScript.EndCharge();
            }            
        }

        if(collision.CompareTag("HitBox") && collision.transform.root.CompareTag("Enemy") && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            playerChargeScript.KnockBackEnemy(collision.transform.root.gameObject);
        }
    }
}
