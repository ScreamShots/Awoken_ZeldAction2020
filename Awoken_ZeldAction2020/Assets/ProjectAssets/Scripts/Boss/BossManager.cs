using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve the healt system of Boss and the state corresponding to his life
/// </summary>

public class BossManager : BasicHealthSystem
{
    public static BossManager Instance;

    #region State 1
    [Space] [Header("Boss State 1")]
    public bool s1_Pattern1;
    public bool s1_Pattern2;
    
    public float s1_timeBtwPattern1And2;
    
    private bool canPlayState1;
    #endregion

    #region State 2
    [Space] [Header("Boss State 2")]
    public bool s2_Pattern1;
    public bool s2_Pattern2;
    public bool s2_Pattern3;

    public float s2_timeBtwPattern1And2;
    public float s2_timeBtwPattern3And1;

    private bool canPlayState2;

    #endregion

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    protected override void Start()
    {
        base.Start();

        canPlayState1 = true;
        canPlayState2 = true;
    }

    protected override void Update()
    {
        base.Update();
        
        if(currentHp <= 650 && currentHp > 500)             //state 1
        {
            Debug.Log("state1");

            if (canPlayState1)
            {
                StartCoroutine(State1());
            }
        }
        else if(currentHp <= 500 && currentHp > 50)         //state 2
        {
            Debug.Log("state2");

            StopCoroutine(State1());                        //for stopping the previous pattern
            s1_Pattern1 = false;
            s1_Pattern2 = false;

            if (canPlayState2)
            {
                StartCoroutine(State2());
            }
        }
        else if(currentHp <= 50 && currentHp > 0)           //state 3
        {
            Debug.Log("state3");

            StopCoroutine(State2());                        //for stopping the previous pattern
            s2_Pattern1 = false;
            s2_Pattern2 = false;
            s2_Pattern3 = false;
        }
    }

    IEnumerator State1()
    {
        canPlayState1 = false;

        s1_Pattern1 = true;
        s1_Pattern2 = false;

        yield return new WaitForSeconds(s1_timeBtwPattern1And2);
        s1_Pattern1 = false;
        s1_Pattern2 = true;

        yield return new WaitForSeconds(s1_timeBtwPattern1And2);
        canPlayState1 = true;
    }

    IEnumerator State2()
    {
        canPlayState2 = false;

        s2_Pattern1 = true;
        s2_Pattern2 = false;
        s2_Pattern3 = false;

        yield return new WaitForSeconds(s2_timeBtwPattern1And2);
        s2_Pattern1 = false;
        s2_Pattern2 = true;
        s2_Pattern3 = false;

        yield return new WaitForSeconds(s2_timeBtwPattern1And2);
        s2_Pattern1 = false;
        s2_Pattern2 = false;
        s2_Pattern3 = true;

        yield return new WaitForSeconds(s2_timeBtwPattern3And1);
        canPlayState2 = true;
    }
}
