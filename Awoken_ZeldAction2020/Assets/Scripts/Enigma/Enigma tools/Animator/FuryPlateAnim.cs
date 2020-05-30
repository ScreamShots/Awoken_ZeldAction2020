using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryPlateAnim : MonoBehaviour
{
    private FuryPlate furyPlate;
    private Animator anim;
    void Start()
    {
        furyPlate = GetComponentInParent<FuryPlate>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PressedPlate();
        UnPressedPlate();
    }

    void PressedPlate()
    {
        if (furyPlate.isPressed == true)
        {
            anim.SetBool("isPressed", true);
        }
    }

    void UnPressedPlate()
    {
        if (furyPlate.isPressed == false)
        {
            anim.SetBool("isPressed", false);
        }
    }
}
