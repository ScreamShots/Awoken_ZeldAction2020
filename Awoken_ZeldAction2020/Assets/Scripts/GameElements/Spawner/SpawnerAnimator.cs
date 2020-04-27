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
    EnemyHealthSystem enemyHealthScript;

    private void Start()
    {
        spawnerAnim = GetComponent<Animator>();
        enemySpawnerScript = GetComponentInParent<EnemySpawner>();
        enemyHealthScript = GetComponentInParent<EnemyHealthSystem>();
    }

    private void Update()
    {
        spawnerAnim.SetBool("SpawnerActivate", enemySpawnerScript.spawnActivate);
        spawnerAnim.SetBool("Dead", enemyHealthScript.corouDeathPlay);
    }
}
