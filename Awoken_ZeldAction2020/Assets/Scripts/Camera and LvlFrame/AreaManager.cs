using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AreaManager : MonoBehaviour
{
    [HideInInspector]
    public CinemachineVirtualCamera thisAreaCam;

    [SerializeField]
    private GameObject[] allTransitionZone = null;
    [SerializeField]
    private GameObject[] allLinkedBlockers = null;


    [SerializeField]
    bool mustKillAllEnemies = false;
    [HideInInspector]
    public bool allEnemyAreDead;
    int enemyDeathCounter = 0;
    [SerializeField]
    bool dungeonRoom = false;


    [HideInInspector]
    public List<SpawnPlateform> allSpawnPlateforms;
    [HideInInspector]
    public List<EnemySpawner> allEnemySpawners;

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

        allSpawnPlateforms = new List<SpawnPlateform>();
    }

    public IEnumerator InitializeFirstCam()
    {
        thisAreaCam.gameObject.SetActive(true);
        thisAreaCam.Priority = 1;
        yield return new WaitForEndOfFrame();
        LoadArea();
    }

    public void ActivateCam()
    {
        thisAreaCam.gameObject.SetActive(true);
        thisAreaCam.Priority = 1;
        LvlManager.Instance.currentArea = this;
    }

    public void DesactivateCam()
    {
        thisAreaCam.gameObject.SetActive(false);
        thisAreaCam.Priority = 0;
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
        }

        foreach(SpawnPlateform spawnPlateform in allSpawnPlateforms)
        {
            spawnPlateform.SpawnEnemy();
        }
        foreach(EnemySpawner spawner in allEnemySpawners)
        {
            spawner.spawnEnable = true;
        }
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
    }

    public void IncrementEnemyDeathCounter()
    {
        if (!allEnemyAreDead)
        {
            enemyDeathCounter++;
            if (enemyDeathCounter >= allSpawnPlateforms.Count)
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
}
