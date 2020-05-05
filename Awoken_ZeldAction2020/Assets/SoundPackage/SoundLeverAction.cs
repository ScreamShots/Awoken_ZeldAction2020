using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du levier Action
/// </summary>

public class SoundLeverAction : MonoBehaviour
{
    private ActionLever levierAction;

    private bool l_actionIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        levierAction = GetComponentInParent<ActionLever>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l_actionIsPressed != levierAction.isPressed)
        {
            if (levierAction == true)
            {
                LeverClick();
            }
            l_actionIsPressed = levierAction.isPressed;
        }
    }

    void LeverClick()
    {
        SoundManager.Instance.Play("LeverClick");
    }
}
