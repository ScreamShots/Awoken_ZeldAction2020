using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevTools;

/// <summary>
/// Made by Rémi Sécher based on Antoine Leroux work
/// This class handle the poulion movement through 3 phase (random walk, chase, attack)/// 
/// </summary>
public class PoulionMovementReworked : MonoBehaviour
{
    #region HideInInspector Var Statement

    //global

    public enum Direction { up, down, left, right }
    [HideInInspector]                                           //enum for animator direction
    public Direction watchDirection = Direction.down;

    [HideInInspector]
    public Rigidbody2D poulionRgb;                              //Requiered object or element for references
    private GameObject player;

    float playerDistance;                                       //distance btwn enemy and player

    [HideInInspector]
    public bool canMove = true;                                 //bool to know if the movement is handle by this class or override by the attack class
    
    Vector2 direction;                                          //move direction

    //Phase1 - Random Mouvement

    Vector3 startPos;                                           //spawn position the enemy can't stay too far while moving through random move

    float randomWalkTimer;                                      //timer to know the duration of a single move in the random move phase
    float stayTimer;                                            //timer to know the duration of interval time btwn two movement of the random move phase

    bool randomDirSet;                                          //use to know if the direction of the random move has already been set
    [HideInInspector]
    public bool isOnRandomMove;                                 //use to know if the enemy is currently moving throug random logic (and not imobile during an interval time)


    //Phase2 - Chase

    [HideInInspector]
    public bool playerDetected;                                 //use to know if the player is near enough to get detected and launch chase phase

    //Phase3 - Attack

    [HideInInspector]
    public bool playerInAttackRange;                            //use to know if the player is near enough to start an attack

    #endregion
    
    #region Serialize Var Statement

    //Phase1 - Random Mouvement
    [Header("Distances")] [Header("Phase1 - Random Mouvement")]

    [SerializeField] [Min(0)]
    float maxRandomWalkDistance = 0;

    [Header("Stats")]

    [SerializeField] [Min(0)]
    float randomMovSpeed = 0;
    
    [Header("Timers")]

    [SerializeField] [Min(0)]
    float randomWalkDuration = 0;
    [SerializeField] [Min(0)]
    float stayDuration = 0;

    //Phase2 - Chase

    [Header("Distances")] [Header("Phase2 - Chase")]

    [SerializeField] [Min(0)]
    float playerDetectionDistance = 0;

    [Header("Stats")]

    [SerializeField] [Min(0)]
    float chaseSpeed = 0;

    [Header("Sinusoidal Movement Properties")]

    [SerializeField] [Min(0)]
    float frequency = 0;
    [SerializeField] [Range(1, 10)]
    float minAmplitude = 0;
    [SerializeField] [Range(1, 10)]
    float maxAmplitude = 0;

    //Phase3 - Attack

    [Header("Distances")] [Header("Phase3 - Attack")]

    [SerializeField] [Min(0)]
    float attackDistance = 0;

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

        poulionRgb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        stayTimer = stayDuration;
    }

    private void Update()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;            //refresh the distance btwn enemy and player at every frame

        if (playerDistance <= attackDistance)                                                   //if player is near enough to start an attack 
        {                                                                                       
            if (!playerInAttackRange && canMove)                                                //change the bool saying that the enemy can attack (see attack class) + setting the move value to 0 once(stop previous move)
            {
                poulionRgb.velocity = Vector2.zero;
            }
            playerInAttackRange = true;
            playerDetected = false;
        }
        else if (playerDistance <= playerDetectionDistance)                                     //if the player is near enough to be chase (but not enough to get attacked)
        {
            playerInAttackRange = false;                                                        //change bool syaing so
            playerDetected = true;
        }
        else                                                                                    //else change bool saying the player is to far (for random Move behaviour)
        {
            playerInAttackRange = false;
            playerDetected = false;
        }

        SetDirection();                                                                         //set the direction every frame for the animator

    }

    private void FixedUpdate()
    {
        if (!playerInAttackRange && canMove)                                //test to know if it's this class that handle move or if it's override by attack class
        {
            if (playerDetected)                                             //if this class hold move management and player is detected apply Chase behaviour (phase 1)
            {
                Chase();
            }
            else                                                            //else it's the random movement behaviour that apply (the player is to far)
            {
                if (stayTimer > 0)                                              //if the timer is not at zero the enemy is in an interval pause moment of the phase 1 (not mooving)
                {
                    if(poulionRgb.velocity != Vector2.zero)                         //if we are in this immmobile phase and the velocity is not sero we do so (so the enemy dont move in this phase)
                    {
                        poulionRgb.velocity = Vector2.zero;
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
            poulionRgb.velocity = direction * randomMovSpeed * Time.fixedDeltaTime;
            randomWalkTimer -= Time.fixedDeltaTime;
        }
        else
        {
            poulionRgb.velocity = Vector2.zero;
            stayTimer = stayDuration;
        }
    }

    void Chase()                                        // method handling the chase phase (played every fixed update if the algorythm says we are in this phase)
    {
        Vector2 playerDirection = player.transform.position - transform.position;                                                                                //set a vector tower the player
        Vector2 sinRandomMove = Vector2.Perpendicular(playerDirection) * Mathf.Sin(Time.fixedTime * frequency) * Random.Range(minAmplitude, maxAmplitude);       //set a perpendicular vertor to the first one modify by a sinusoid
        direction = playerDirection + sinRandomMove;                                                                                                             //adding both of them to make the little shaking run of the enemy
        direction.Normalize();

        poulionRgb.velocity = direction * chaseSpeed * Time.fixedDeltaTime;                                                                 
    }

    void SetDirection()                                 //method setting the direction for the animator
    {
        if (!playerDetected)                                                //this part work if we are in phase 1
        {
            if (poulionRgb.velocity.x != 0 || poulionRgb.velocity.y != 0)
            {
                if (Mathf.Abs(poulionRgb.velocity.x) > Mathf.Abs(poulionRgb.velocity.y))
                {
                    if (poulionRgb.velocity.x > 0)
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
                    if (poulionRgb.velocity.y > 0)
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
