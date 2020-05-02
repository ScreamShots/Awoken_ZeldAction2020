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
    bool mustKillAllEnemies;
    int enemyDeathCounter = 0;


    [HideInInspector]
    public List<SpawnPlateform> allSpawnPlateforms;
    [HideInInspector]
    public List<EnemySpawner> allEnemySpawners;

    private void Awake()
    {
        thisAreaCam = GetComponentInChildren<CinemachineVirtualCamera>();
        thisAreaCam.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
        thisAreaCam.Follow = PlayerManager.Instance.gameObject.transform;
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
        if (mustKillAllEnemies)
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
            Debug.Log(spawnPlateform.assignedEnemy);
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
        if (mustKillAllEnemies)
        {
            enemyDeathCounter++;
            if (enemyDeathCounter >= allSpawnPlateforms.Count)
            {
                foreach (GameObject blocker in allLinkedBlockers)
                {
                    blocker.layer = LayerMask.NameToLayer("EnemyBlocker");
                }
                foreach (GameObject transZone in allTransitionZone)
                {
                    transZone.SetActive(true);
                }
                mustKillAllEnemies = false;
            }
        }
    }
}
