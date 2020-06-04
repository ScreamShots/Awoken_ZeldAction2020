using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This Script gather player action relativ to movement, like actual move or facing direction system
/// </summary> 

public class PlayerMovement : MonoBehaviour
{
    #region HideInInspector var Statement
    public enum Direction { up, down, left, right }

    public static Rigidbody2D playerRgb;    
    public Direction watchDirection = Direction.down;

    [HideInInspector] public float horizontalAxis;
    [HideInInspector] public float verticalAxis;
    private Vector2 move;
    [HideInInspector] public bool isRunning;

    #endregion

    #region SerializeField var Statement
    [Header ("Stats")] 
    
    [Min (0)] [Tooltip ("speed of the player on his basic movement (Min: 0)")]
    public float currentSpeed;
    [Min(0)]
    public float normalWalkSpeed;
    [Min(0)]
    public float shieldWalkSpeed;

    #endregion

    private void Awake()
    {
        playerRgb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentSpeed = normalWalkSpeed;
    }

    private void FixedUpdate()      // usage of the Fixed Update because we are working on physics and rigidBody
    {
        if(GameManager.Instance.gameState == GameManager.GameState.Running)
        {
            if (PlayerStatusManager.Instance.canMove)
            {
                Move();
            }          
        }

        if (PlayerStatusManager.Instance.canChangeDirection)
        {
            SetDirection();
        }
        if (playerRgb.velocity.x != 0 || playerRgb.velocity.y != 0) { isRunning = true; }
        else { isRunning = false; }
    }   

    void Move()                     // PlayerBasic Movement
    {
        horizontalAxis = Input.GetAxis("HorizontalAxis");           //getting axis values from the inputSystem Files (mapped input)
        verticalAxis = Input.GetAxis("VerticalAxis");

        move = new Vector2(horizontalAxis, verticalAxis);           // setting a vector that give the player the direction of the movement
        move = move.normalized;                                     // normalizing the direction to prevent the player to move faster on diagonal directions 

        playerRgb.velocity = move * currentSpeed * Time.fixedDeltaTime;    // actualy do the player move (Time.fixedDeltaTime) is required to prevent lag issue


    }                  

    void SetDirection()            // this part is used to determined the direction where the player is Watching / Mooving (for Animation && Attack)
    {
        if (playerRgb.velocity.x != 0 || playerRgb.velocity.y != 0)  //we want to change the direction only if the player is currenty mooving
        {
            if (Mathf.Abs(playerRgb.velocity.x) > Mathf.Abs(playerRgb.velocity.y))    //testing wich one of the two axis is the strongest (value most distant from 0)
            {
                if (playerRgb.velocity.x > 0)                                     //if horizontal axis is strongest and positiv the player is facing right
                {
                    watchDirection = Direction.right;
                }
                else
                {
                    watchDirection = Direction.left;                        //if horizontal axis is strongest and negativ the player is facing left
                }
            }
            else
            {
                if (playerRgb.velocity.y > 0)                                       //if vertical axis is strongest and positiv the player is facing up
                {
                    watchDirection = Direction.up;
                }
                else
                {
                    watchDirection = Direction.down;                        //if vertical axis is strongest and positiv the player is facing down
                }
            }
        }        
    }
}
