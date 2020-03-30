using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulionReworkedAnimator : MonoBehaviour
{
    Animator poulionAnim;
    PoulionAttackReworked poulionAttackScript;
    PoulionMovementReworked poulionMoveScript;

    PoulionMovementReworked.Direction animDirection;

    private void Start()
    {
        poulionAnim = GetComponent<Animator>();
        poulionAttackScript = GetComponentInParent<PoulionAttackReworked>();
        poulionMoveScript = GetComponentInParent<PoulionMovementReworked>();    
    }

    private void Update()
    {
        if (!poulionAttackScript.isStun)
        {
            SetAnimDirection();
        }        

        poulionAnim.SetBool("RandomWalk", poulionMoveScript.isOnRandomMove);
        poulionAnim.SetBool("Chase", poulionMoveScript.playerDetected);
        poulionAnim.SetBool("PrepaCharge", poulionAttackScript.isPreparingCharge);
        poulionAnim.SetBool("Charge", poulionAttackScript.isCharging);
        poulionAnim.SetBool("Stun", poulionAttackScript.isStun);
    }


    void SetAnimDirection()
    {
        animDirection = poulionMoveScript.watchDirection;
        switch (animDirection)
        {
            case PoulionMovementReworked.Direction.up:
                poulionAnim.SetFloat("XMove", 0);
                poulionAnim.SetFloat("YMove", 1);
                break;
            case PoulionMovementReworked.Direction.down:
                poulionAnim.SetFloat("XMove", 0);
                poulionAnim.SetFloat("YMove", -1);
                break;
            case PoulionMovementReworked.Direction.right:
                poulionAnim.SetFloat("XMove", 1);
                poulionAnim.SetFloat("YMove", 0);
                break;
            case PoulionMovementReworked.Direction.left:
                poulionAnim.SetFloat("XMove", -1);
                poulionAnim.SetFloat("YMove", 0);
                break;
            default:
                break;
        }
    }
}
