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
    private bool l_launchAttack;
    private bool l_corouDeathPlay;
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

        if(l_launchAttack != minototaureAttack.lauchAttack)
        {
            if(minototaureAttack.lauchAttack == true)
            {
                Attack();
            }
            l_launchAttack = minototaureAttack.lauchAttack;
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
            minototaureManager.PlayOnlyOnce("MinototaureDeath");
        }
        else
        {
            minototaureManager.Stop("MinototaureDeath");
        }

    }
}
