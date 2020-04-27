using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather animations relative to invulnerable spawner
/// </summary>

public class SpawnerInvulnerableAnimator : MonoBehaviour
{
    Animator spawnerInvulnerableAnim;
    EnemySpawner enemySpawnerScript;

    private void Start()
    {
        spawnerInvulnerableAnim = GetComponent<Animator>();
        enemySpawnerScript = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        spawnerInvulnerableAnim.SetBool("SpawnerActivate", enemySpawnerScript.spawnActivate);
    }
}
