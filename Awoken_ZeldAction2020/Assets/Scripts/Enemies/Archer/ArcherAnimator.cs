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
    private bool alreadyAttacked;
    #endregion

    private void Start()
    {
        archerAnimator = GetComponent<Animator>();
        archerMoveScript = GetComponentInParent<ArcherMovement>();
        archerAttackScript = GetComponentInParent<ArcherAttack>();
    }

    private void Update()
    {
        SetWatchDirection();
        Running();
        Attack();
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

    void Running()                                                                                  //Launch run animation   
    {
        archerAnimator.SetBool("isRunning", archerMoveScript.isRunning);
    }

    void Attack()
    {
        if (archerAttackScript.archerIsAttacking && !alreadyAttacked)
        {
            alreadyAttacked = true;
            archerAnimator.SetTrigger("isAttacking");
        }
        else if (!archerAttackScript.archerIsAttacking && alreadyAttacked)
        {
            alreadyAttacked = false;                                                
        }
    }
}
