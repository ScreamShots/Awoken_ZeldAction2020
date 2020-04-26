using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather the animation of Poulion
/// </summary>

public class PoulionAnimator : MonoBehaviour
{
    #region Variables

    private Animator poulionAnimator;
    private PoulionMovement poulionMoveScript;
    private PoulionAttack poulionAttackScript;
    private bool alreadyDead;
    #endregion

    private void Start()
    {
        poulionAnimator = GetComponent<Animator>();
        poulionMoveScript = GetComponentInParent<PoulionMovement>();
        poulionAttackScript = GetComponentInParent<PoulionAttack>();
    }

    private void Update()
    {
        SetWatchDirection();
        SetWatchDirectionAttack();
        Running();
        Attack();
    }

    void SetWatchDirection()                                                                        //giving information relative to the watch direction to the animator
    {
        switch (poulionMoveScript.watchDirection)
        {
            case PoulionMovement.Direction.down:
                poulionAnimator.SetFloat("XMovement", 0);
                poulionAnimator.SetFloat("YMovement", -1);
                break;
            case PoulionMovement.Direction.up:
                poulionAnimator.SetFloat("XMovement", 0);
                poulionAnimator.SetFloat("YMovement", 1);
                break;
            case PoulionMovement.Direction.left:
                poulionAnimator.SetFloat("XMovement", -1);
                poulionAnimator.SetFloat("YMovement", 0);
                break;
            case PoulionMovement.Direction.right:
                poulionAnimator.SetFloat("XMovement", 1);
                poulionAnimator.SetFloat("YMovement", 0);
                break;
            default:
                poulionAnimator.SetFloat("XMovement", 0);
                poulionAnimator.SetFloat("YMovement", -1);
                break;
        }
    }

    void SetWatchDirectionAttack()                                                                        
    {
        switch (poulionAttackScript.watchDirection)
        {
            case PoulionAttack.Direction.down:
                poulionAnimator.SetFloat("XAttack", 0);
                poulionAnimator.SetFloat("YAttack", -1);
                break;
            case PoulionAttack.Direction.up:
                poulionAnimator.SetFloat("XAttack", 0);
                poulionAnimator.SetFloat("YAttack", 1);
                break;
            case PoulionAttack.Direction.left:
                poulionAnimator.SetFloat("XAttack", -1);
                poulionAnimator.SetFloat("YAttack", 0);
                break;
            case PoulionAttack.Direction.right:
                poulionAnimator.SetFloat("XAttack", 1);
                poulionAnimator.SetFloat("YAttack", 0);
                break;
            default:
                poulionAnimator.SetFloat("XAttack", 0);
                poulionAnimator.SetFloat("YAttack", -1);
                break;
        }
    }

    void Running()                                                                                  //Launch run animation   
    {
        poulionAnimator.SetBool("isRunning", poulionMoveScript.isRunning);
    }

    void Attack()
    {
        poulionAnimator.SetBool("isAttacking", poulionAttackScript.poulionIsAttacking);

        poulionAnimator.SetBool("isCharging", poulionAttackScript.chargeOn);

        poulionAnimator.SetBool("isStun", poulionAttackScript.isStun);
    }

}
