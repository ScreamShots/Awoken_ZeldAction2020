using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ArcherSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Damage Sound")]
    public AudioClip archerDamage;
    [Range(0f, 1f)] public float archerDamageVolume = 0.5f;

    public AudioClip archerDeath;
    [Range(0f, 1f)] public float archerDeathVolume = 0.5f;

    [Space]
    [Header("Attack Sound")]
    public AudioClip archerAgro;
    [Range(0f, 1f)] public float archernAgroVolume = 0.5f;

    public AudioClip archerShoot;
    [Range(0f, 1f)] public float archerShootVolume = 0.5f;

    private EnemyHealthSystem enemyHealthScript;
    private ArcherAttack archerAttackScript;
    private ArcherMovement archerMovementScript;
    
    private bool archerTakeDmg = false;
    private bool archerIsAttacking = false;
    private bool playerDetected = false;

    #endregion

    void Start()
    {
        enemyHealthScript = GetComponentInParent<EnemyHealthSystem>();
        archerAttackScript = GetComponentInParent<ArcherAttack>();
        archerMovementScript = GetComponentInParent<ArcherMovement>();

        enemyHealthScript.onDead.AddListener(Dead);
    }

    void Update()
    {
        ArcherStoptted();
        ArcherAttack();
        ArcherTakeDamage();
    }

    void ArcherStoptted()
    {
        if (archerMovementScript.playerIsAggro)
        {
            if (!playerDetected)
            {
                playerDetected = true;
                SoundManager.Instance.PlaySfx(archerAgro, archernAgroVolume);
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    void ArcherAttack()
    {
        if (archerAttackScript.animationAttack)
        {
            if (!archerIsAttacking)
            {
                archerIsAttacking = true;
                SoundManager.Instance.PlaySfx(archerShoot, archerShootVolume);
            }
        }
        else
        {
            archerIsAttacking = false;
        }
    }

    void ArcherTakeDamage()
    {
        if (enemyHealthScript.canFlash && enemyHealthScript.currentHp >= 10)
        {
            if (!archerTakeDmg)
            {
                archerTakeDmg = true;
                SoundManager.Instance.PlaySfx(archerDamage, archerDamageVolume);
            }
        }
        else
        {
            archerTakeDmg = false;
        }
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(archerDeath, archerDeathVolume);
    }
}
