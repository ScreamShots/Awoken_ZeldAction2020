using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use to destroy blood particle after played
/// </summary>

public class DestroyBloodParticle : MonoBehaviour
{
    ParticleSystem particleSystemComponent;

    void Start()
    {
        particleSystemComponent = GetComponent<ParticleSystem>();

        Destroy(gameObject, particleSystemComponent.main.duration);
    }
}
