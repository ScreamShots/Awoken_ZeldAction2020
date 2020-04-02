using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAnimator : MonoBehaviour
{
    Animator lightningAnim;
    LightningComportement lightningScript;

    private void Start()
    {
        lightningAnim = GetComponent<Animator>();
        lightningScript = GetComponentInParent<LightningComportement>();
    }

    private void Update()
    {
        lightningAnim.SetBool("Thunder", lightningScript.thunderIsSlam);
    }
}
