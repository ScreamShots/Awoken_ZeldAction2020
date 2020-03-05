using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Rémi Sécher
/// This script is used to manage the player animator
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    private Animator plyAnimator;
    private PlayerMovement playerMoveScript;

    private void Start()
    {
        plyAnimator = GetComponent<Animator>();
        playerMoveScript = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        SetWatchDirection();
        Running();
    }

    void SetWatchDirection()
    {
        switch (playerMoveScript.watchDirection)
        {
            case PlayerMovement.Direction.down:
                plyAnimator.SetFloat("XMovement", 0);
                plyAnimator.SetFloat("YMovement", -1);
                break;
            case PlayerMovement.Direction.up:
                plyAnimator.SetFloat("XMovement", 0);
                plyAnimator.SetFloat("YMovement", 1);
                break;
            case PlayerMovement.Direction.left:
                plyAnimator.SetFloat("XMovement", -1);
                plyAnimator.SetFloat("YMovement", 0);
                break;
            case PlayerMovement.Direction.right:
                plyAnimator.SetFloat("XMovement", 1);
                plyAnimator.SetFloat("YMovement", 0);
                break;
            default:
                plyAnimator.SetFloat("XMovement", 0);
                plyAnimator.SetFloat("YMovement", -1);
                break;
        }
    }

    void Running()
    {
        plyAnimator.SetBool("isRunning", playerMoveScript.isRunning);
    }
}
