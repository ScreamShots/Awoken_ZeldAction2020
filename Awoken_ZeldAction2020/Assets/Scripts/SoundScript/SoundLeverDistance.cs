using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du levier Distance
/// </summary>

public class SoundLeverDistance : MonoBehaviour
{
    private DistanceLever levierDistance;

    private bool l_distanceIsPressed;
    // Start is called before the first frame update
    void Start()
    {
        levierDistance = GetComponentInParent<DistanceLever>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l_distanceIsPressed != levierDistance.isPressed)
        {
            if (levierDistance == true)
            {
                LeverClick();
            }
            l_distanceIsPressed = levierDistance.isPressed;
        }
    }

    void LeverClick()
    {
        SoundManager.Instance.Play("LeverClick");
    }
}
