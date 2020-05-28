using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    [Header("Spawner Configuration")]
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private GameObject spawnCloud = null;

    public Collider2D spawnZone = null;

    [Space] public float spawnRadius = 0;
    public float timeBtwSpawn = 0;
    public float enemySpawnLimit = 0;

    private bool spawnInProgress;

    [HideInInspector] public bool spawnActivate;                    //bool for animation

    [Space]
    public GameObject[] enemiesToSpawn;
    
    [Space] public int enemiesDead; 
    public List<GameObject> enemiesSpawned;

    GameElementsHealthSystem spawnerHealthSystem;

    [HideInInspector]
    public bool spawnEnable = true;
    bool l_spawnEnable = true;
    AreaManager linkedAreaManager;
    #endregion

    void Start()
    {
        if (GetComponentInParent<AreaManager>() != null)
        {
            linkedAreaManager = GetComponentInParent<AreaManager>();
            linkedAreaManager.allEnemySpawners.Add(this);
            spawnEnable = false;
            l_spawnEnable = false;
            spawnZone = linkedAreaManager.freeZoneCollider;
        }
        else
        {
            StartCoroutine(FastEnemySpawn());
        }

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
                        enemiesDead++;
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

            if(linkedAreaManager != null)
            {
                for (int i = 0; i < enemiesSpawned.Count; i++)
                {
                    linkedAreaManager.allEnemiesToKill.Remove(enemiesSpawned[i]);
                    Instantiate(EnemyManager.Instance.cloud, enemiesSpawned[i].transform.position, Quaternion.identity);
                    Destroy(enemiesSpawned[i]);
                }
            }

            enemiesSpawned = new List<GameObject>();
        }
    }

    private void OnDestroy()
    {
        if(linkedAreaManager != null)
        {
            linkedAreaManager.allEnemySpawners.Remove(this);

            foreach (GameObject enemy in enemiesSpawned)
            {
                linkedAreaManager.allOrphanEnemies.Add(enemy);
            }
        }
    }

    public void KillAllEnnemies()
    {
        if (enemiesSpawned.Count > 0)
        {
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                enemiesSpawned[i].gameObject.GetComponent<EnemyHealthSystem>().currentHp = 0;
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

        if(spawnZone != null)
        {
            spawnPos = spawnZone.ClosestPoint(spawnPos);
        }
        

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
            if(linkedAreaManager != null)
            {
                linkedAreaManager.allEnemiesToKill.Add(enemyWhoSpawn);
            }            
        }

        spawnInProgress = false;
        spawnActivate = false;
    }

    public IEnumerator FastEnemySpawn()
    {
        spawnInProgress = true;
        spawnActivate = true;

        Vector2 spawnPos = spawnPoint.transform.position;
        spawnPos += Random.insideUnitCircle * spawnRadius;

        if (spawnZone != null)
        {
            spawnPos = spawnZone.ClosestPoint(spawnPos);
        }


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
            if (linkedAreaManager != null)
            {
                linkedAreaManager.allEnemiesToKill.Add(enemyWhoSpawn);
            }
        }

        spawnInProgress = false;
        spawnActivate = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
