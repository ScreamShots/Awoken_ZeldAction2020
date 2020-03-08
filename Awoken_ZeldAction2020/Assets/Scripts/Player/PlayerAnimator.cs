using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Rémi Sécher
/// This script is used to manage the player animator
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    #region HideInInspector var Statement 

    private Animator plyAnimator;
    private PlayerMovement playerMoveScript;
    private bool alreadyAttacked;

    #endregion

    private void Start()
    {
        plyAnimator = GetComponent<Animator>();                                     //finding requiered component
        playerMoveScript = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        SetWatchDirection();
        Running();
        TempAttack();
        SetBlock();
    }


    void SetBlock()
    {
        plyAnimator.SetBool("isBlocking", PlayerStatusManager.Instance.isBlocking);
    }                                                              //Launch block animation
    void SetWatchDirection()                                                        //giving information relative to the watch direction to the animator
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
    void Running()                                                                  //Launch run animation   
    {
        plyAnimator.SetBool("isRunning", playerMoveScript.isRunning);               
    }    
    void TempAttack()                                                               //Launch attack animation    
    {
        if (PlayerStatusManager.Instance.isAttacking && !alreadyAttacked)          
        {
            alreadyAttacked = true;
            plyAnimator.SetTrigger("Temp_LaunchAttack");
        }
        else if (!PlayerStatusManager.Instance.isAttacking && alreadyAttacked)
        {
            alreadyAttacked = false;                                                //security to avoid animation starting twice for a single attack
        }
    }
}
