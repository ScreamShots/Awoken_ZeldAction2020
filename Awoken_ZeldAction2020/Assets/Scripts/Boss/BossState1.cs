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

    public GameObject Lightning;
    private GameObject player;

    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        timeLeft = timeBtwLightning;
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
        }
        else if (BossManager.Instance.s1_Pattern2)
        {

        }
    }

    void Pattern1()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft += timeBtwLightning;
            Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
        }
    }
}
