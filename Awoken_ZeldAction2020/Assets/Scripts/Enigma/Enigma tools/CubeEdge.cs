using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent helped by Rémi Secher
/// This script detect in from witch side the player collides with the cube and allow the cube to move
/// </summary>
public class CubeEdge : MonoBehaviour
{
    #region Edge collider statement
    public enum Direction { up, down, right, left};
    public Direction edgeDirection;
    private bool playerCollisionning = false;
    private CubeToPush cubeScript;
    private PlayerMovement playerMoveScript;
    #endregion

    private void Start()
    {
        cubeScript = transform.GetComponentInParent<CubeToPush>();
        playerMoveScript = PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>();
    }


    private void OnCollisionEnter2D(Collision2D other) //Looks if the player is collisioning the cube
    {
        if(other.transform.root.tag == "Player")
        {
            playerCollisionning = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) // looks if the player isn't collisioning the cube
    {
        if (other.transform.root.tag == "Player")
        {
            playerCollisionning = false;
            cubeScript.playerPushing = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) // Looks if the player is actually pushing the cube
    {
        if (playerCollisionning == true) // The player is pushing the cube
        {
            switch(edgeDirection)
            {
                case Direction.up: //The player enter the UpEdgeCollider of the cube
                    if(playerMoveScript.verticalAxis <0)
                    {
                        cubeScript.playerPushing = true;
                        cubeScript.move = new Vector2(0, -1); //The cube is going downway
                    }
                    break;
                case Direction.down: //The player enter the DownEdgeCollider of the cube
                    if (playerMoveScript.verticalAxis > 0)
                    {
                        cubeScript.playerPushing = true;
                        cubeScript.move = new Vector2(0, 1); //The cube is going Upway
                    }
                    break;
                case Direction.right: //The player enter the RightEdgeCollider of the cube
                    if (playerMoveScript.horizontalAxis < 0)
                    {
                        cubeScript.playerPushing = true;
                        cubeScript.move = new Vector2(-1, 0); //The cube is going Leftway
                    }
                    break;
                case Direction.left: //The player enter the LeftEdgeCollider of the cube
                    if (playerMoveScript.horizontalAxis > 0)
                    {
                        cubeScript.playerPushing = true;
                        cubeScript.move = new Vector2(1, 0); //The cube is going Rightway
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
