using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather animations of the spawner
/// </summary>

public class SpawnerAnimator : MonoBehaviour
{
    Animator spawnerAnim;
    EnemySpawner enemySpawnerScript;

    private void Start()
    {
        spawnerAnim = GetComponent<Animator>();
        enemySpawnerScript = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        spawnerAnim.SetBool("SpawnerActivate", enemySpawnerScript.spawnActivate);
    }
}
