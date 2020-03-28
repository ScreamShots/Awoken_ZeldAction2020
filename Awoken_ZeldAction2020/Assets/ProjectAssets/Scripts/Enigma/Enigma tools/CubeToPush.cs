using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Bastien Prigent helped by Rémi Secher
/// Gives physics to the cube and make it move
/// </summary>
public class CubeToPush : MonoBehaviour
{
    #region cube statement
    public Rigidbody2D cubeRgb;
    public bool playerPushing = false;
    public Vector2 move;
    public float moveSpeed;
    #endregion



    private void Start()
    {
        cubeRgb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerPushing == true) //Looking if the player is pushing the cube
        {
            Move(); //If yes the cube moves
        }
        else if (playerPushing == false) //The player isn't pushing the cube
        {
            if (cubeRgb.velocity.x != 0 || cubeRgb.velocity.y != 0) //Looking if the cube has a vertical or horizontal velocity
            {
                cubeRgb.velocity = new Vector2(0, 0); //Cube has got a vertical or horizontal velocity so we stop the cube
            }
        }
        
    }

    /// <summary>
    /// Method that makes the cube move
    /// </summary>
    private void Move()
    {
        cubeRgb.velocity = move * moveSpeed * Time.fixedDeltaTime;
    }
}
