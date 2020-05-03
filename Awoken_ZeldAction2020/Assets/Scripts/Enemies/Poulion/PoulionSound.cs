using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du Poulion
/// </summary>


public class PoulionSound : MonoBehaviour
{
    #region HideInInspector var Statement
    private EnemyHealthSystem healthSystem;
    private PrefabSoundManager poulionManager;
    private PoulionMovementReworked poulionMove;
    private PoulionAttackReworked poulionAttack;

    private bool l_playerDetected;
    private bool l_isCharging;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponentInParent<EnemyHealthSystem>();
        poulionManager = GetComponent<PrefabSoundManager>();
        poulionMove = GetComponentInParent<PoulionMovementReworked>();
        poulionAttack = GetComponentInParent<PoulionAttackReworked>();
    }

    // Update is called once per frame
    void Update()
    {
        DeathSound();

        if (l_playerDetected != poulionMove.playerDetected)
        {
            if (poulionMove.playerDetected == true)
            {
                Spotted();
            }

            l_playerDetected = poulionMove.playerDetected;
        }

        if (l_isCharging != poulionAttack.isCharging)
        {
            if (poulionAttack.isCharging == true)
            {
                Attack();
            }

            l_isCharging = poulionAttack.isCharging;
        }
    }

    void DeathSound()
    {
        if (healthSystem.currentHp <= 0)
        {
            poulionManager.PlayOnlyOnce("PoulionDeath");
        }
    }

    void Spotted()
    {
        if (poulionMove.playerDetected == true && poulionAttack.isPreparingCharge == false && poulionAttack.isCharging == false && poulionAttack.isStun == false)
        {
            poulionManager.Play("PoulionSpotted");
        }
    }

    void Attack()
    {
        if (poulionAttack.isCharging == true)
        {
            poulionManager.Play("PoulionCharge");
        }
    }
}
