using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [Header("Spawner Configuration")]
    [SerializeField] private float spawnRadius;
    [SerializeField] private float timeBtwSpawn;
    [SerializeField] private float enemySpawnLimit;

    private bool spawnInProgress;

    [Space]
    public GameObject[] enemiesToSpawn;
    [Space] public List<GameObject> enemiesSpawned;

    #endregion

    void Update()
    {
        if (enemiesSpawned.Count > 0)
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                if (enemiesSpawned[i].GetComponent<BasicHealthSystem>().currentHp <= 0)
                {
                    enemiesSpawned.Remove(enemiesSpawned[i]);
                }
            }
        }

        if(enemiesSpawned.Count < enemySpawnLimit)
        {
            if (!spawnInProgress)
            {
                StartCoroutine(SpawnEnemy());
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        spawnInProgress = true;

        yield return new WaitForSeconds(timeBtwSpawn);

        Vector2 spawnPos = transform.position;
        spawnPos += Random.insideUnitCircle * spawnRadius;

        GameObject enemyWhoSpawn = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPos, Quaternion.identity);
        enemiesSpawned.Add(enemyWhoSpawn);

        spawnInProgress = false;
    }
}
