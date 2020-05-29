using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnterAnim : MonoBehaviour
{
    private TuyauEnter enter;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        enter = GetComponentInParent<TuyauEnter>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayAnim();
    }

    void PlayAnim()
    {
        if (enter.isEnter == true)
        {
            anim.SetBool("isEnter", true);
        }
        else if (enter.isEnter == false)
        {
            anim.SetBool("isEnter", false);
        }
    }
}
