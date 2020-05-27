using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Attack Sound")]
    public AudioClip attack1;
    [Range(0f, 1f)] public float attack1Volume = 0.5f;

    public AudioClip attack2;
    [Range(0f, 1f)] public float attack2Volume = 0.5f;

    public AudioClip attack3;
    [Range(0f, 1f)] public float attack3Volume = 0.5f;

    [Space]
    public AudioClip hitAttack1;
    [Range(0f, 1f)] public float hitAttack1Volume = 0.5f;

    public AudioClip hitAttack2;
    [Range(0f, 1f)] public float hitAttack2Volume = 0.5f;

    public AudioClip hitAttack3;
    [Range(0f, 1f)] public float hitAttack3Volume = 0.5f;

    [Space]
    public AudioClip gameElementAttack1;
    [Range(0f, 1f)] public float gameElementAttack1Volume = 0.5f;

    public AudioClip gameElementAttack2;
    [Range(0f, 1f)] public float gameElementAttack2Volume = 0.5f;

    public AudioClip gameElementAttack3;
    [Range(0f, 1f)] public float gameElementAttack3Volume = 0.5f;

    [Space]
    public AudioClip indestructibleElementAttack;
    [Range(0f, 1f)] public float indestructibleElementAttackVolume = 0.5f;

    [Space]
    public AudioClip enemyInvulnerable;
    [Range(0f, 1f)] public float enemyInvulnerableVolume = 0.5f;

    [Space]
    public AudioClip enemyProtected;
    [Range(0f, 1f)] public float enemyProtectedVolume = 0.5f;

    [Space]
    [Header("Run Sound")]
    public AudioClip playerRun;
    [Range(0f, 1f)] public float playerRunVolume = 0.5f;

    public AudioClip playerRunShield;
    [Range(0f, 1f)] public float playerRunShieldVolume = 0.5f;

    [Space]
    [Header("Charge Sound")]
    public AudioClip playerRunCharge;
    [Range(0f, 1f)] public float playerRunChargeVolume = 0.5f;

    public AudioClip chargeExplosion;
    [Range(0f, 1f)] public float chargeExplosionVolume = 0.5f;

    public AudioClip furryBarFull;
    [Range(0f, 1f)] public float furryBarFullVolume = 0.5f;

    [Space]
    [Header("Shield Sound")]
    public AudioClip breakShield;
    [Range(0f, 1f)] public float breakShieldVolume = 0.5f;

    public AudioClip shieldOut;
    [Range(0f, 1f)] public float shieldOutVolume = 0.5f;

    public AudioClip elementBlocked;
    [Range(0f, 1f)] public float elementBlockedVolume = 0.5f;

    public AudioClip parry;
    [Range(0f, 1f)] public float parryVolume = 0.5f;

    [Space]
    [Header("Damage Sound")]
    public AudioClip playerDamage;
    [Range(0f, 1f)] public float playerDamageVolume = 0.5f;

    [Space]
    [Header("Dead Sound")]
    public AudioClip playerDead;
    [Range(0f, 1f)] public float playerDeadVolume = 0.5f;
    public AudioClip deadSound;
    [Range(0f, 1f)] public float deadSoundVolume = 0.5f;

    private PlayerHealthSystem playerHealthScript;
    private PlayerMovement playerMovementScript;
    private PlayerShield playerShieldScript;
    private PlayerCharge playerChargeScript;
    private PlayerAttack playerAttackScript;

    private bool playerTakingDamage = false;
    private bool playerIsDead = false;
    private bool playerIsCharging = false;
    private bool playerIsAttacking = false;
    private bool shieldBreak = false;
    private bool explosionIsPlay = false;
    private bool shieldIsOut = false;
    private bool elementBlock = false;
    private bool elementParry = false;
    private bool furryBarIsFull = false;
    #endregion

    void Start()
    {
        playerHealthScript = GetComponentInParent<PlayerHealthSystem>();
        playerMovementScript = GetComponentInParent<PlayerMovement>();
        playerShieldScript = GetComponentInParent<PlayerShield>();
        playerChargeScript = GetComponentInParent<PlayerCharge>();
        playerAttackScript = GetComponentInParent<PlayerAttack>();
    }

    void Update()
    {
        PlayerDamage();
        PlayerDead();
        PlayerAttack();

        PlayerRun();

        NoStamina();
        ExplosionCharge();
        FurryIsFull();
        ShieldTakeOut();
        ElementBlock();
        Parry();
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
                SoundManager.Instance.PlaySfx(deadSound, deadSoundVolume);
            }
        }
        else
        {
            playerIsDead = false;
        }
    }

    void PlayerRun()
    {
        if (playerMovementScript.isRunning && !PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isAttacking && !PlayerStatusManager.Instance.isCharging)                //Normal run : without shield
        {
            SoundManager.Instance.PlayFootSteps(playerRun, playerRunVolume);
            playerIsCharging = false;
        }
        else if (playerMovementScript.isRunning && PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isAttacking && !PlayerStatusManager.Instance.isCharging)            //Shield run : with shield up 
        {
            SoundManager.Instance.PlayFootSteps(playerRunShield, playerRunShieldVolume);
            playerIsCharging = false;
        }
        else if (playerMovementScript.isRunning && !PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isAttacking && PlayerStatusManager.Instance.isCharging)
        {
            if (!playerIsCharging)
            {
                playerIsCharging = true;
                SoundManager.Instance.PlaySfx(playerRunCharge, playerRunChargeVolume);
            }
        }
        else
        {
            playerIsCharging = false;
        }
    }

    void PlayerAttack()
    {
        if (PlayerStatusManager.Instance.isAttacking)
        {
            if (!playerIsAttacking)
            {
                playerIsAttacking = true;
                WhichAttack();
            }
        }
        else
        {
            playerIsAttacking = false;
        }
    }

    void WhichAttack()
    {
        if (playerAttackScript.inRangeElement.Count > 0)                                                    //player attack an element
        {
            for (int i = 0; i < playerAttackScript.inRangeElement.Count; i++)
            {
                if (playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>() != null)
                {
                    if (playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg)  //enemy can be attack
                    {
                        if (playerAttackScript.attackState == 1)
                        {
                            SoundManager.Instance.PlaySfx(hitAttack1, hitAttack1Volume);
                        }
                        else if (playerAttackScript.attackState == 2)
                        {
                            SoundManager.Instance.PlaySfx(hitAttack2, hitAttack2Volume);
                        }
                        else if (playerAttackScript.attackState == 3)
                        {
                            SoundManager.Instance.PlaySfx(hitAttack3, hitAttack3Volume);
                        }
                    }
                    else if (!playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg && playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().protectedByPegase)    //enemy is protect by Pegase
                    {
                        if (playerAttackScript.attackState == 1 || playerAttackScript.attackState == 2 || playerAttackScript.attackState == 3)
                        {
                            SoundManager.Instance.PlaySfx(enemyProtected, enemyProtectedVolume);
                        }
                    }
                    else if (!playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg && !playerAttackScript.inRangeElement[i].GetComponent<EnemyHealthSystem>().protectedByPegase)   //enemy can't be attack (relative to pattern)
                    {
                        if (playerAttackScript.attackState == 1 || playerAttackScript.attackState == 2 || playerAttackScript.attackState == 3)
                        {
                            SoundManager.Instance.PlaySfx(enemyInvulnerable, enemyInvulnerableVolume);
                        }
                    }
                }
                else if (playerAttackScript.inRangeElement[i].GetComponent<GameElementsHealthSystem>() != null)     //player attack game element, ex : turret
                {
                    if (playerAttackScript.inRangeElement[i].GetComponent<GameElementsHealthSystem>().canTakeDmg)   //game element destructible
                    {
                        if (playerAttackScript.attackState == 1)
                        {
                            SoundManager.Instance.PlaySfx(gameElementAttack1, gameElementAttack1Volume);
                        }
                        else if (playerAttackScript.attackState == 2)
                        {
                            SoundManager.Instance.PlaySfx(gameElementAttack2, gameElementAttack2Volume);
                        }
                        else if (playerAttackScript.attackState == 3)
                        {
                            SoundManager.Instance.PlaySfx(gameElementAttack3, gameElementAttack3Volume);
                        }
                    }
                    else                                                                                            //player element indestructible
                    {
                        if (playerAttackScript.attackState == 1 || playerAttackScript.attackState == 2 || playerAttackScript.attackState == 3)
                        {
                            SoundManager.Instance.PlaySfx(indestructibleElementAttack, indestructibleElementAttackVolume);
                        }
                    }
                }
            }
        }
        else                                                                                                        //player attack nothing : strike the air
        {
            if (playerAttackScript.attackState == 1)
            {
                SoundManager.Instance.PlaySfx(attack1, attack1Volume);
            }
            else if (playerAttackScript.attackState == 2)
            {
                SoundManager.Instance.PlaySfx(attack2, attack2Volume);
            }
            else if (playerAttackScript.attackState == 3)
            {
                SoundManager.Instance.PlaySfx(attack3, attack3Volume);
            }
        }
    }

    void NoStamina()
    {
        if (playerShieldScript.currentStamina == 0)
        {
            if (!shieldBreak)
            {
                shieldBreak = true;
                SoundManager.Instance.PlaySfx(breakShield, breakShieldVolume);
            }
        }
        else
        {
            shieldBreak = false;
        }
    }

    void ExplosionCharge()
    {
        if (playerChargeScript.explosionIsPlaying)
        {
            if (!explosionIsPlay)
            {
                explosionIsPlay = true;
                SoundManager.Instance.PlaySfx(chargeExplosion, chargeExplosionVolume);
            }
        }
        else
        {
            explosionIsPlay = false;
        }
    }

    void FurryIsFull()
    {
        if(playerAttackScript.currentFury == playerAttackScript.maxFury)
        {
            if (!furryBarIsFull)
            {
                furryBarIsFull = true;
                SoundManager.Instance.PlaySfx(furryBarFull, furryBarFullVolume);
            }
        }
        else
        {
            furryBarIsFull = false;
        }
    }

    void ShieldTakeOut()
    {
        if (PlayerStatusManager.Instance.isBlocking)
        {
            if (!shieldIsOut)
            {
                shieldIsOut = true;
                SoundManager.Instance.PlaySfx(shieldOut, shieldOutVolume);
            }
        }
        else
        {
            shieldIsOut = false;
        }
    }

    void ElementBlock()
    {
        if (playerShieldScript.blockingAnElement)
        {
            if (!elementBlock)
            {
                elementBlock = true;
                SoundManager.Instance.PlaySfx(elementBlocked, elementBlockedVolume);
            }
        }
        else
        {
            elementBlock = false;
        }
    }

    void Parry()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.ProjectilePary)
        {
            if (!elementParry)
            {
                elementParry = true;
                SoundManager.Instance.PlayParry(parry, parryVolume);
            }
        }
        else
        {
            elementParry = false;
            SoundManager.Instance.StopParry();
        }
    }
}
