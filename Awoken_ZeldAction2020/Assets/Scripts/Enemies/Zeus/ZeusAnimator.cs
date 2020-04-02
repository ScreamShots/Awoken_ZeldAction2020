using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusAnimator : MonoBehaviour
{
    Animator ZeusAnim;
    BossState1 ZeusScriptState1;

    private void Start()
    {
        ZeusAnim = GetComponent<Animator>();
        ZeusScriptState1 = GetComponentInParent<BossState1>();
    }

    private void Update()
    {
        ZeusAnim.SetBool("ZeusThunder", ZeusScriptState1.animThunder);
        ZeusAnim.SetBool("ZeusSchockWave", ZeusScriptState1.animShockWave);
    }
}
