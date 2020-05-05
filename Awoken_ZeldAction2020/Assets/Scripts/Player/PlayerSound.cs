﻿using System.Collections;
using System.Collections.Generic;
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
    private PlayerHealthSystem healthSystem;
    private PlayerShield playerShield;

    private bool l_isBlocking;
    private bool l_isAttacking;
    private bool l_currentStamina;
    private float l_fragmentNumber;
    private bool l_onParry;
    private bool l_blocked;
    private bool l_canFlash;
    #endregion

    void Start()
    {
        l_fragmentNumber = PlayerManager.fragmentNumber;
        playerMoveScript = GetComponentInParent<PlayerMovement>();
        playerAttackScript = GetComponentInParent<PlayerAttack>();
        healthSystem = GetComponentInParent<PlayerHealthSystem>();
        playerShield = GetComponentInParent<PlayerShield>();
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

        //Nécessite l'ajout d'un bool blocked dans le script PlayerShield, le bool doit passer en "true" avant le démarrage de la couroutine OnBlocked.
        // Cette variable doit passer à "false" à la fin de la couroutine
        /*if (l_blocked != playerShield.blocked)  
        {
            if (playerShield.blocked == true)
            {
                OnBlock();
            }

            l_blocked = playerShield.blocked;
        }*/

        /*if (l_canFlash != healthSystem.canFlash) //Nécesite de rendre la variable EnemyHealthSystem.canFlash public pour fonctionner
       {
           if (healthSystem.canFlash == true)
           {
               PlayerTakeDamage();
           }
           l_canFlash = healthSystem.canFlash;
       }*/

        if (l_fragmentNumber != PlayerManager.fragmentNumber)
        {
            NewFragment();
            l_fragmentNumber = PlayerManager.fragmentNumber;
        }

        if(l_onParry != playerShield.onPary)
        {
            if(playerShield.onPary == true)
            {
                Parry();
            }
            l_onParry = playerShield.onPary;
        }
    }

    void DeathSound()
    {
        if (healthSystem.currentHp <= 0)
        {
            SoundManager.Instance.Play("PlayerDeath");
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
          /* if (PlayerStatusManager.Instance.isAttacking == true)
           {
               for (int i = 0; i < playerAttackScript.inRangeElement.Count; i++)   //Nécessite de rendre la variable PlayerAttack.inRangeElement public pour fonctionner
               {
                   if (playerAttackScript.attackState == 1 && playerAttackScript.inRangeElement.Count <= 0 && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == true) 
                   {
                       SoundManager.Instance.Play("Attack1");
                   }
                   else if (playerAttackScript.attackState == 2 && playerAttackScript.inRangeElement.Count <= 0 && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == true)
                   {
                       SoundManager.Instance.Play("Attack2");
                   }
                   else if (playerAttackScript.attackState == 3 && playerAttackScript.inRangeElement.Count <= 0 && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == true)
                   {
                       SoundManager.Instance.Play("Attack3");
                   }
                   else if (playerAttackScript.attackState == 1 && playerAttackScript.inRangeElement.Count > 0 && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == true)
                   {
                       SoundManager.Instance.Play("AttackHitEnnemy1");
                   }
                   else if (playerAttackScript.attackState == 2 && playerAttackScript.inRangeElement.Count > 0 && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == true)
                   {
                       SoundManager.Instance.Play("AttackHitEnnemy2");
                   }
                   else if (playerAttackScript.attackState == 3 && playerAttackScript.inRangeElement.Count > 0 && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == true)
                   {
                       SoundManager.Instance.Play("AttackHitEnnemy3");
                   }
                   else if (playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg == false && playerAttackScript.inRangeElement.Count > 0)
                   {
                       SoundManager.Instance.Play("NoDamage");
                   }
                }

            }*/
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
        Debug.Log("Blocked");
        SoundManager.Instance.Play("BlockedAttack");
    }

    void NewFragment()
    {
        SoundManager.Instance.Play("NewFragment");
    }

    void Parry()
    {
        SoundManager.Instance.Play("PlayerParry");
    }


}
