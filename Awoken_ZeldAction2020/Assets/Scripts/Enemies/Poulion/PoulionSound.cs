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
    private PrefabSoundManager poulionManager;
    private PoulionMovementReworked poulionMove;
    private PoulionAttackReworked poulionAttack;
    private EnemyHealthSystem poulionHealth;

    private bool l_playerDetected;
    private bool l_isCharging;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        poulionManager = GetComponent<PrefabSoundManager>();
        poulionMove = GetComponentInParent<PoulionMovementReworked>();
        poulionAttack = GetComponentInParent<PoulionAttackReworked>();
        poulionHealth = GetComponentInParent<EnemyHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();

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

    void Death()
    {
        if(poulionHealth.currentHp <= 0)
        {
            SoundManager.Instance.Play("PoulionDeath");
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
