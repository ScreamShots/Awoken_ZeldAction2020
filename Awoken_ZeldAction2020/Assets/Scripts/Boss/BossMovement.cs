using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather movement of Boss according to the state active
/// </summary>

public class BossMovement : MonoBehaviour
{
    #region Variables
    public Transform middleArena;

    public Transform throneArena;

    #endregion

    private void FixedUpdate()
    {
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
}
