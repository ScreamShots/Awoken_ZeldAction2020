using System.Collections;
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

    //global

    [HideInInspector]
    public Rigidbody2D minototaureRgb;
    private GameObject player;
    MinototaureAttack minototaureAttackScript;
    EnemyHealthSystem minototaureHealthScript;

    public enum Direction { up, down, left, right, diagonalUpRight, diagonalUpLeft, diagonalDownRight, diagonalDownLeft }
    [HideInInspector]                                           
    public Direction watchDirection = Direction.down;

    Vector3 startPos;

    float playerDistance;
    Vector2 direction;

    //attack information

    [HideInInspector]
    public bool canMove = true;

    [HideInInspector]
    public bool playerInAttackRange;

    [HideInInspector]
    public bool playerDetected;

    [HideInInspector]
    public bool minototaureCooldown;

    //random Move
    float randomWalkTimer;                                      
    float stayTimer;                                       

    bool randomDirSet;                                        
    [HideInInspector]
    public bool isOnRandomMove;

    bool knockBack = false;
    EnemyKnockBackCaller knockBackCaller;

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

    //Phase2 - Chase PJ

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

    [Header("Stats")]
    [Header("Phase4 - Cooldown")]

    [SerializeField]
    [Min(0)]
    float cooldownSpeed = 0;
    #endregion

    #region Tools
    [Header("DevTools")]                                        //variables for dev tools

    [SerializeField] private bool showRanges = false;
    private GameObject allRangesCircles;

    private bool areRangesDisplayed = false;
    #endregion

    [HideInInspector] public bool playerIsAggro = false;

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
        minototaureAttackScript = GetComponent<MinototaureAttack>();
        minototaureHealthScript = GetComponent<EnemyHealthSystem>();
        knockBackCaller = GetComponent<EnemyKnockBackCaller>();

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
                if (!playerInAttackRange && canMove)                                            //change the bool saying that the enemy can attack (see attack class) + setting the move value to 0 once(stop previous move)
                {
                    if (!knockBack)
                    {
                        minototaureRgb.velocity = Vector2.zero;
                    }                    
                    isOnRandomMove = false;
                }
                playerInAttackRange = true;
                playerDetected = false;
            }
            else if (playerDistance <= playerDetectionDistance)
            {
                playerIsAggro = true;
                playerInAttackRange = false;
                playerDetected = true;
                if (!minototaureCooldown)
                {
                    minototaureHealthScript.canTakeDmg = true;
                }
                else if (minototaureCooldown)
                {
                    minototaureHealthScript.canTakeDmg = false;
                }
            }
            else                                                                                //else change bool saying the player is to far (for random Move behaviour)
            {
                playerIsAggro = false;
                if (!minototaureCooldown)
                {
                    minototaureHealthScript.canTakeDmg = true;
                    playerInAttackRange = false;
                    playerDetected = false;
                }
                else if (minototaureCooldown)
                {
                    minototaureHealthScript.canTakeDmg = false;
                    playerDetected = true;
                    playerInAttackRange = false;
                }
            }

            if (minototaureAttackScript.cooldownAttack)
            {
                if (playerInAttackRange)
                {
                    if (!knockBack)
                    {
                        minototaureRgb.velocity = Vector2.zero;
                    }                    
                }
                minototaureCooldown = true;                                                     //change bool saying Minototaure is on cooldown phase
            }
            else
            {
                minototaureCooldown = false;
            }
        }

        if (!minototaureAttackScript.isAttacking)
        {
            SetDirection();                     //set the direction every frame for the animator
        }                                                                       
    }

    private void FixedUpdate()
    {
        if (!knockBack)
        {
            Vector2 playerDirection = player.transform.position - transform.position;               //direction between enemy and player

            if (!playerInAttackRange && canMove)
            {
                if (playerDetected)                                                                 //if Player is detected, Minototaure rush on him with speed increase                                   
                {
                    minototaureRgb.velocity = playerDirection.normalized * chaseSpeed * Time.fixedDeltaTime;
                    isOnRandomMove = false;
                }
                else                                                                                //else it's the random movement behaviour that apply (the player is to far)
                {
                    if (stayTimer > 0)
                    {
                        if (minototaureRgb.velocity != Vector2.zero)                                //if we are in this immmobile phase and the velocity is not sero we do so (so the enemy dont move in this phase)
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

            if (minototaureCooldown && !playerInAttackRange)                                        //if Minotoaure is on cooldown phase, follow Player with slow speed
            {
                minototaureRgb.velocity = playerDirection.normalized * cooldownSpeed * Time.fixedDeltaTime;
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
        Vector2 playerdirection = player.transform.position - transform.position;
        playerdirection.Normalize();
        float playerAngle = Mathf.Atan2(playerdirection.y, playerdirection.x) * Mathf.Rad2Deg;

        if(playerAngle < 0)
        {
            playerAngle += 360;
        }

        if (!isOnRandomMove)
        {
            if (playerAngle > 22.5f && playerAngle < 67.5f)
            {
                watchDirection = Direction.diagonalUpRight;
            }
            else if (playerAngle > 67.5f && playerAngle < 112.5f)
            {
                watchDirection = Direction.up;
            }
            else if(playerAngle > 112.5f && playerAngle < 157.5f)
            {
                watchDirection = Direction.diagonalUpLeft;
            }
            else if(playerAngle > 157.5f && playerAngle < 202.5f)
            {
                watchDirection = Direction.left;
            }
            else if (playerAngle > 202.5f && playerAngle < 247.5f)
            {
                watchDirection = Direction.diagonalDownLeft;
            }
            else if (playerAngle > 247.5f && playerAngle < 292.5f)
            {
                watchDirection = Direction.down;
            }
            else if (playerAngle > 292.5f && playerAngle <337.5f)
            {
                watchDirection = Direction.diagonalDownRight;
            }
            else
            {
                watchDirection = Direction.right;
            }
        }
        else
        {
            if (minototaureRgb.velocity != Vector2.zero)
            {
                if (Mathf.Abs(minototaureRgb.velocity.x) > Mathf.Abs(minototaureRgb.velocity.y))
                {
                    if (minototaureRgb.velocity.x < 0)
                    {
                        watchDirection = Direction.left;
                    }
                    else
                    {
                        watchDirection = Direction.right;
                    }
                }
                else
                {
                    if (minototaureRgb.velocity.y < 0)
                    {
                        watchDirection = Direction.down;
                    }
                    else
                    {
                        watchDirection = Direction.up;
                    }
                }
            }
        }
    }


    public void StartKnockBack()
    {
        StartCoroutine(ChargeKnockBack());
    }

    public IEnumerator ChargeKnockBack()
    {
        knockBack = true;
        minototaureRgb.velocity = Vector2.zero;

        yield return new WaitForFixedUpdate();

        minototaureRgb.velocity = knockBackCaller.knockBackDir * knockBackCaller.knockBackStrength;

        yield return new WaitForSeconds(knockBackCaller.knockBackTime);

        minototaureRgb.velocity = Vector2.zero;
        knockBack = false;
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
