using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevTools;

/// <summary>
/// Made by Antoine
/// This script gather movement of Pegase
/// </summary>

public class PegaseMovement : MonoBehaviour
{
    #region HideInInspector Var Statement

    //global

    public enum Direction { up, down, left, right }
    [HideInInspector]                                          
    public Direction watchDirection = Direction.down;

    [HideInInspector]
    public Rigidbody2D pegaseRgb;                             
    private GameObject player;

    float playerDistance;                                    

    [HideInInspector]
    public bool canMove = true;                                

    Vector2 direction;                                       

    //Phase1 - Random Mouvement

    Vector3 startPos;                                        

    float randomWalkTimer;                                 
    float stayTimer;                                         

    bool randomDirSet;                                        
    [HideInInspector]
    public bool isOnRandomMove;                                


    //Phase2 - Chase

    [HideInInspector]
    public bool playerDetected;                                
    #endregion

    #region Serialize Var Statement

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
    [Header("Phase2 - Teleport")]

    [SerializeField]
    [Min(0)]
    float playerDetectionDistance = 0;

    [Header("Stats")]

    [SerializeField]
    [Min(0)]
    float maxRadiusTeleport = 0;
    [SerializeField]
    [Min(0)]
    float minRadiusTeleport = 0;
    #endregion

    #region Tools
    [Header("DevTools")]                                        //variables for dev tools

    [SerializeField] private bool showRanges = false;
    private GameObject allRangesCircles;

    private bool areRangesDisplayed = false;
    #endregion

    private void OnValidate()                                   //do stuff when a value is change within the inspector
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

        pegaseRgb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        stayTimer = stayDuration;
    }

    private void Update()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;          

        if (playerDistance <= playerDetectionDistance)                                    
        {                                                      
            playerDetected = true;
        }
        else                                                                                  
        {
            playerDetected = false;
        }

        SetDirection();                                                                        

    }

    private void FixedUpdate()
    {
        if (canMove)                          
        {
            if (playerDetected)                                             
            {
                Teleport();
            }
            else                                                            //else it's the random movement behaviour that apply (the player is to far)
            {
                if (stayTimer > 0)                                              //if the timer is not at zero the enemy is in an interval pause moment of the phase 1 (not mooving)
                {
                    if (pegaseRgb.velocity != Vector2.zero)                         //if we are in this immmobile phase and the velocity is not sero we do so (so the enemy dont move in this phase)
                    {
                        pegaseRgb.velocity = Vector2.zero;
                    }
                    if (randomDirSet)                                               //reset some value proper to that state
                    {
                        randomDirSet = false;
                    }
                    stayTimer -= Time.deltaTime;
                    isOnRandomMove = false;
                }
                else                                                            //if the timer is at 0, we can launch a random movement moment handled by the method RandomMove() 
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
            pegaseRgb.velocity = direction * randomMovSpeed * Time.fixedDeltaTime;
            randomWalkTimer -= Time.fixedDeltaTime;
        }
        else
        {
            pegaseRgb.velocity = Vector2.zero;
            stayTimer = stayDuration;
        }
    }

    void Teleport()                                     
    {
        Vector3 randomPosition = Random.insideUnitCircle * (maxRadiusTeleport - minRadiusTeleport);
        transform.position = player.transform.position + randomPosition.normalized * minRadiusTeleport + randomPosition;

        startPos = transform.position;
        RefreshRangeCircles();
        allRangesCircles.SetActive(true);
    }

    void SetDirection()                                 //method setting the direction for the animator
    {
        if (!playerDetected)                                                //this part work if we are in phase 1
        {
            if (pegaseRgb.velocity.x != 0 || pegaseRgb.velocity.y != 0)
            {
                if (Mathf.Abs(pegaseRgb.velocity.x) > Mathf.Abs(pegaseRgb.velocity.y))
                {
                    if (pegaseRgb.velocity.x > 0)
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
                    if (pegaseRgb.velocity.y > 0)
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
        else if (playerDetected)                                            //this part work if the enemy is charging (a bit different to avoid the enemy to change animation every frma due to the sinusoidal movement going up and down very fast)
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

    void DrawRangeCircles()                 //function that draw 2 circle, one for eache range the poulion has (attack and chase)
    {
        DestroyImmediate(allRangesCircles);

        allRangesCircles = new GameObject { name = "All Range Circles" };
        allRangesCircles.transform.parent = transform;
        allRangesCircles.transform.localPosition = new Vector3(0, 0, 0);

        var randomMoveCircle = new GameObject { name = "RandomMov MaxRange Circle" };
        randomMoveCircle.transform.parent = allRangesCircles.transform;

        var chaseRangeDisplay = new GameObject { name = "Detection Circle" };
        chaseRangeDisplay.transform.parent = allRangesCircles.transform;

        randomMoveCircle.DrawCircle(maxRandomWalkDistance, 0.02f, 50, Color.green, true, startPos);
        chaseRangeDisplay.DrawCircle(playerDetectionDistance, 0.02f, 50, Color.yellow, false);

        chaseRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);

        allRangesCircles.SetActive(false);
    }

    [ContextMenu("Refresh Range Circles")]
    void RefreshRangeCircles()          //function that refresh the range circles by calling this function from the inspector (right click on the name of the script)
    {
        if (allRangesCircles != null)
        {
            DestroyImmediate(allRangesCircles);
        }
        DrawRangeCircles();
        showRanges = false;
    }
}
