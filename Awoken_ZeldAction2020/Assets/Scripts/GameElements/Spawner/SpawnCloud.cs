using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script destroy game object after animation
/// </summary>

public class SpawnCloud : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
