using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState3 : MonoBehaviour
{
    #region Pattern1
    [HideInInspector] public bool pattern1IsRunning;

    [Header("Pattern1")] public Transform secretRoomArena;
    public Transform spawnerPlace;
    public Transform spawnerPlace2;
    public GameObject spawner;
    [HideInInspector] public GameObject spawnerA;
    [HideInInspector] public GameObject spawnerB;
    public float timeBeforePlaceSpawner;
    private float time;

    [Space]
    [Header("Stats")]
    public int ennemisToKillToOpenGate;

    [Space] [SerializeField] private float spawnRadius;
    [SerializeField] private float timeBtwSpawn;
    [SerializeField] private float enemySpawnLimit;

    [Space] public GameObject[] enemiesToSpawn;

    [HideInInspector] public bool spawnerExist;
    #endregion

    [HideInInspector] public bool ZeusTp;
    [HideInInspector] public bool ZeusIsTirred;
    private bool CoroutinePlayOnce;

    void Start()
    {
        time = timeBeforePlaceSpawner;
        
        spawner.GetComponent<EnemySpawner>().spawnRadius = spawnRadius;
        spawner.GetComponent<EnemySpawner>().timeBtwSpawn = timeBtwSpawn;
        spawner.GetComponent<EnemySpawner>().enemySpawnLimit = enemySpawnLimit;
    }

    private void Update()
    {
        AttackState3();
        Move();
    }

    void Move()
    {
        if (BossManager.Instance.s3_Pattern1)
        {
            if (!CoroutinePlayOnce)
            {
                CoroutinePlayOnce = true;
                StartCoroutine(ZeusCanTpSecretRoom());
            }
        }
    }

    void AttackState3()
    {
        if (BossManager.Instance.s3_Pattern1)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                if (!spawnerExist)
                {
                    spawnerExist = true;
                    Pattern1();                                 //Instantiate a spawner to a position
                }
            }
        }
    }

    void Pattern1()
    {
        spawnerA = Instantiate(spawner, spawnerPlace.position, spawner.transform.rotation);
        spawnerB = Instantiate(spawner, spawnerPlace2.position, spawner.transform.rotation);
    }

    IEnumerator ZeusCanTpSecretRoom()
    {
        ZeusTp = true;
        yield return new WaitForSeconds(0.3f);
        transform.position = secretRoomArena.position;
        ZeusTp = false;
        yield return new WaitForSeconds(0.5f);
        ZeusIsTirred = true;
    }
}
