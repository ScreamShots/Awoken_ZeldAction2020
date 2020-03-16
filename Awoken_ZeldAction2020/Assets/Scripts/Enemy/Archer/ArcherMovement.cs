﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevTools;

/// <summary>
/// Made by Antoine
/// This script involve movement of archer and facing direction 
/// </summary>

public class ArcherMovement : MonoBehaviour
{
    #region Variables
    [Header("Archer Speed")]

    [SerializeField] [Min(0)]
    private float chaseSpeed = 0;

    [SerializeField] [Min(0)]
    private float retreatSpeed = 0;

    [Header("Archer Movement")]
    public float retreatDistance;
    public float chaseDistance;
    public float attackDistance;

    private GameObject player;

    private Rigidbody2D rb;

    [Header("DevTools")]                                    //variables for dev tools

    [SerializeField] private bool showRanges = false;
    private GameObject allRangesCircles;

    private bool areRangesDisplayed = false;
    #endregion

    private void OnValidate()                                   //do stuff when a value is change within the inspector
    {
        #region Activate / Desactivate range circles on tick the var bool 
        if (showRanges == true && areRangesDisplayed == false)
        {
            if(allRangesCircles != null)
            {
                allRangesCircles.SetActive(true);
                areRangesDisplayed = true;
            }           
        }
        else if (showRanges == false && areRangesDisplayed == true)
        {
            if (allRangesCircles != null)
            {
                allRangesCircles.SetActive(false);
                areRangesDisplayed = false;
            }
        }

        #endregion
    }


    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        rb = gameObject.GetComponent<Rigidbody2D>();

        DrawRangeCircles();
    }

    private void FixedUpdate()
    {
        Move();  
    }

    void Move()
    {                                                
        float distance = Vector2.Distance(transform.position, player.transform.position);                                           //Calculate distance between archer && player


        Vector2 direction = (player.transform.position - transform.position).normalized;                                                //Calculate direction between archer && player


        if (distance <= chaseDistance && distance > attackDistance && !GetComponent<ArcherAttack>().archerIsAttacking)                   //Move to player if archer isn't attack
        {
            rb.velocity = direction * chaseSpeed * Time.fixedDeltaTime;
            GetComponent<ArcherAttack>().archerCanAttack = false;
        }
        else if (distance <= retreatDistance && !gameObject.GetComponent<ArcherAttack>().archerIsAttacking)                              //Escape from player if archer isn't attack
        {
            rb.velocity = direction * -retreatSpeed * Time.fixedDeltaTime;
            GetComponent<ArcherAttack>().archerCanAttack = false;
        }
        else if (distance < chaseDistance && distance > retreatDistance)                                                                //Stop at his position
        {
            rb.velocity = Vector2.zero;
            GetComponent<ArcherAttack>().archerCanAttack = true;
        }
        else if(distance > chaseDistance)
        {
            rb.velocity = Vector2.zero;
            GetComponent<ArcherAttack>().archerCanAttack = false;
        }
    }

    void DrawRangeCircles()
    {
        DestroyImmediate(allRangesCircles);

        allRangesCircles = new GameObject { name = "All Range Circles" };
        allRangesCircles.transform.parent = transform;
        allRangesCircles.transform.localPosition = new Vector3(0, 0, 0);

        var retreatRangeDisplay = new GameObject { name = "Retreat Circle" };
        retreatRangeDisplay.transform.parent = allRangesCircles.transform;        

        var chaseRangeDisplay = new GameObject { name = "Chase Circle" };
        chaseRangeDisplay.transform.parent = allRangesCircles.transform;        

        var attackRangeDisplay = new GameObject { name = "Chase Circle" };
        attackRangeDisplay.transform.parent = allRangesCircles.transform;
        

        retreatRangeDisplay.DrawCircle(retreatDistance, 0.02f, 50, Color.green);
        chaseRangeDisplay.DrawCircle(chaseDistance, 0.02f, 50, Color.yellow);
        attackRangeDisplay.DrawCircle(attackDistance, 0.02f, 50, Color.red);

        attackRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);
        chaseRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);
        retreatRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);

        allRangesCircles.SetActive(false);
    }                       //function that draw 3 circle, one for eache range the archer has (attack, chase and retreat)

    [ContextMenu("Refresh Range Circles")]
    void RefreshRangeCircles()
    {
        if(allRangesCircles != null)
        {
            DestroyImmediate(allRangesCircles);
        }        
        DrawRangeCircles();
        showRanges = false;
    }                   //function that refresh the range circles by calling this function from the inspector (right click on the name of the script)
}
