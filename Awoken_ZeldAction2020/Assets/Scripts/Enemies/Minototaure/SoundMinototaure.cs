using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du Minototaure
/// </summary>

public class SoundMinototaure : MonoBehaviour
{
    private PrefabSoundManager minototaureManager;
    private MinototaureMovement minototaureMove;
    private MinototaureAttack minototaureAttack;
    private EnemyHealthSystem minototaureHealth;

    private bool l_playerDetected;
    private bool l_isPreparingAttack;
    private bool l_corouDeathPlay;
    private bool l_lauchAttack;
    private bool l_canFlash;
    // Start is called before the first frame update
    void Start()
    {
        minototaureManager = GetComponent<PrefabSoundManager>();
        minototaureMove = GetComponentInParent<MinototaureMovement>();
        minototaureAttack = GetComponentInParent<MinototaureAttack>();
        minototaureHealth = GetComponentInParent<EnemyHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();

        if(l_playerDetected != minototaureMove.playerDetected)
        {
            if(minototaureMove.playerDetected == true)
            {
                Spotted();
            }
            l_playerDetected = minototaureMove.playerDetected;
        }

        if(l_isPreparingAttack != minototaureAttack.isPreparingAttack)
        {
            if(minototaureAttack.isPreparingAttack == true)
            {
                Attack();
            }
            l_isPreparingAttack = minototaureAttack.isPreparingAttack;
        }

        if (l_lauchAttack != minototaureAttack.lauchAttack)
        {
            if (minototaureAttack.lauchAttack == true)
            {
                LaunchAttack();
            }
            l_lauchAttack = minototaureAttack.lauchAttack;
        }

        if (l_canFlash != minototaureHealth.canFlash) //Nécesite de rendre la variable EnemyHealthSystem.canFlash public pour fonctionner
       {
           if (minototaureHealth.canFlash == true)
           {
               MinototaureDamaged();
           }
           l_canFlash = minototaureHealth.canFlash;
       }
    }

    void Spotted()
    {
        minototaureManager.Play("MinototaureSpotted");
    }

    void Attack()
    {
        minototaureManager.Play("MinototaureAttack");
    }

    void Death()
    {
        if(minototaureHealth.currentHp <= 0)
        {
            SoundManager.Instance.Play("MinototaureDeath");
        }

    }

    void MinototaureDamaged()
    {
        minototaureManager.Play("MinototaureTakeHit");
    }

    void LaunchAttack()
    {
        minototaureManager.Play("MinototaureLaunchAttack");
    }
}
