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


    [SerializeField]
    bool mustKillAllEnemies = false;
    [HideInInspector]
    public bool allEnemyAreDead;
    [SerializeField]
    bool dungeonRoom = false;

    [HideInInspector]
    public List<SpawnPlateform> allSpawnPlateforms;
    [HideInInspector]
    public List<EnemySpawner> allEnemySpawners;
    [HideInInspector]
    public List<GameObject> allEnemiesToKill;

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

        allSpawnPlateforms = new List<SpawnPlateform>();
    }

    private void Update()
    {
        if(areaLoaded && mustKillAllEnemies && !allEnemyAreDead)
        {
            allEnemiesToKill = allEnemiesToKill.Where(item => item != null).ToList();

            if(allEnemiesToKill.Count == 0)
            {
                foreach (GameObject blocker in allLinkedBlockers)
                {
                    blocker.layer = LayerMask.NameToLayer("EnemyBlocker");
                }
                foreach (GameObject transZone in allTransitionZone)
                {
                    transZone.SetActive(true);
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
            foreach(EnemySpawner spawner in allEnemySpawners)
            {
                allEnemiesToKill.Add(spawner.gameObject);
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

        areaLoaded = false;

        thisAreaCam.gameObject.SetActive(false);
        thisAreaCam.Priority = 0;
    }
}
