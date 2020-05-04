using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script permet de jouer le son de destruction des tourelles
/// </summary>
public class SoundDeathTurret : MonoBehaviour
{

    void Awake()
    {
        SoundManager.Instance.Play("TurretDeath");
    }
}
