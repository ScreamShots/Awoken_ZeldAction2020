using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Run Sound")]
    public AudioClip playerRun;
    [Range(0f, 1f)] public float playerRunVolume = 0.5f;

    public AudioClip playerRunShield;
    [Range(0f, 1f)] public float playerRunShieldVolume = 0.5f;

    [Space]
    [Header("Damage Sound")]
    public AudioClip playerDamage;
    [Range(0f, 1f)] public float playerDamageVolume = 0.5f;

    [Space]
    [Header("Dead Sound")]
    public AudioClip playerDead;
    [Range(0f, 1f)] public float playerDeadVolume = 0.5f;

    private PlayerHealthSystem playerHealthScript;
    private PlayerMovement playerMovementScript;

    private bool playerTakingDamage = false;
    private bool playerIsDead = false;
    #endregion

    void Start()
    {
        playerHealthScript = GetComponentInParent<PlayerHealthSystem>();
        playerMovementScript = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        PlayerDamage();
        PlayerDead();
        PlayerRun();
    }

    void PlayerDamage()
    {
        if (playerHealthScript.canFlash)
        {
            if (!playerTakingDamage)
            {
                playerTakingDamage = true;
                SoundManager.Instance.PlaySfx(playerDamage, playerDamageVolume);
            }
        }
        else
        {
            playerTakingDamage = false;
        }
    }

    void PlayerDead()
    {
        if (playerHealthScript.currentHp <= 0)
        {
            if (!playerIsDead)
            {
                playerIsDead = true;
                SoundManager.Instance.PlaySfx(playerDead, playerDeadVolume);
            }
        }
        else
        {
            playerIsDead = false;
        }
    }

    void PlayerRun()
    {
        if (playerMovementScript.isRunning && !PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isAttacking)                //Normal run : without shield
        {
            SoundManager.Instance.PlayFootSteps(playerRun, playerRunVolume);
        }
        else if (playerMovementScript.isRunning && PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isAttacking)            //Shield run : with shield up 
        {
            SoundManager.Instance.PlayFootSteps(playerRunShield, playerRunShieldVolume);
        }
        else if (!playerMovementScript.isRunning)
        {
            //SoundManager.Instance.Stop("FootstepsNoShield");
            //SoundManager.Instance.Stop("FootstepsShield");
        }
    }
}
