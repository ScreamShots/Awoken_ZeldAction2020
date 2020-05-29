using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PoulionSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Damage Sound")]
    public AudioClip poulionDamage;
    [Range(0f, 1f)] public float poulionDamageVolume = 0.5f;

    public AudioClip poulionDeath;
    [Range(0f, 1f)] public float poulionDeathVolume = 0.5f;

    [Space]
    [Header("Attack Sound")]
    public AudioClip poulionAgro;
    [Range(0f, 1f)] public float poulionAgroVolume = 0.5f;

    public AudioClip poulionCharge;
    [Range(0f, 1f)] public float poulionChargeVolume = 0.5f;

    private EnemyHealthSystem enemyHealthScript;
    private PoulionMovementReworked poulionMovementScript;
    private PoulionAttackReworked poulionAttackScript;

    private bool poulionTakeDmg = false;
    private bool poulionIsAttacking = false;
    private bool playerDetected = false;

    #endregion

    void Start()
    {
        enemyHealthScript = GetComponentInParent<EnemyHealthSystem>();
        poulionMovementScript = GetComponentInParent<PoulionMovementReworked>();
        poulionAttackScript = GetComponentInParent<PoulionAttackReworked>();

        enemyHealthScript.onDead.AddListener(Dead);
    }

    void Update()
    {
        PoulionStoptted();
        PoulionAttack();
        PoulionTakeDamage();
    }

    void PoulionStoptted()
    {
        if (poulionMovementScript.playerDetected && !poulionAttackScript.isPreparingCharge && !poulionAttackScript.isCharging && !poulionAttackScript.isStun)
        {
            if (!playerDetected)
            {
                playerDetected = true;
                SoundManager.Instance.PlaySfx(poulionAgro, poulionAgroVolume);
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    void PoulionAttack()
    {
        if (poulionAttackScript.isCharging)
        {
            if (!poulionIsAttacking)
            {
                poulionIsAttacking = true;
                SoundManager.Instance.PlaySfx(poulionCharge, poulionChargeVolume);
            }
        }
        else
        {
            poulionIsAttacking = false;
        }
    }

    void PoulionTakeDamage()
    {
        if (enemyHealthScript.canFlash && enemyHealthScript.currentHp >= 10)
        {
            if (!poulionTakeDmg)
            {
                poulionTakeDmg = true;
                SoundManager.Instance.PlaySfx(poulionDamage, poulionDamageVolume);
            }
        }
        else
        {
            poulionTakeDmg = false;
        }
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(poulionDeath, poulionDeathVolume);
    }
}
