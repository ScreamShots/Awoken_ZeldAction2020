using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevTools;

/// <summary>
/// Made by Antoine
/// This script gather movement of Poulion and facing direction
/// </summary>

public class PoulionMovement : MonoBehaviour
{
    #region Variables
    [Header("Poulion Speed")]

    [SerializeField]
    [Min(0)]
    private float neutralSpeed = 0;

    [SerializeField]
    [Min(0)]
    private float chaseSpeed = 0;

    [Header("Poulion Movement")]
    public float chaseDistance;
    public float attackDistance;

    [Header("State 1")]
    public float timeChangingDirection;
    public float timeBeforePause;
    private float timeLeft;

    [Header("State 2")]
    [Range(1, 10)]
    public int magnitudeMin;
    [Range(1, 10)]
    public int magnitudeMax;
    public float frequency;

    private GameObject player;

    private Rigidbody2D rb;

    private Vector2 direction;

    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection = Direction.down;

    [HideInInspector] public bool isRunning;

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
            if (allRangesCircles != null)
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

        timeLeft = timeChangingDirection;

        DrawRangeCircles();
    }

    private void FixedUpdate()
    {
        Move();
        SetDirection();
        OnValidate();
    }

    void Move()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);                                           //Calculate distance between poulion && player

        direction = (player.transform.position - transform.position).normalized;                                                    //Calculate direction between poulion && player

        Vector2 normalToDirection = Vector2.Perpendicular(direction);                                                               //Normal Vector (perpendicular) of vector direction

        Vector2 randomPerpendicular = normalToDirection.normalized * Mathf.Sin(Time.fixedTime * frequency) * Random.Range(magnitudeMin, magnitudeMax);      //Random perpendicular vector of vector direction


        if (distance > chaseDistance && !GetComponent<PoulionAttack>().poulionIsAttacking)                                                                  //Random movement 
        {
            GetComponent<PoulionAttack>().poulionCanAttack = false;

            timeLeft -= Time.fixedDeltaTime;

            if(timeLeft <= 0)
            {
                StartCoroutine(RandomMove());
            }
        }
        else if (distance > attackDistance && distance < chaseDistance && !GetComponent<PoulionAttack>().poulionIsAttacking)                                //Move to player in zigzagging
        {
            rb.velocity = (randomPerpendicular + direction) * chaseSpeed * Time.fixedDeltaTime;
            GetComponent<PoulionAttack>().poulionCanAttack = false;
        }
        else if (distance <= attackDistance)                                                                                                                //Attack of the poulion
        {
            GetComponent<PoulionAttack>().poulionCanAttack = true;
        }
        else
        {
            GetComponent<PoulionAttack>().poulionCanAttack = false;
        }

        if (rb.velocity.x != 0 || rb.velocity.y != 0) { isRunning = true; }                                                                                 //Check if poulion are moving
        else { isRunning = false; }
    }

    void SetDirection()
    {
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
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
                if (direction.y > 0)
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

        var chaseRangeDisplay = new GameObject { name = "Chase Circle" };
        chaseRangeDisplay.transform.parent = allRangesCircles.transform;

        var attackRangeDisplay = new GameObject { name = "Chase Circle" };
        attackRangeDisplay.transform.parent = allRangesCircles.transform;

        chaseRangeDisplay.DrawCircle(chaseDistance, 0.02f, 50, Color.yellow);
        attackRangeDisplay.DrawCircle(attackDistance, 0.02f, 50, Color.red);

        attackRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);
        chaseRangeDisplay.transform.localPosition = new Vector3(0, 0, 0);

        allRangesCircles.SetActive(false);
    }                       //function that draw 2 circle, one for eache range the poulion has (attack and chase)

    [ContextMenu("Refresh Range Circles")]
    void RefreshRangeCircles()
    {
        if (allRangesCircles != null)
        {
            DestroyImmediate(allRangesCircles);
        }
        DrawRangeCircles();
        showRanges = false;
    }                       //function that refresh the range circles by calling this function from the inspector (right click on the name of the script)

    IEnumerator RandomMove()
    {
        timeLeft += timeChangingDirection;
        Vector2 randomDirection = new Vector2(Random.Range(-10, +10), Random.Range(-10, +10));
        rb.velocity = randomDirection.normalized * neutralSpeed * Time.fixedDeltaTime;

        yield return new WaitForSeconds(timeBeforePause);
        rb.velocity = Vector2.zero;
    }
}
