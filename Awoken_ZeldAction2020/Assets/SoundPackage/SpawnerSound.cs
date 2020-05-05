using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du Spawner
/// </summary>

public class SpawnerSound : MonoBehaviour
{
    private PrefabSoundManager spawnerManager;
    private EnemySpawner spawner;
    private EnemyHealthSystem spawnerHealth;

    private bool l_spawnActivate;

    // Start is called before the first frame update
    void Start()
    {
        spawnerManager = GetComponent<PrefabSoundManager>();
        spawner = GetComponentInParent<EnemySpawner>();
        spawnerHealth = GetComponentInParent<EnemyHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();

        if(l_spawnActivate != spawner.spawnActivate)
        {
            if(spawner.spawnActivate == true)
            {
                Spawned();
            }
            l_spawnActivate = spawner.spawnActivate;
        }
    }

    void Death()
    {
        if(spawnerHealth.currentHp <= 0)
        {
            spawnerManager.PlayOnlyOnce("SpawnerDeath");
        }
        else
        {
            spawnerManager.Stop("SpawnerDeath");
        }
    }

    void Spawned()
    {
        spawnerManager.Play("Spawned");
    }
}
