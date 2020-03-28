using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLeverAnim : MonoBehaviour
{
    private ActionLever leverAnim;
    private Animator anim;
    
    void Start()
    {
        leverAnim = GetComponentInParent<ActionLever>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LeverOn();
        LeverOff();
    }

    void LeverOn()
    {
        if(leverAnim.isPressed == true)
        {
            anim.SetBool("isPressed", true);
        }
    }
    void LeverOff()
    {
        if(leverAnim.isPressed == false)
        {
            anim.SetBool("isPressed", false);
        }
    }
}
