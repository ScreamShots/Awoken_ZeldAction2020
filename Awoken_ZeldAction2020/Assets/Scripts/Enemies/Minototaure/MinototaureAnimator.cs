using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather animation of minototaure
/// </summary>

public class MinototaureAnimator : MonoBehaviour
{
    Animator minototaureAnim;
    MinototaureAttack minototaureAttackScript;
    MinototaureMovement minototaureMoveScript;

    MinototaureMovement.Direction animDirection;

    private void Start()
    {
        minototaureAnim = GetComponent<Animator>();
        minototaureAttackScript = GetComponentInParent<MinototaureAttack>();
        minototaureMoveScript = GetComponentInParent<MinototaureMovement>();
    }

    private void Update()
    {
        if (!minototaureAttackScript.isStun && !minototaureAttackScript.lauchAttack)                        //to stop looking direction of Player
        {
            SetAnimDirection();
        }

        minototaureAnim.SetBool("RandomWalk", minototaureMoveScript.isOnRandomMove);
        minototaureAnim.SetBool("Chase", minototaureMoveScript.playerDetected);
        minototaureAnim.SetBool("PrepaAttack", minototaureAttackScript.isPreparingAttack);
        minototaureAnim.SetBool("Attack", minototaureAttackScript.lauchAttack);
        minototaureAnim.SetBool("Stun", minototaureAttackScript.isStun);
    }


    void SetAnimDirection()
    {
        animDirection = minototaureMoveScript.watchDirection;
        switch (animDirection)
        {
            case MinototaureMovement.Direction.up:
                minototaureAnim.SetFloat("XMove", 0);
                minototaureAnim.SetFloat("YMove", 1);
                break;
            case MinototaureMovement.Direction.down:
                minototaureAnim.SetFloat("XMove", 0);
                minototaureAnim.SetFloat("YMove", -1);
                break;
            case MinototaureMovement.Direction.right:
                minototaureAnim.SetFloat("XMove", 1);
                minototaureAnim.SetFloat("YMove", 0);
                break;
            case MinototaureMovement.Direction.left:
                minototaureAnim.SetFloat("XMove", -1);
                minototaureAnim.SetFloat("YMove", 0);
                break;
            default:
                break;
        }
    }
}
