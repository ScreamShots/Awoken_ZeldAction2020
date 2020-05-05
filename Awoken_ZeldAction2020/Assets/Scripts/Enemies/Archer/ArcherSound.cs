using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons de l'Archer
/// </summary>

public class ArcherSound : MonoBehaviour
{
    #region HideInInspector var Statement
    private EnemyHealthSystem healthSystem;
    private PrefabSoundManager archerManager;
    private ArcherMovement archerMove;
    private ArcherAttack archerAttack;
    private GameObject player;

    private bool l_archerAnimationAttack;
    private bool l_canFlash;
    #endregion

    void Start()
    {
        healthSystem = GetComponentInParent<EnemyHealthSystem>();
        archerManager = GetComponent<PrefabSoundManager>();
        archerMove = GetComponentInParent<ArcherMovement>();
        archerAttack = GetComponentInParent<ArcherAttack>();
        player = PlayerManager.Instance.gameObject;

    }

    void Update()
    {
        Death();
        Spotted();

        if(l_archerAnimationAttack != archerAttack.animationAttack)
        {
            if (archerAttack.animationAttack == true)
            {
                Attack();
            }
            l_archerAnimationAttack = archerAttack.animationAttack;
        }

        /*if (l_canFlash != healthSystem.canFlash) //Nécesite de rendre la variable EnemyHealthSystem.canFlash public pour fonctionner
       {
           if (healthSystem.canFlash == true)
           {
               ArcherDamaged();
           }
           l_canFlash = healthSystem.canFlash;
       }*/
    }

    void Death()
    {
        if (healthSystem.currentHp <= 0)
        {
            SoundManager.Instance.Play("ArcherDeath");
        }
    }

    void Spotted()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= archerMove.chaseDistance && distance > archerMove.attackDistance && !archerAttack.archerIsAttacking)
        {
            archerManager.PlayOnlyOnce("ArcherSpotted");
        }
        else
        {
            archerManager.Stop("ArcherSpotted");
        }

    }

    void Attack()
    {
        archerManager.Play("ArcherAttack");       
    }

    void ArcherDamaged()
    {
        archerManager.Play("ArcherDamaged");
    }
}
