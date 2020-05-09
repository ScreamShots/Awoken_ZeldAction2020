using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealAnimation : MonoBehaviour
{
    Animator sealAnim;
    SealTeleporter sealTeleporterScript;

    private void Start()
    {
        sealAnim = GetComponent<Animator>();
        sealTeleporterScript = GetComponentInParent<SealTeleporter>();
    }

    private void Update()
    {
        sealAnim.SetBool("TPActivate", sealTeleporterScript.canPlayTPAnimation);
    }
}
