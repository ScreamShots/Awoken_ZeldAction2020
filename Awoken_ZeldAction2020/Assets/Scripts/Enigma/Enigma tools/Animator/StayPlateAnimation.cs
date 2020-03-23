using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayPlateAnimation : MonoBehaviour
{
    private StayPressurePlate stayPlate;
    private Animator anim;
    void Start()
    {
        stayPlate = GetComponentInParent<StayPressurePlate>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PressedPlate();
        UnPressedPlate();
    }

    void PressedPlate()
    {
        if ( stayPlate.isPressed == true)
        {
            anim.SetBool("isPressed", true);
        }
    }

    void UnPressedPlate()
    {
        if (stayPlate.isPressed == false)
        {
            anim.SetBool("isPressed", false);
        }
    }
}
