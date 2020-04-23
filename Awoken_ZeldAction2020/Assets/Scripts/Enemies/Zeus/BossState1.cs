using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather attacks relative to state 1 of Zeus
/// </summary>

public class BossState1 : MonoBehaviour
{
    #region Pattern1
    [HideInInspector ]public bool pattern1IsRunning;

    [Header("Pattern1")] public Transform throneArena;

    [Space]
    public float timeBtwLightning;
    private float timeLeft;

    [HideInInspector] public bool animThunder;

    public GameObject Lightning;
    private GameObject newLightning;
    #endregion

    #region Pattern2
    [HideInInspector] public bool pattern2IsRunning;

    [Header("Pattern2")] public Transform middleArena; public Transform shockWaveSpawn;

    [Space]
    public float timeBtwShockWave;                              //need to be superior of alive time of ShockWave
    private float timeLeft2;

    [HideInInspector] public bool animShockWave;

    public GameObject ShockWave;
    private GameObject newShockWave;
    #endregion

    private GameObject player;

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        timeLeft = 1f;
        timeLeft2 = 1f;
    }

    private void Update()
    {
        AttackState1();
        Move();
        CheckPatternRunning();
    }

    void CheckPatternRunning()
    {
        if (newLightning != null)
        {
            pattern1IsRunning = true;
        }
        else
        {
            pattern1IsRunning = false;
        }

        if (newShockWave != null)
        {
            pattern2IsRunning = true;
        }
        else
        {
            pattern2IsRunning = false;
        }
    }

    void Move()
    {
        if (BossManager.Instance.s1_Pattern1)
        {
            transform.position = throneArena.position;
        }
        else if (BossManager.Instance.s1_Pattern2)
        {
            transform.position = middleArena.position;
        }
    }

    void AttackState1()
    {
        if (BossManager.Instance.s1_Pattern1)
        {
            Pattern1();                                                                                 //Instantiate the lightning 
            animShockWave = false;
        }
        else if (BossManager.Instance.s1_Pattern2)                                                    
        {
            animThunder = false;
            Pattern2();                                                                                 //Instantiate the shock wave
        }
    }

    void Pattern1()
    {
        timeLeft -= Time.deltaTime;
        animThunder = false;

        if (timeLeft <= 0)
        {
            animThunder = true;
            timeLeft += timeBtwLightning;
            GameObject lightningInstance = Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
            newLightning = lightningInstance;
        }
    }

    void Pattern2()
    {
        timeLeft2 -= Time.deltaTime;
        animShockWave = false;

        if(timeLeft2 <= 0.5 && timeLeft2 > 0.4)                         //Time for play the animation of Zeus
        {
            animShockWave = true;
        }

        if (timeLeft2 <= 0)
        {
            timeLeft2 += timeBtwShockWave;
            GameObject shockWaveInstance = Instantiate(ShockWave, shockWaveSpawn.position, ShockWave.transform.rotation);
            newShockWave = shockWaveInstance;
        }
    }
}
