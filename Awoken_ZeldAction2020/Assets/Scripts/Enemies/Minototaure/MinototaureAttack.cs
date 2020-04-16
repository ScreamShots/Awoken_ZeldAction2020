﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather attack of the Minototaure
/// </summary>

public class MinototaureAttack : MonoBehaviour
{
    #region Variables
    //global

    MinototaureMovement minototaureMoveScript;
    MinototaureDetectZone minototaureDetectScript;
    EnemyHealthSystem minototaureHealthScript;
    GameObject player;

    //attack

    [HideInInspector]
    public bool isAttacking;                //is the enemy on and attack state
    [HideInInspector]
    public bool isPreparingAttack;          //is the enemy preparing for charge
    [HideInInspector]
    public bool lauchAttack;                //is the enemy attack
    [HideInInspector]
    public bool isStun;                     //is the enemy stun
    [HideInInspector]
    public bool cooldownAttack;             //is the enemy is on cooldown;
    #endregion

    #region Inspector Settings
    [SerializeField] private GameObject attackZone = null;

    //Part1 - Preparation
    [Header("Part1 - Preparation")]

    [SerializeField]
    [Min(0)]
    float prepareChargeTime = 0;

    [SerializeField]
    [Min(0)]
    float timeBtwAttack = 0;

    [Header("Part2 - Stun")]

    [SerializeField]
    [Min(0)]
    float stunTime = 0;

    [Header("Stats")]

    [SerializeField]
    [Min(0)]
    float dmg = 0;
    [SerializeField]
    [Min(0)]
    float staminaLost = 0;
    #endregion

    private void Start()
    {
        minototaureMoveScript = GetComponent<MinototaureMovement>();
        minototaureDetectScript = attackZone.GetComponent<MinototaureDetectZone>();
        minototaureHealthScript = GetComponent<EnemyHealthSystem>();
        player = PlayerManager.Instance.gameObject;
    }

    private void Update()
    {
        if (!isStun)
        {
            AttackRotation();
        }

        if (minototaureMoveScript.playerInAttackRange && !isAttacking && !cooldownAttack)          
        {
            minototaureMoveScript.canMove = false;                              
            isAttacking = true;
            StartCoroutine(ChargePreparation());                           
        }
    }

    private void FixedUpdate()
    {
        if (lauchAttack)                                                                        
        {
            Attack();
        }
    }

    void Attack()
    {
        if (minototaureDetectScript.isOverlappingShield == true)
        {
            if (minototaureDetectScript.overlappedShield.GetComponent<ShieldHitZone>().isActivated)
            {
                lauchAttack = false;
                isStun = true;
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                StartCoroutine(Stun());
            }
            else if (minototaureDetectScript.isOverlappingPlayer)
            {
                lauchAttack = false;
                isStun = true;
                player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
                StartCoroutine(Stun());
            }
        }
        else if (minototaureDetectScript.isOverlappingPlayer == true && !minototaureDetectScript.isOverlappingShield)
        {
            lauchAttack = false;
            isStun = true;
            player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
            StartCoroutine(Stun());
        }
        else
        {
            lauchAttack = false;
            isStun = true;
            StartCoroutine(Stun());
        }
    }

    void AttackRotation()                                                       //rotate the attack collider linked to the watch direction
    {
        switch (minototaureMoveScript.watchDirection)
        {
            case MinototaureMovement.Direction.down:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MinototaureMovement.Direction.up:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case MinototaureMovement.Direction.right:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MinototaureMovement.Direction.left:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }

    IEnumerator ChargePreparation()     //Coroutine that handle Charge Preparation
    {
        minototaureHealthScript.canTakeDmg = false;
        isPreparingAttack = true;                                   //animator bool
        yield return new WaitForSeconds(prepareChargeTime);         //Wait the preparation duration
        isPreparingAttack = false;                                  //animator bool
        lauchAttack = true;
    }

    IEnumerator Stun()                  //Coroutine that handle stun
    {
        minototaureHealthScript.canTakeDmg = true;
        yield return new WaitForSeconds(stunTime + 0.5f);       //wait the stun duration
        minototaureHealthScript.canTakeDmg = false;
        isStun = false;
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;                                    //saying that the attack is finished
        minototaureMoveScript.canMove = true;

        StartCoroutine(WaitBeforeAttack());
    }

    IEnumerator WaitBeforeAttack()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(timeBtwAttack);
        cooldownAttack = false;
    }
}
