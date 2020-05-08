using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use for emit particle on foot of Player
/// </summary>

public class FootstepsParticles : MonoBehaviour
{
    #region Variables
    
    public ParticleSystem footParticles;

    PlayerMovement playerMovementScript;

    #endregion

    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!playerMovementScript.isRunning)
        {
            CreateFootParticle();
        }
    }

    void CreateFootParticle()
    {
        footParticles.Play();
    }
}
