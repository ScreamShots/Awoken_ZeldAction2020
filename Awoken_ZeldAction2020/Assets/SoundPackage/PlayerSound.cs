using System.Collections;
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
    private BlockHandler projectile;

    private bool l_isBlocking;
    private bool l_isAttacking;
    private bool l_currentStamina;
    private float l_fragmentNumber;
    private bool l_onParry;
    private bool l_blockingAnElement;
    private bool l_canFlash;
    private bool l_hasBeenLaunchedBack;
    #endregion

    void Start()
    {
        l_fragmentNumber = PlayerManager.fragmentNumber;
        playerMoveScript = GetComponentInParent<PlayerMovement>();
        playerAttackScript = GetComponentInParent<PlayerAttack>();
        healthSystem = GetComponentInParent<PlayerHealthSystem>();
        playerShield = GetComponentInParent<PlayerShield>();
        projectile = GetComponent<BlockHandler>();
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


        if (l_blockingAnElement != playerShield.blockingAnElement)  
        {
            if (playerShield.blockingAnElement == true)
            {
                OnBlock();
            }

            l_blockingAnElement = playerShield.blockingAnElement;
        }

       if (l_canFlash != healthSystem.canFlash)
       {
           if (healthSystem.canFlash == true)
           {
               PlayerTakeDamage();
           }
           l_canFlash = healthSystem.canFlash;
       }

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

        if (l_hasBeenLaunchedBack != projectile.hasBeenLaunchBack)
        {
            if (projectile.hasBeenLaunchBack == true)
            {
                Debug.Log("stop");
                SoundManager.Instance.Stop("PlayerParry");
            }
            l_hasBeenLaunchedBack = projectile.hasBeenLaunchBack;
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
           if (PlayerStatusManager.Instance.isAttacking == true)
           {
                if(playerAttackScript.inRangeElement.Count != 0)
                {
                    for (int i = 0; i < playerAttackScript.inRangeElement.Count; i++)
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
                }
                else
                {
                    if (playerAttackScript.attackState == 1)
                    {

                        SoundManager.Instance.Play("Attack1");
                    }
                    else if (playerAttackScript.attackState == 2)
                    {
                        SoundManager.Instance.Play("Attack2");
                    }
                    else if (playerAttackScript.attackState == 3)
                    {
                        SoundManager.Instance.Play("Attack3");
                    }
                }
               

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

    void NewFragment()
    {
        SoundManager.Instance.Play("NewFragment");
    }

    void Parry()
    {
        SoundManager.Instance.PlayOnlyOnce("PlayerParry");
    }


}
