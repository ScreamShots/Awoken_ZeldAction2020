using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// Animation of the Instant Pressure Plate
/// </summary>
public class InstantPlateAnimation : MonoBehaviour
{
    private InstantPressurePlate instantPlate;
    private Animator anim;

    void Start()
    {
        instantPlate = GetComponentInParent<InstantPressurePlate>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PressedPlate();
        UnpressedPlate();
    }

    void PressedPlate()
    {
        if (instantPlate.isPressed == true)
        {
            anim.SetBool("isPressed", true);
        }
    }

    void UnpressedPlate()
    {
        if (instantPlate.isPressed == false)
        {
            anim.SetBool("isPressed", false);
        }
    }
}