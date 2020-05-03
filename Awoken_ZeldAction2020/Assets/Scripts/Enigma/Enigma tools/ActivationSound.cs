using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons des différents levier et plaques de pression
/// </summary>

public class ActivationSound : MonoBehaviour
{

    private DistanceLever levierDistance;
    private InstantPressurePlate plateInstant;
    private StayPressurePlate plateStay;
    private ActionLever levierAction;

    private bool l_distanceIsPressed;
    private bool l_instantIsPressed;
    private bool l_stayIsPressed;
    private bool l_actionIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
