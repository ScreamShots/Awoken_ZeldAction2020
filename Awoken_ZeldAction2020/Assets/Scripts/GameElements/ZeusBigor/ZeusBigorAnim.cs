using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusBigorAnim : MonoBehaviour
{
    private Animator anim;
    private ZeusBigor statue;

    void Start()
    {
        anim = GetComponent<Animator>();
        statue = GetComponentInParent<ZeusBigor>();
    }


    void Update()
    {
        if (statue.isStatueActivated == true)
        {
            anim.SetBool("isActivated", true);
        }
    }
}
