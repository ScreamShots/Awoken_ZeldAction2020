using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState1 : MonoBehaviour
{
    #region Pattern1
    [Header("Pattern1")] public Transform throneArena;

    public float timeBtwLightning;
    private float timeLeft;

    [HideInInspector] public bool animThunder;

    public GameObject Lightning;
    #endregion

    #region Pattern2
    [Header("Pattern2")] public Transform middleArena;

    public float timeBtwShockWave;
    private float timeLeft2;

    [HideInInspector] public bool animShockWave;

    public GameObject ShockWave;
    #endregion

    private GameObject player;

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        timeLeft = timeBtwLightning;
        timeLeft2 = timeBtwShockWave;
    }

    private void Update()
    {
        AttackState1();
        Move();
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
            Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
        }
    }

    void Pattern2()
    {
        timeLeft2 -= Time.deltaTime;
        animShockWave = false;

        if(timeLeft2 <= 0.6 && timeLeft2 > 0.5)                         //Time for play the animation of Zeus
        {
            animShockWave = true;
        }

        if (timeLeft2 <= 0)
        {
            timeLeft2 += timeBtwShockWave;
            Instantiate(ShockWave, transform.position, ShockWave.transform.rotation);
        }
    }
}
