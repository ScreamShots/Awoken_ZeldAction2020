using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [Header("Spawner Configuration")]
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private GameObject spawnCloud = null;

    [Space] public float spawnRadius = 0;
    public float timeBtwSpawn = 0;
    public float enemySpawnLimit = 0;

    private bool spawnInProgress;

    [HideInInspector] public bool spawnActivate;                    //bool for animation

    [Space]
    public GameObject[] enemiesToSpawn;
    [Space] public List<GameObject> enemiesSpawned;

    GameElementsHealthSystem spawnerHealthSystem;

    [HideInInspector]
    public bool spawnEnable = true;
    bool l_spawnEnable = true;
    #endregion
    private void Awake()
    {
        if (GetComponentInParent<AreaManager>() != null)
        {
            GetComponentInParent<AreaManager>().allEnemySpawners.Add(this);
            spawnEnable = false;
            l_spawnEnable = false;
        }
    }

    void Start()
    {
        spawnerHealthSystem = GetComponent<GameElementsHealthSystem>();

    }

    void Update()
    {
        if (spawnEnable)
        {
            if (spawnEnable != l_spawnEnable)
            {
                l_spawnEnable = spawnEnable;
            }

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
        else if (!spawnEnable && spawnEnable != l_spawnEnable)
        {
            l_spawnEnable = spawnEnable;

            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                Instantiate(EnemyManager.Instance.cloud, enemiesSpawned[i].transform.position, Quaternion.identity);
                Destroy(enemiesSpawned[i]);
            }

            enemiesSpawned = new List<GameObject>();
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

        if (spawnEnable)
        {
            Instantiate(spawnCloud, spawnPos, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.1f);

        if (spawnEnable)
        {
            GameObject enemyWhoSpawn = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPos, Quaternion.identity);
            enemiesSpawned.Add(enemyWhoSpawn);
        }

        spawnInProgress = false;
        spawnActivate = false;
    }
}
