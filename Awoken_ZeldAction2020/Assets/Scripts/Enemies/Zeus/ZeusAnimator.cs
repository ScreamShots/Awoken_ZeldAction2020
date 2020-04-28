using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusAnimator : MonoBehaviour
{
    Animator ZeusAnim;
    BossState1 ZeusScriptState1;
    BossState2 ZeusScriptState2;

    private void Start()
    {
        ZeusAnim = GetComponent<Animator>();
        ZeusScriptState1 = GetComponentInParent<BossState1>();
        ZeusScriptState2 = GetComponentInParent<BossState2>();
    }

    private void Update()
    {
        ZeusAnim.SetBool("ZeusThunder", ZeusScriptState1.animThunder);
        ZeusAnim.SetBool("ZeusSchockWave", ZeusScriptState1.animShockWave);
        ZeusAnim.SetBool("ZeusThunder2", ZeusScriptState2.animThunder);
        ZeusAnim.SetBool("ZeusShoot", ZeusScriptState2.animShoot);
        ZeusAnim.SetBool("ZeusWall", ZeusScriptState2.animWall);
        ZeusAnim.SetBool("isPunching", ZeusScriptState2.isPunching);
    }
}
