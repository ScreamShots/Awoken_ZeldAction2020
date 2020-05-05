using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons des Pégases
/// </summary>

public class SoundPegase : MonoBehaviour
{
    private EnemyHealthSystem healthSystem;
    private PrefabSoundManager pegaseManager;
    private PegaseMovement pegaseMovement;

    private bool l_playerDetected;
    private bool l_isTeleport;
    private bool l_cooldownActive;
    private bool l_canFlash;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponentInParent<EnemyHealthSystem>();
        pegaseManager = GetComponent<PrefabSoundManager>();
        pegaseMovement = GetComponentInParent<PegaseMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();

        if(l_playerDetected != pegaseMovement.playerDetected)
        {
            if(pegaseMovement.playerDetected == true && pegaseMovement.cooldownActive == false)
            {
                Spotted();
            }
            l_playerDetected = pegaseMovement.playerDetected;
        }

        if(l_isTeleport != pegaseMovement.isTeleport)
        {
            if(pegaseMovement.isTeleport == true)
            {
                Teleportation();
            }
            l_isTeleport = pegaseMovement.isTeleport;
        }

        if (l_cooldownActive != pegaseMovement.cooldownActive)
        {
            if (pegaseMovement.cooldownActive == true)
            {
                StaminaOut();
            }
            l_cooldownActive = pegaseMovement.cooldownActive;
        }

       if (l_canFlash != healthSystem.canFlash) //Nécesite de rendre la variable EnemyHealthSystem.canFlash public pour fonctionner
       {
           if (healthSystem.canFlash == true)
           {
               PegaseDamaged();
           }
           l_canFlash = healthSystem.canFlash;
       }

    }

    void Death()
    {
        if (healthSystem.currentHp <= 0)
        {
            SoundManager.Instance.Play("PegaseDeath");
        }
    }

    void Spotted()
    {
        pegaseManager.Play("PegaseSpotted");
    }

    void Teleportation()
    {
        pegaseManager.Play("PegaseTp");
    }

    void StaminaOut()
    {
        pegaseManager.Play("PegasePanic");
    }

    void PegaseDamaged()
    {
        pegaseManager.Play("PegaseDamaged");
    }
}
