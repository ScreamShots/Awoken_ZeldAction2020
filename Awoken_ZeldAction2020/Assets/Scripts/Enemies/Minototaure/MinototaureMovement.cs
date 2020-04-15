﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevTools;

/// <summary>
/// Made by Antoine
/// This script gather movement of Minototaure
/// </summary>

public class MinototaureMovement : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public Rigidbody2D minototaureRgb;                              
    private GameObject player;

    //global
    public enum Direction { up, down, left, right }
    [HideInInspector]                                           //enum for animator direction
    public Direction watchDirection = Direction.down;

    Vector3 startPos;

    float playerDistance;
    Vector2 direction;

    [HideInInspector]
    public bool canMove = true;

    [HideInInspector]
    public bool playerInAttackRange;

    [HideInInspector]
    public bool playerDetected;

    //random Move
    float randomWalkTimer;                                      
    float stayTimer;                                       

    bool randomDirSet;                                        
    [HideInInspector]
    public bool isOnRandomMove;                                

    #endregion

    #region Inspector Settings
    //Phase1 - Random Mouvement
    [Header("Distances")]
    [Header("Phase1 - Random Mouvement")]

    [SerializeField]
    [Min(0)]
    float maxRandomWalkDistance = 0;

    [Header("Stats")]

    [SerializeField]
    [Min(0)]
    float randomMovSpeed = 0;

    [Header("Timers")]

    [SerializeField]
    [Min(0)]
    float randomWalkDuration = 0;
    [SerializeField]
    [Min(0)]
    float stayDuration = 0;

    //Phase2 - Chase

    [Header("Distances")]
    [Header("Phase2 - Chase")]

    [SerializeField]
    [Min(0)]
    float playerDetectionDistance = 0;

    [Header("Stats")]

    [SerializeField]
    [Min(0)]
    float chaseSpeed = 0;

    //Phase3 - Attack

    [Header("Distances")]
    [Header("Phase3 - Attack")]

    [SerializeField]
    [Min(0)]
    float attackDistance = 0;

    #endregion

    #region Tools
    [Header("DevTools")]                                        //variables for dev tools

    [SerializeField] private bool showRanges = false;
    private GameObject allRangesCircles;

    private bool areRangesDisplayed = false;
    #endregion

    private void OnValidate()                                   
    {
        #region Activate / Desactivate range circles on tick the var bool 
        if (showRanges == true && areRangesDisplayed == false)
        {
            if (allRangesCircles != null)
            {
                allRangesCircles.SetActive(true);
                areRangesDisplayed = true;
            }
            else
            {
                DrawRangeCircles();
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
        if (PlayerManager.Instance != null)
        {
            player = PlayerManager.Instance.gameObject;
        }

        minototaureRgb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        stayTimer = stayDuration;
    }

    private void Update()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;            //refresh the distance btwn enemy and player at every frame

        if (canMove)
        {
            if (playerDistance <= attackDistance)
            {
                if (!playerInAttackRange && canMove)                                                //change the bool saying that the enemy can attack (see attack class) + setting the move value to 0 once(stop previous move)
                {
                    minototaureRgb.velocity = Vector2.zero;
                }
                playerInAttackRange = true;
                playerDetected = true;
            }
            else if (playerDistance <= playerDetectionDistance)
            {
                playerInAttackRange = false;
                playerDetected = true;
            }
            else                                                                                    //else change bool saying the player is to far (for random Move behaviour)
            {
                playerInAttackRange = false;
                playerDetected = false;
            }
        }

        SetDirection();                                                                         //set the direction every frame for the animator

    }

    private void FixedUpdate()
    {
        Vector2 playerDirection = player.transform.position - transform.position;               //direction between enemy and player

        if (!playerInAttackRange && canMove)                                
        {
            if (playerDetected)                                             
            {
                minototaureRgb.velocity = playerDirection.normalized * chaseSpeed * Time.fixedDeltaTime;
            }
            else                                                            //else it's the random movement behaviour that apply (the player is to far)
            {
                if (stayTimer > 0)                                              
                {
                    if (minototaureRgb.velocity != Vector2.zero)                         //if we are in this immmobile phase and the velocity is not sero we do so (so the enemy dont move in this phase)
                    {
                        minototaureRgb.velocity = Vector2.zero;
                    }
                    if (randomDirSet)                                              
                    {
                        randomDirSet = false;
                    }
                    stayTimer -= Time.deltaTime;
                    isOnRandomMove = false;
                }
                else                                                            
                {
                    isOnRandomMove = true;
                    RandomMove();
                }
            }
        }
    }

    void RandomMove()
    {
        float distanceFromStartPos = (startPos - transform.position).magnitude;

        if (!randomDirSet)
        {
            if (distanceFromStartPos > maxRandomWalkDistance)
            {
                direction = startPos - transform.position;
                direction.Normalize();
            }
            else
            {
                direction = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                direction.Normalize();
            }
            randomWalkTimer = randomWalkDuration;
            randomDirSet = true;
        }

        if (randomWalkTimer > 0)
        {
            minototaureRgb.velocity = direction * randomMovSpeed * Time.fixedDeltaTime;
            randomWalkTimer -= Time.fixedDeltaTime;
        }
        else
        {
            minototaureRgb.velocity = Vector2.zero;
            stayTimer = stayDuration;
        }
    }

    void SetDirection()                                 //method setting the direction for the animator
    {
        if (!playerDetected)                                                //this part work if we are in phase 1
        {
            if (minototaureRgb.velocity.x != 0 || minototaureRgb.velocity.y != 0)
            {
                if (Mathf.Abs(minototaureRgb.velocity.x) > Mathf.Abs(minototaureRgb.velocity.y))
                {
                    if (minototaureRgb.velocity.x > 0)
                    {
                        watchDirection = Direction.right;
                    }
                    else
                    {
                        watchDirection = Direction.left;
                    }
                }
                else
                {
                    if (minototaureRgb.velocity.y > 0)
                    {
                        watchDirection = Direction.up;
                    }
                    else
                    {
                        watchDirection = Direction.down;
                    }
                }
            }
        }
        else if (playerDetected)                                            
        {
            Vector2 playerdirection = player.transform.position - transform.position;
            playerdirection.Normalize();

            if (Mathf.Abs(playerdirection.x) > Mathf.Abs(playerdirection.y))
            {
                if (playerdirection.x > 0)
                {
                    watchDirection = Direction.right;
                }
                else
                {
                    watchDirection = Direction.left;
                }
            }
            else
            {
                if (playerdirection.y > 0)
                {
                    watchDirection = Direction.up;
                }
                else
                {
                    watchDirection = Direction.down;
                }
            }
        }

    }

    void DrawRangeCircles()                 
    {
        DestroyImmediate(allRangesCircles);

        allRangesCircles = new GameObject { name = "All Range Circles" };
        allRangesCircles.transform.parent = transform;
        allRangesCircles.transform.localPosition = new Vector3(0, 0, 0);

        var randomMoveCircle = new GameObject { name = "RandomMov MaxRange Circle" };
        randomMoveCircle.transform.parent = allRangesCircles.transform;

        var chaseRangeDisplay = new GameObject { name = "Detection Circle" };
        chaseRangeDisplay.transform.parent = allRangesCircles.transform;

        var attackRangeDisplay = new GameObject { name = "Attack Circle" };
        attackRangeDisplay.transform.parent = allRangesCircles.transform;


        randomMoveCircle.DrawCircle(maxRandomWalkDistance, 0.02f, 50, Color.green, true, startPos);
        chaseRangeDisplay.DrawCircle(playerDetectionDistance, 0.02f, 50, Color.yellow, false);
        attackRangeDisplay.DrawCircle(attackDistance, 0.02f, 50, Color.red, false);

        attackRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);
        chaseRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);

        allRangesCircles.SetActive(false);
    }

    [ContextMenu("Refresh Range Circles")]
    void RefreshRangeCircles()          
    {
        if (allRangesCircles != null)
        {
            DestroyImmediate(allRangesCircles);
        }
        DrawRangeCircles();
        showRanges = false;
    }
}
