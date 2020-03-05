using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This Script gather player action relativ to movement, like actual move or facing direction system
/// </summary> 

[RequireComponent(typeof (PlayerManager))]
public class PlayerMovement : MonoBehaviour
{
    #region Hidden var Statement
    private enum Direction { up, down, left, right }

    private Rigidbody2D playerRgb;    
    private Direction watchDirection;

    private float horizontalAxis;
    private float verticalAxis;
    private Vector2 move;

    #endregion

    #region Inspector Visible var Statement
    [Space] [Header ("Stats")] 
    
    [SerializeField] [Min (0)] [Tooltip ("speed of the player on his basic movement (Min: 0)")]
    private float speed;

    #endregion

    private void Start()
    {
        playerRgb = GetComponent<Rigidbody2D>();
        watchDirection = Direction.up;                  // we set a direction on the object creation as a security 
    }

    private void FixedUpdate()      // usage of the Fixed Update because we are working on physics and rigidBody
    {
        Move();

        SetDirection();
    }   

    void Move()                     // PlayerBasic Movement
    {
        horizontalAxis = Input.GetAxis("HorizontalAxis");           //getting axis values from the inputSystem Files (mapped input)
        verticalAxis = Input.GetAxis("VerticalAxis");
        move = new Vector2(horizontalAxis, verticalAxis);           // setting a vector that give the player the direction of the movement

        playerRgb.velocity = move * speed * Time.fixedDeltaTime;    // actualy do the player move (Time.fixedDeltaTime) is required to prevent lag issue
    }                  

    void SetDirection()            // this part is used to determined the direction where the player is Watching / Mooving (for Animation && Attack)
    {
        if (playerRgb.velocity.x != 0 || playerRgb.velocity.y != 0)  //we want to change the direction only if the player is currenty mooving
        {
            if (Mathf.Abs(horizontalAxis) > Mathf.Abs(verticalAxis))    //testing wich one of the two axis is the strongest (value most distant from 0)
            {
                if (horizontalAxis > 0)                                     //if horizontal axis is strongest and positiv the player is facing right
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
                if (verticalAxis > 0)                                       //if vertical axis is strongest and positiv the player is facing up
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
