using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class MainMenuButton : MonoBehaviour, ISelectHandler
{
    Button myButton = null;
    Slider mySlider = null;
    TextMeshProUGUI myText = null;

    bool colorOnTransparent = false;
    bool colorOnOpac = false;

    public UnityEvent onSelection;

    private bool handleButton = false;
    private bool handleSlider = false;

    private void Start()
    {
        if (gameObject.GetComponent<Button>() != null)
        {
            myButton = GetComponent<Button>();
            handleButton = true;
        }
        else if (gameObject.GetComponent<Slider>() != null)
        {
            mySlider = GetComponent<Slider>();
            handleSlider = true;
        }

        myText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (handleButton)
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
        else if (handleSlider)
        {
            if (!mySlider.IsInteractable() && !colorOnTransparent)
            {
                colorOnTransparent = true;
                colorOnOpac = false;
                myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, myText.color.a * 0.5f);
            }
            else if (mySlider.IsInteractable() && !colorOnOpac)
            {
                colorOnTransparent = false;
                colorOnOpac = true;
                myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 1);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelection.Invoke();
    }
}
