using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather differents attacks of the Boss depends on the State active 
/// </summary>

public class BossAttack : MonoBehaviour
{
    #region State 1
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
    }

    /*void AttackState1()
    {
        if (BossManager.Instance.state1_Pattern1)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft <= 0)
            {

            }
        }
    }*/
}
