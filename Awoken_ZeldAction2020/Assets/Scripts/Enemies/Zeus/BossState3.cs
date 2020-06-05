using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState3 : MonoBehaviour
{
    #region Pattern1

    [Space(30)]
    [Header("PATTERN 1 - Spawner")]

    public int ennemisToKillToOpenGate;

    [SerializeField] private float spawnRadius = 0;
    [SerializeField] private float timeBtwSpawn = 0;
    [SerializeField] private float enemySpawnLimit = 0;

    [Space] public GameObject[] enemiesToSpawn;

    #endregion

    BossState2Bis bossState2BisScript;

    [HideInInspector] public bool CounterIsActivate = false;
    [HideInInspector] public bool spawnerExist;
    [HideInInspector] public bool ZeusTp;

    void Start()
    {
        bossState2BisScript = GetComponent<BossState2Bis>();       
    }

    private void Update()
    {
        ResetSpawner();
    }

    void ResetSpawner()
    {
        if (BossManager.Instance.s3_Pattern1)
        {
            if (!spawnerExist)
            {
                spawnerExist = true;

                bossState2BisScript.spawnerA.GetComponent<EnemySpawner>().spawnEnable = true;
                bossState2BisScript.spawnerA.GetComponent<EnemySpawner>().spawnRadius = spawnRadius;
                bossState2BisScript.spawnerA.GetComponent<EnemySpawner>().timeBtwSpawn = timeBtwSpawn;
                bossState2BisScript.spawnerA.GetComponent<EnemySpawner>().enemySpawnLimit = enemySpawnLimit;
                bossState2BisScript.spawnerA.GetComponent<EnemySpawner>().enemiesToSpawn = enemiesToSpawn;
                bossState2BisScript.spawnerA.GetComponent<EnemySpawner>().enemiesDead = 0;

                bossState2BisScript.spawnerB.GetComponent<EnemySpawner>().spawnEnable = true;
                bossState2BisScript.spawnerB.GetComponent<EnemySpawner>().spawnRadius = spawnRadius;
                bossState2BisScript.spawnerB.GetComponent<EnemySpawner>().timeBtwSpawn = timeBtwSpawn;
                bossState2BisScript.spawnerB.GetComponent<EnemySpawner>().enemySpawnLimit = enemySpawnLimit;
                bossState2BisScript.spawnerB.GetComponent<EnemySpawner>().enemiesToSpawn = enemiesToSpawn;
                bossState2BisScript.spawnerB.GetComponent<EnemySpawner>().enemiesDead = 0;

                CounterIsActivate = true;
            }
        }
    }
}
