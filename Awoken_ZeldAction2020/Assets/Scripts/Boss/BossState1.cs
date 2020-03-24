using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState1 : MonoBehaviour
{
    #region Movement
    public Transform middleArena;

    public Transform throneArena;

    #endregion

    #region Attack
    public float timeBtwLightning;
    private float timeLeft;

    #endregion

    private void Start()
    {
        timeLeft = timeBtwLightning;
    }

    private void Update()
    {
        //AttackState1();
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

    /*void AttackState1()
    {
        if (BossManager.Instance.s1_Pattern1)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {

            }
        }
    }*/
}
