using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons de la plaque de pression Instant
/// </summary>

public class SoundInstantPlate : MonoBehaviour
{
    private InstantPressurePlate plateInstant;

    private bool l_instantIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        plateInstant = GetComponentInParent<InstantPressurePlate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l_instantIsPressed != plateInstant.isPressed)
        {
            if (plateInstant == true)
            {
                PressurePlateActivated();
            }
            l_instantIsPressed = plateInstant.isPressed;
        }
    }

    void PressurePlateActivated()
    {
        SoundManager.Instance.Play("ActivatePlate");
    }
}
