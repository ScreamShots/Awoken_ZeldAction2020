using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlateform : MonoBehaviour
{
    [SerializeField]
    public EnemyManager.Enemies assignedEnemy = EnemyManager.Enemies.Poulion;
    GameObject instanceEnemyToSpawn;
    
    bool enemyIsSpawned = false;
    [HideInInspector]
    public bool enemyIsDead = false;
    AreaManager linkedAreaManager;

    private void Start()
    {
        linkedAreaManager = GetComponentInParent<AreaManager>();
        linkedAreaManager.allSpawnPlateforms.Add(this);
    }

    public void SpawnEnemy()
    {
        if(!enemyIsDead && !enemyIsSpawned)
        {
            Instantiate(EnemyManager.Instance.cloud, transform.position, Quaternion.identity);
            instanceEnemyToSpawn = Instantiate(EnemyManager.Instance.enemiesToSpawn[assignedEnemy], transform.position, Quaternion.identity);
            instanceEnemyToSpawn.GetComponent<EnemyHealthSystem>().linkedSpawnPlateform = this;
            if(assignedEnemy == EnemyManager.Enemies.Pegase)
            {
                instanceEnemyToSpawn.GetComponent<PegaseMovement>().tpZone = linkedAreaManager.freeZoneCollider; 
            }
            linkedAreaManager.allEnemiesToKill.Add(instanceEnemyToSpawn);
            enemyIsSpawned = true;
        }        
    }

    public void UnSpawnEnemy()
    {
        if(!enemyIsDead && enemyIsSpawned)
        {
            linkedAreaManager.allEnemiesToKill.Remove(instanceEnemyToSpawn);
            Instantiate(EnemyManager.Instance.cloud, instanceEnemyToSpawn.transform.position, Quaternion.identity);
            Destroy(instanceEnemyToSpawn);
            enemyIsSpawned = false;
        }
    }

}
