using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeToPush : MonoBehaviour
{
    public  Rigidbody2D cubeRgb;
    private PlayerMovement playerMovement;

    private void Start()
    {
        cubeRgb = GetComponent<Rigidbody2D>();
        playerMovement = PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>();
    }
    private void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.transform.root.CompareTag("Player") /*&& PlayerMovement.playerRgb.velocity.x != 0 || PlayerMovement.playerRgb.velocity.y != 0*/)
        {
            switch (playerMovement.watchDirection)
            {
                case PlayerMovement.Direction.down:
                    cubeRgb.velocity = new Vector2 (0,-1);
                    break;
                case PlayerMovement.Direction.up:
                    cubeRgb.velocity = new Vector2 (0, 1);
                    break;
                case PlayerMovement.Direction.right:
                    cubeRgb.velocity = new Vector2 (1, 0);
                    break;
                case PlayerMovement.Direction.left:
                    cubeRgb.velocity = new Vector2(-1, 0) ;
                    break;
            }
        }
    }   

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.transform.root.CompareTag("Player"))
        {
            cubeRgb.velocity = Vector2.zero;
        }
    }

}
