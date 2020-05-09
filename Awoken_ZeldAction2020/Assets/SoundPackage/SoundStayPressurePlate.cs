using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons de la plaque de pression on Stay
/// </summary>

public class SoundStayPressurePlate : MonoBehaviour
{
    private StayPressurePlate plateStay;

    private bool l_stayIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        plateStay = GetComponentInParent<StayPressurePlate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l_stayIsPressed != plateStay.isPressed)
        {
            if (plateStay == true)
            {
                PressurePlateActivated();
            }
            
            l_stayIsPressed = plateStay.isPressed;
        }
    }

    void PressurePlateActivated()
    {
        SoundManager.Instance.Play("ActivatePlate");
    }
}
