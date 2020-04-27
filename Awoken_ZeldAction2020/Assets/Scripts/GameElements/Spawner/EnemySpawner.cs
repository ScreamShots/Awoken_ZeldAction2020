using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [Header("Spawner Configuration")]
    [SerializeField] private Transform spawnPoint;

    [Space] [SerializeField] private float spawnRadius = 0;
    [SerializeField] private float timeBtwSpawn = 0;
    [SerializeField] private float enemySpawnLimit = 0;

    private bool spawnInProgress;

    [HideInInspector] public bool spawnActivate;                    //bool for animation

    [Space]
    public GameObject[] enemiesToSpawn;
    [Space] public List<GameObject> enemiesSpawned;

    EnemyHealthSystem enemyHealthScript;
    #endregion

    void Start()
    {
        enemyHealthScript = GetComponent<EnemyHealthSystem>();
    }

    void Update()
    {
        if (!enemyHealthScript.corouDeathPlay)
        {
            if (enemiesSpawned.Count > 0)
            {
                for (int i = 0; i < enemiesSpawned.Count; i++)
                {
                    if (enemiesSpawned[i].gameObject == null)
                    {
                        enemiesSpawned.Remove(enemiesSpawned[i]);
                    }
                }
            }

            if (enemiesSpawned.Count < enemySpawnLimit)
            {
                if (!spawnInProgress)
                {
                    StartCoroutine(SpawnEnemy());
                }
            }
        }     
    }

    IEnumerator SpawnEnemy()
    {
        spawnInProgress = true;

        yield return new WaitForSeconds(timeBtwSpawn - 0.2f);
        spawnActivate = true;

        yield return new WaitForSeconds(0.2f);

        Vector2 spawnPos = spawnPoint.transform.position;
        spawnPos += Random.insideUnitCircle * spawnRadius;

        GameObject enemyWhoSpawn = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPos, Quaternion.identity);
        enemiesSpawned.Add(enemyWhoSpawn);

        spawnInProgress = false;
        spawnActivate = false;
    }
}
