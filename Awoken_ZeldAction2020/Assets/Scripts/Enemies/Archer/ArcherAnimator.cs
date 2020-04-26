using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather animation of Archer
/// </summary>

public class ArcherAnimator : MonoBehaviour
{
    #region Variables

    private Animator archerAnimator;
    private ArcherMovement archerMoveScript;
    private ArcherAttack archerAttackScript;
    private bool alreadyDead = false;

    #endregion

    private void Start()
    {
        archerAnimator = GetComponent<Animator>();
        archerMoveScript = GetComponentInParent<ArcherMovement>();
        archerAttackScript = GetComponentInParent<ArcherAttack>();
    }

    private void Update()
    {
        if (!alreadyDead)
        {
            SetWatchDirection();
            SetWatchDirectionAttack();
            Running();
            Attack();
        }
      
    }

    void SetWatchDirection()                                                                        //giving information relative to the watch direction to the animator
    {
        switch (archerMoveScript.watchDirection)
        {
            case ArcherMovement.Direction.down:
                archerAnimator.SetFloat("XMovement", 0);
                archerAnimator.SetFloat("YMovement", -1);
                break;
            case ArcherMovement.Direction.up:
                archerAnimator.SetFloat("XMovement", 0);
                archerAnimator.SetFloat("YMovement", 1);
                break;
            case ArcherMovement.Direction.left:
                archerAnimator.SetFloat("XMovement", -1);
                archerAnimator.SetFloat("YMovement", 0);
                break;
            case ArcherMovement.Direction.right:
                archerAnimator.SetFloat("XMovement", 1);
                archerAnimator.SetFloat("YMovement", 0);
                break;
            default:
                archerAnimator.SetFloat("XMovement", 0);
                archerAnimator.SetFloat("YMovement", -1);
                break;
        }
    }

    void SetWatchDirectionAttack()
    {
        switch (archerAttackScript.watchDirection)
        {
            case ArcherAttack.Direction.down:
                archerAnimator.SetFloat("XAttack", 0);
                archerAnimator.SetFloat("YAttack", -1);
                break;
            case ArcherAttack.Direction.up:
                archerAnimator.SetFloat("XAttack", 0);
                archerAnimator.SetFloat("YAttack", 1);
                break;
            case ArcherAttack.Direction.left:
                archerAnimator.SetFloat("XAttack", -1);
                archerAnimator.SetFloat("YAttack", 0);
                break;
            case ArcherAttack.Direction.right:
                archerAnimator.SetFloat("XAttack", 1);
                archerAnimator.SetFloat("YAttack", 0);
                break;
            default:
                archerAnimator.SetFloat("XAttack", 0);
                archerAnimator.SetFloat("YAttack", -1);
                break;
        }
    }

    void Running()                                                                                  //Launch run animation   
    {
        archerAnimator.SetBool("isRunning", archerMoveScript.isRunning);

        archerAnimator.SetBool("isRetrait", archerMoveScript.isRetrait);
    }

    void Attack()
    {
        archerAnimator.SetBool("isAttacking", archerAttackScript.animationAttack);
    }
}
