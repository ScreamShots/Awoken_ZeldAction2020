using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegaseAnimator : MonoBehaviour
{
    Animator pegaseAnim;
    PegaseSupport pegasenSupportScript;
    PegaseMovement pegaseMoveScript;

    PegaseMovement.Direction animDirection;

    private void Start()
    {
        pegaseAnim = GetComponent<Animator>();
        pegasenSupportScript = GetComponentInChildren<PegaseSupport>();
        pegaseMoveScript = GetComponentInParent<PegaseMovement>();
    }

    private void Update()
    {
        SetAnimDirection();

        pegaseAnim.SetBool("RandomWalk", pegaseMoveScript.isOnRandomMove);
        pegaseAnim.SetBool("PrepareTP", pegaseMoveScript.prepareTeleport);
        pegaseAnim.SetBool("TP", pegaseMoveScript.isTeleport);
    }


    void SetAnimDirection()
    {
        animDirection = pegaseMoveScript.watchDirection;
        switch (animDirection)
        {
            case PegaseMovement.Direction.up:
                pegaseAnim.SetFloat("XMove", 0);
                pegaseAnim.SetFloat("YMove", 1);
                break;
            case PegaseMovement.Direction.down:
                pegaseAnim.SetFloat("XMove", 0);
                pegaseAnim.SetFloat("YMove", -1);
                break;
            case PegaseMovement.Direction.right:
                pegaseAnim.SetFloat("XMove", 1);
                pegaseAnim.SetFloat("YMove", 0);
                break;
            case PegaseMovement.Direction.left:
                pegaseAnim.SetFloat("XMove", -1);
                pegaseAnim.SetFloat("YMove", 0);
                break;
            default:
                break;
        }
    }
}
