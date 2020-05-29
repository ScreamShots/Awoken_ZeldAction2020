using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuButton : MonoBehaviour
{
    Button myButton = null;
    TextMeshProUGUI myText = null;

    bool colorOnTransparent = false;
    bool colorOnOpac = false;

    private void Start()
    {
        myButton = GetComponent<Button>();
        myText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!myButton.IsInteractable() && !colorOnTransparent)
        {
            colorOnTransparent = true;
            colorOnOpac = false;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, myText.color.a * 0.5f);
        }
        else if (myButton.IsInteractable() && !colorOnOpac)
        {
            colorOnTransparent = false;
            colorOnOpac = true;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 1);
        }
    }
}
