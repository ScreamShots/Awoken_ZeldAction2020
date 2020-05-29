using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauExitAnim : MonoBehaviour
{
    private TuyauEnter exit;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        exit = GetComponentInParent<TuyauEnter>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayExit();
    }

    void PlayExit()
    {
        if(exit.isExit == true)
        {
            anim.SetBool("isExit", true);
        }
        else if (exit.isExit == false)
        {
            anim.SetBool("isExit", false);
        }
    }
}
