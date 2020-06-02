using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MinototaureSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Damage Sound")]
    public AudioClip MinototaureDamage;
    [Range(0f, 1f)] public float MinototaureDamageVolume = 0.5f;

    public AudioClip MinototaureDeath;
    [Range(0f, 1f)] public float MinototaureDeathVolume = 0.5f;

    [Space]
    [Header("Attack Sound")]
    public AudioClip MinototaureAgro;
    [Range(0f, 1f)] public float MinototaureAgroVolume = 0.5f;

    public AudioClip MinototaureChargeAttack;
    [Range(0f, 1f)] public float MinototaureChargeAttackVolume = 0.5f;

    public AudioClip MinototaureAttack;
    [Range(0f, 1f)] public float MinototaureAttackVolume = 0.5f;

    private EnemyHealthSystem enemyHealthScript;
    private MinototaureAttack minototaureAttackScript;
    private MinototaureMovement minototaureMovementScript;

    private bool MinototaureTakeDmg = false;
    private bool MinototaurePrepare = false;
    private bool MinototaureIsAttacking = false;
    private bool playerDetected = false;

    #endregion

    void Start()
    {
        enemyHealthScript = GetComponentInParent<EnemyHealthSystem>();
        minototaureAttackScript = GetComponentInParent<MinototaureAttack>();
        minototaureMovementScript = GetComponentInParent<MinototaureMovement>();

        enemyHealthScript.onDead.AddListener(Dead);
    }

    void Update()
    {
        MinototaureStoptted();
        MinototaurePrepareAttack();
        MinototaureLauchAttack();
        MinototaureTakeDamage();
    }

    void MinototaureStoptted()
    {
        if (minototaureMovementScript.playerIsAggro)
        {
            if (!playerDetected)
            {
                playerDetected = true;
                SoundManager.Instance.PlaySfx(MinototaureAgro, MinototaureAgroVolume);
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    void MinototaurePrepareAttack()
    {
        if (minototaureAttackScript.isPreparingAttack)
        {
            if (!MinototaurePrepare)
            {
                MinototaurePrepare = true;
                SoundManager.Instance.PlaySfx(MinototaureChargeAttack, MinototaureChargeAttackVolume);
            }
        }
        else
        {
            MinototaurePrepare = false;
        }
    }

    void MinototaureLauchAttack()
    {
        if (minototaureAttackScript.isAttacking)
        {
            if (!minototaureAttackScript.isPreparingAttack)
            {
                if (!MinototaureIsAttacking)
                {
                    MinototaureIsAttacking = true;
                    if (minototaureAttackScript.minototaureDetectScript.overlappedShield != null)
                    {
                        if (!minototaureAttackScript.minototaureDetectScript.overlappedShield.GetComponent<ShieldHitZone>().isActivated)
                        {
                            SoundManager.Instance.PlaySfx(MinototaureAttack, MinototaureAttackVolume);
                        }
                    }
                }
            }
        }
        else
        {
            MinototaureIsAttacking = false;
        }
    }

    void MinototaureTakeDamage()
    {
        if (enemyHealthScript.canFlash && enemyHealthScript.currentHp >= 10)
        {
            if (!MinototaureTakeDmg)
            {
                MinototaureTakeDmg = true;
                SoundManager.Instance.PlaySfx(MinototaureDamage, MinototaureDamageVolume);
            }
        }
        else
        {
            MinototaureTakeDmg = false;
        }
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(MinototaureDeath, MinototaureDeathVolume);
    }
}
