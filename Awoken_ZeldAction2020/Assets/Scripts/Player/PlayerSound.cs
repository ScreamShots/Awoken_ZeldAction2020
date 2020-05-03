using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du player
/// </summary>
public class PlayerSound : MonoBehaviour
{
    #region HideInInspector var Statement 

    private PlayerMovement playerMoveScript;
    private PlayerAttack playerAttackScript;
    private BasicHealthSystem healthSystem;
    private PlayerShield playerShield;
    private BlockHandler blocked;

    private bool l_isBlocking;
    private bool l_isAttacking;
    private bool l_currentStamina;
    private bool l_isBlocked;
    #endregion

    void Start()
    {
        
        playerMoveScript = GetComponentInParent<PlayerMovement>();
        playerAttackScript = GetComponentInParent<PlayerAttack>();
        healthSystem = GetComponentInParent<BasicHealthSystem>();
        playerShield = GetComponentInParent<PlayerShield>();
        blocked = GetComponent<BlockHandler>();
    }

    void Update()
    {
        DeathSound();
        NormalRun();
        NoStamina();

        if (l_isAttacking != PlayerStatusManager.Instance.isAttacking)
        {
            if (PlayerStatusManager.Instance.isAttacking == true)
            {
                Attack();
            }

            l_isAttacking = PlayerStatusManager.Instance.isAttacking;
        }
        if (l_isBlocking != PlayerStatusManager.Instance.isBlocking)
        {
            if(PlayerStatusManager.Instance.isBlocking == true)
            {
                TakeShieldOut();
            }

            l_isBlocking = PlayerStatusManager.Instance.isBlocking;
        }

        if (l_isBlocked != blocked.isBlocked)
        {
            if (blocked.isBlocked == true)
            {
                OnBlock();
            }

            l_isBlocked = blocked.isBlocked;
        }
    }

    void DeathSound()
    {
        if (healthSystem.currentHp <= 0)
        {
            SoundManager.Instance.PlayOnlyOnce("PlayerDeath");
        }
       
    }

    void NormalRun()
    {
        if (playerMoveScript.isRunning == true && PlayerStatusManager.Instance.isBlocking == false && PlayerStatusManager.Instance.isAttacking == false)
        {
            SoundManager.Instance.Stop("FootstepsShield");
            SoundManager.Instance.PlayOnlyOnce("FootstepsNoShield");
        }
        else if (playerMoveScript.isRunning == true && PlayerStatusManager.Instance.isBlocking == true && PlayerStatusManager.Instance.isAttacking == false)
        {
            SoundManager.Instance.Stop("FootstepsNoShield");
            SoundManager.Instance.PlayOnlyOnce("FootstepsShield");
        }
        else if (playerMoveScript.isRunning == false)
        {
            SoundManager.Instance.Stop("FootstepsNoShield");
            SoundManager.Instance.Stop("FootstepsShield");

        }
    }

    void Attack()
    {
        if (PlayerStatusManager.Instance.isAttacking == true)
        {
            /*if(playerAttackScript.attackState == 1 && playerAttackScript.inRangeElement.Count <= 0) //Nécessite de rendre la variable PlayerAttack.inRangeElement public pour fonctionner
            {
                SoundManager.Instance.Play("Attack1");
            }
            else if (playerAttackScript.attackState == 2 && playerAttackScript.inRangeElement.Count <= 0)
            {
                SoundManager.Instance.Play("Attack2");
            }
            else if (playerAttackScript.attackState == 3 && playerAttackScript.inRangeElement.Count <= 0)
            {
                SoundManager.Instance.Play("Attack3");
            }
            else if (playerAttackScript.attackState == 1 && playerAttackScript.inRangeElement.Count > 0)
            {
                SoundManager.Instance.Play("AttackHitEnnemy1");
            }
            else if (playerAttackScript.attackState == 2 && playerAttackScript.inRangeElement.Count > 0)
            {
                SoundManager.Instance.Play("AttackHitEnnemy2");
            }
            else if (playerAttackScript.attackState == 3 && playerAttackScript.inRangeElement.Count > 0)
            {
                SoundManager.Instance.Play("AttackHitEnnemy3");
            }*/

        }
    }

    void PlayerTakeDamage()
    {
        SoundManager.Instance.Play("DamagePlayer");
    }

    void TakeShieldOut()
    {

            SoundManager.Instance.Play("ShieldTakeOut");

        
    }

    void NoStamina()
    {
        if (playerShield.currentStamina == 0)
        {
            SoundManager.Instance.PlayOnlyOnce("StaminaOut");
        }
        else
        {
            SoundManager.Instance.Stop("StaminaOut");
        }
    }

    void OnBlock()
    {
            SoundManager.Instance.Play("BlockedAttack");


    }



}
