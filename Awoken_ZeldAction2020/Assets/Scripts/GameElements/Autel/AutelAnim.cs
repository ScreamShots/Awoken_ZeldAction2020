using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutelAnim : MonoBehaviour
{
    private Animator anim;
    private Autel autel;

    void Start()
    {
        anim = GetComponent<Animator>();
        autel = GetComponentInParent<Autel>();
    }


    void Update()
    {
        if (autel.isAutelActivated == true)
        {
            anim.SetBool("isActivated", true);
        }
    }
}
