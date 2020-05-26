using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class AreaManager : MonoBehaviour
{
    [HideInInspector]
    public CinemachineVirtualCamera thisAreaCam;

    [SerializeField]
    private GameObject[] allTransitionZone = null;
    [SerializeField]
    private GameObject[] allLinkedBlockers = null;

    [SerializeField]
    private Transform pointOfRespawn = null;
    
    public Collider2D freeZoneCollider;

    [SerializeField]
    bool mustKillAllEnemies = false;
    [HideInInspector]
    public bool allEnemyAreDead;
    [SerializeField]
    bool dungeonRoom = false;
    [SerializeField]
    public bool canSpawnEnemies = true;

    [HideInInspector]
    public List<SpawnPlateform> allSpawnPlateforms =  new List<SpawnPlateform>();
    [HideInInspector]
    public List<EnemySpawner> allEnemySpawners = new List<EnemySpawner>();
    [HideInInspector]
    public List<GameObject> allEnemiesToKill = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> allTurrets = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> allOrphanEnemies = new List<GameObject>();

    bool areaLoaded;
   

    private void Awake()
    {
        thisAreaCam = GetComponentInChildren<CinemachineVirtualCamera>();
        if (!dungeonRoom)
        {
            thisAreaCam.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<Collider2D>();
            thisAreaCam.gameObject.GetComponent<CinemachineConfiner>().InvalidatePathCache();
            thisAreaCam.Follow = PlayerManager.Instance.gameObject.transform;
        }
        thisAreaCam.Priority = 0;
        thisAreaCam.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(areaLoaded && !allEnemyAreDead)
        {
            allEnemiesToKill = allEnemiesToKill.Where(item => item != null).ToList();

            if(allEnemiesToKill.Count == 0)
            {
                if (mustKillAllEnemies)
                {
                    foreach (GameObject blocker in allLinkedBlockers)
                    {
                        blocker.layer = LayerMask.NameToLayer("EnemyBlocker");
                    }
                    foreach (GameObject transZone in allTransitionZone)
                    {
                        transZone.SetActive(true);
                    }
                }                

                allEnemyAreDead = true;
            }
        }
    }

    public IEnumerator InitializeFirstCam()
    {
        thisAreaCam.gameObject.SetActive(true);
        thisAreaCam.Priority = 1;
        PlayerManager.Instance.transform.position = pointOfRespawn.position;
        yield return new WaitForEndOfFrame();
        LoadArea();
    }

    public void ActivateCam()
    {
        thisAreaCam.gameObject.SetActive(true);
        thisAreaCam.Priority = 1;
        LvlManager.Instance.currentArea = this;
    }

    public void LoadArea()
    {
        if (mustKillAllEnemies && !allEnemyAreDead && allSpawnPlateforms.Count > 0)
        {
            foreach(GameObject blocker in allLinkedBlockers)
            {
                blocker.layer = LayerMask.NameToLayer("Default");
            }
            foreach(GameObject transZone in allTransitionZone)
            {
                transZone.SetActive(false);
            }
            foreach (EnemySpawner spawner in allEnemySpawners)
            {
                allEnemiesToKill.Add(spawner.gameObject);
            }
        }

        foreach (GameObject turret in allTurrets)
        {
            if(turret != null)
            {
                turret.GetComponent<TurretShoot>().isActivated = true;
            }
        }

        if (canSpawnEnemies)
        {
            foreach (SpawnPlateform spawnPlateform in allSpawnPlateforms)
            {
                spawnPlateform.SpawnEnemy();
            }
            foreach (EnemySpawner spawner in allEnemySpawners)
            {
                spawner.spawnEnable = true;
                StartCoroutine(spawner.FastEnemySpawn());
            }
        }
      

        areaLoaded = true;
    }

    public void UnLoadArea()
    {
        foreach (SpawnPlateform spawnPlateform in allSpawnPlateforms)
        {
            spawnPlateform.UnSpawnEnemy();
        }
        foreach (EnemySpawner spawner in allEnemySpawners)
        {
            spawner.spawnEnable = false;
        }
        foreach (GameObject blocker in allLinkedBlockers)
        {
            blocker.layer = LayerMask.NameToLayer("EnemyBlocker");
        }
        foreach (GameObject transZone in allTransitionZone)
        {
            transZone.SetActive(true);
        }
        foreach (GameObject turret in allTurrets)
        {
            if(turret != null)
            {
                turret.GetComponent<TurretShoot>().isActivated = false;
            }
        }
        foreach (GameObject orphanEnemy in allOrphanEnemies)
        {
            if(orphanEnemy != null)
            {
                Instantiate(EnemyManager.Instance.cloud, orphanEnemy.transform.position, Quaternion.identity);
                Destroy(orphanEnemy);
            }
        }

        allOrphanEnemies = new List<GameObject>();

        areaLoaded = false;

        thisAreaCam.gameObject.SetActive(false);
        thisAreaCam.Priority = 0;
    }
}
