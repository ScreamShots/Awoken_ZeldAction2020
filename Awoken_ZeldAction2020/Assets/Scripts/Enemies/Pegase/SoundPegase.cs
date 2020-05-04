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
            if(pegaseMovement.playerDetected == true)
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

    }

    void Death()
    {
        if (healthSystem.currentHp <= 0)
        {
            pegaseManager.PlayOnlyOnce("PegaseDeath");
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
}
