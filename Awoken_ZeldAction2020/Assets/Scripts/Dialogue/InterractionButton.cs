using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractionButton : MonoBehaviour
{
    private Animator buttonAnimator;

    private void Start()
    {
        buttonAnimator = GetComponent<Animator>();
    }

    public void ShowButton()
    {
        buttonAnimator.SetBool("Show", true);
        buttonAnimator.SetBool("Hide", false);
    }

    public void HideButton()
    {
        buttonAnimator.SetBool("Hide", true);
        buttonAnimator.SetBool("Show", false);
    }
}
