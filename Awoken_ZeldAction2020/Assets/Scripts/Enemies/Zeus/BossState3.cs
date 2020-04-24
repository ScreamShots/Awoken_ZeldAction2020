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

    [Space]
    [Header("Stats")]
    [SerializeField] private float spawnRadius;
    [SerializeField] private float timeBtwSpawn;
    [SerializeField] private float enemySpawnLimit;

    [Space] public GameObject[] enemiesToSpawn;

    private bool spawnerExist;
    #endregion

    private void Update()
    {
        AttackState3();
        Move();
    }

    void Move()
    {
        if (BossManager.Instance.s3_Pattern1)
        {
            transform.position = secretRoomArena.position;
        }
    }

    void AttackState3()
    {
        if (BossManager.Instance.s3_Pattern1)
        {
            if (!spawnerExist)
            {
                spawnerExist = true;
                Pattern1();                                 //Instantiate a spawner to a position
            }
        }
    }

    void Pattern1()
    {
        Instantiate(spawner, spawnerPlace.position, spawner.transform.rotation);
        Instantiate(spawner, spawnerPlace2.position, spawner.transform.rotation);
    }
}
