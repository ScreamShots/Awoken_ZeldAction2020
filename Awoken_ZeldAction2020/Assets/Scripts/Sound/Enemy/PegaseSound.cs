using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PegaseSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Damage Sound")]
    public AudioClip pegaseDamage;
    [Range(0f, 1f)] public float pegaseDamageVolume = 0.5f;

    public AudioClip pegaseDeath;
    [Range(0f, 1f)] public float pegaseDeathVolume = 0.5f;

    [Space]
    [Header("TP Sound")]
    public AudioClip pegaseTP;
    [Range(0f, 1f)] public float pegaseTPVolume = 0.5f;

    public AudioSource pegaseExhausted;
    [Range(0f, 1f)] public float pegaseExhaustedVolume = 0.5f;

    private EnemyHealthSystem enemyHealthScript;
    private PegaseMovement pegaseMovementScript;

    private bool pegaseTakeDmg = false;
    private bool pegaseIsTP = false;
    private bool pegaseIsExhausted = false;

    #endregion

    void Start()
    {
        enemyHealthScript = GetComponentInParent<EnemyHealthSystem>();
        pegaseMovementScript = GetComponentInParent<PegaseMovement>();

        enemyHealthScript.onDead.AddListener(Dead);
    }

    void Update()
    {
        PegaseTeleport();
        PegaseCooldown();
        PegaseTakeDamage();
    }

    void PegaseTeleport()
    {
        if (pegaseMovementScript.prepareTeleport)
        {
            if (!pegaseIsTP)
            {
                pegaseIsTP = true;
                SoundManager.Instance.PlaySfx(pegaseTP, pegaseTPVolume);
            }
        }
        else
        {
            pegaseIsTP = false;
        }
    }

    void PegaseCooldown()
    {
        if (pegaseMovementScript.cooldownActive)
        {
            if (!pegaseIsExhausted)
            {
                pegaseIsExhausted = true;
                SoundManager.Instance.PlayCubePushed(pegaseExhausted, pegaseExhaustedVolume);
            }
        }
        else
        {
            pegaseIsExhausted = false;
            SoundManager.Instance.StopCubePushed(pegaseExhausted);
        }
    }

    void PegaseTakeDamage()
    {
        if (enemyHealthScript.canFlash && enemyHealthScript.currentHp >= 10)
        {
            if (!pegaseTakeDmg)
            {
                pegaseTakeDmg = true;
                SoundManager.Instance.PlaySfx(pegaseDamage, pegaseDamageVolume);
            }
        }
        else
        {
            pegaseTakeDmg = false;
        }
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(pegaseDeath, pegaseDeathVolume);
    }
}
