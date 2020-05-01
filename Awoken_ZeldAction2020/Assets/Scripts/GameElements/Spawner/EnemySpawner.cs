using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [Header("Spawner Configuration")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject spawnCloud;

    [Space] [SerializeField] private float spawnRadius = 0;
    [SerializeField] private float timeBtwSpawn = 0;
    [SerializeField] private float enemySpawnLimit = 0;

    private bool spawnInProgress;

    [HideInInspector] public bool spawnActivate;                    //bool for animation

    [Space]
    public GameObject[] enemiesToSpawn;
    [Space] public List<GameObject> enemiesSpawned;

    GameElementsHealthSystem enemyHealthScript;
    TurretDetectionZone detectionZoneScript;
    #endregion

    void Start()
    {
        enemyHealthScript = GetComponent<GameElementsHealthSystem>();
        detectionZoneScript = GetComponentInChildren<TurretDetectionZone>();
    }

    void Update()
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

        if (detectionZoneScript.playerInZone)                       //if Player is in detection zone = spawn enemy
        {
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

        yield return new WaitForSeconds(timeBtwSpawn - 0.3f);
        spawnActivate = true;

        Vector2 spawnPos = spawnPoint.transform.position;
        spawnPos += Random.insideUnitCircle * spawnRadius;

        yield return new WaitForSeconds(0.2f);

        Instantiate(spawnCloud, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        GameObject enemyWhoSpawn = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPos, Quaternion.identity);
        enemiesSpawned.Add(enemyWhoSpawn);

        spawnInProgress = false;
        spawnActivate = false;
    }
}
