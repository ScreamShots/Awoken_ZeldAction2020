using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve the healt system of Boss and the state corresponding to his life
/// </summary>

public class BossManager : EnemyHealthSystem
{
    public static BossManager Instance;

    #region State 1
    BossState1 state1Script;

    [Space] [Header("Boss State 1")]
    public bool s1_Pattern1;
    public bool s1_Pattern2;
    
    [Space] [Header("Stats")]
    public float s1_pattern1_DurationTime;
    public float s1_pattern2_DurationTime;
    [Space]
    public float s1_pauseTimePattern1_2;
    [Space]
    public float pauseTimeBtwState1_2;

    private bool canPlayState1_pattern1;
    private bool canPlayState1_pattern2;
    #endregion

    #region State 2
    BossState2 state2Script;

    [Space] [Header("Boss State 2")]
    public bool s2_Pattern1;
    public bool s2_Pattern2;
    public bool s2_Pattern3;

    [Space] [Header("Stats")]
    public float s2_pattern1_DurationTime;
    public float s2_pattern2_DurationTime;
    public float s2_pattern3_DurationTime;
    [Space]
    public float s2_pauseTimePattern1_2;
    public float s2_pauseTimePattern3_1;

    private bool canPlayState2_pattern1;
    private bool canPlayState2_pattern2;
    private bool canPlayState2_pattern3;

    #endregion

    private bool canPlayNextState;
    private bool playCoroutine;

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

        state1Script = GetComponent<BossState1>();
        state2Script = GetComponent<BossState2>();


        canPlayState1_pattern1 = true;
        canPlayState2_pattern1 = true;
    }

    protected override void Update()
    {
        base.Update();
        
        if(currentHp <= 650 && currentHp > 500)             //state 1
        {
            State1();
        }
        else if(currentHp <= 500 && currentHp > 50)         //state 2
        {
            StopCoroutine(S1Pattern1()); StopCoroutine(S1Pattern2());                                   //for stopping the previous pattern

            s1_Pattern1 = false;
            s1_Pattern2 = false;

            if (!playCoroutine)
            {
                playCoroutine = true;
                StartCoroutine(waitBeforeStartNextState());
            }
            if (canPlayNextState)
            {
                State2();
            }
        }
        else if(currentHp <= 50 && currentHp > 0)           //state 3
        {
            StopCoroutine(S2Pattern1()); StopCoroutine(S2Pattern2()); StopCoroutine(S2Pattern3());      //for stopping the previous pattern

            s2_Pattern1 = false;
            s2_Pattern2 = false;
            s2_Pattern3 = false;
        }
    }

    #region State1
    void State1()
    {
        if (canPlayState1_pattern1 & !state1Script.pattern2IsRunning)
        {
            StartCoroutine(S1Pattern1());
        }
        else if (canPlayState1_pattern2)
        {
            s1_Pattern1 = false;

            if (!state1Script.pattern1IsRunning)
            {
                StartCoroutine(S1Pattern2());
            }
        }
    }

    IEnumerator S1Pattern1()
    {
        canPlayState1_pattern1 = false;

        s1_Pattern1 = true;
        canTakeDmg = false;
        s1_Pattern2 = false;

        yield return new WaitForSeconds(s1_pattern1_DurationTime);

        if(s1_pauseTimePattern1_2 > 0)
        {
            s1_Pattern1 = false;
            yield return new WaitForSeconds(s1_pauseTimePattern1_2);
            canPlayState1_pattern2 = true;
        }
        else
        {
            canPlayState1_pattern2 = true;
        }
    }

    IEnumerator S1Pattern2()
    {
        canPlayState1_pattern2 = false;

        s1_Pattern1 = false;
        canTakeDmg = true;
        s1_Pattern2 = true;

        yield return new WaitForSeconds(s1_pattern2_DurationTime);

        if (s1_pauseTimePattern1_2 > 0)
        {
            s1_Pattern2 = false;
            yield return new WaitForSeconds(s1_pauseTimePattern1_2);
            canPlayState1_pattern1 = true;
        }
        else
        {
            canPlayState1_pattern1 = true;
        }
    }
    #endregion

    #region State2
    void State2()
    {
        if (canPlayState2_pattern1)
        {
            s2_Pattern3 = false;

            if (!state2Script.pattern3IsRunning)
            {
                StartCoroutine(S2Pattern1());
            }
        }
        else if (canPlayState2_pattern2 & !state2Script.pattern1IsRunning)
        {
            StartCoroutine(S2Pattern2());
        }
        else if (canPlayState2_pattern3 & !state2Script.pattern2IsRunning)
        {
            StartCoroutine(S2Pattern3());
        }
    }

    IEnumerator S2Pattern1()
    {
        canPlayState2_pattern1 = false;

        s2_Pattern1 = true;
        s2_Pattern2 = false;
        s2_Pattern3 = false;
        canTakeDmg = true;

        yield return new WaitForSeconds(s2_pattern1_DurationTime);

        if (s2_pauseTimePattern1_2 > 0)
        {
            s2_Pattern1 = false;
            yield return new WaitForSeconds(s2_pauseTimePattern1_2);
            canPlayState2_pattern2 = true;
        }
        else
        {
            canPlayState2_pattern2 = true;
        }
    }

    IEnumerator S2Pattern2()
    {
        canPlayState2_pattern2 = false;

        s2_Pattern1 = false;
        s2_Pattern2 = true;
        s2_Pattern3 = false;
        canTakeDmg = true;

        yield return new WaitForSeconds(s2_pattern2_DurationTime);

        if (s2_pauseTimePattern1_2 > 0)
        {
            s2_Pattern2 = false;
            yield return new WaitForSeconds(s2_pauseTimePattern1_2);
            canPlayState2_pattern3 = true;
        }
        else
        {
            canPlayState2_pattern3 = true;
        }
    }

    IEnumerator S2Pattern3()
    {
        canPlayState2_pattern3 = false;

        s2_Pattern1 = false;
        s2_Pattern2 = false;
        s2_Pattern3 = true;
        canTakeDmg = false;

        yield return new WaitForSeconds(s2_pattern3_DurationTime);

        if (s2_pauseTimePattern3_1 > 0)
        {
            s2_Pattern3 = false;
            yield return new WaitForSeconds(s2_pauseTimePattern3_1);
            canPlayState2_pattern1 = true;
        }
        else
        {
            canPlayState2_pattern1 = true;
        }
    }
    #endregion

    IEnumerator waitBeforeStartNextState()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(pauseTimeBtwState1_2);
        canPlayNextState = true;
    }
}
