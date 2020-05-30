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
    TextMeshProUGUI myText = null;

    bool colorOnTransparent = false;
    bool colorOnOpac = false;

    public UnityEvent onSelection;

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

    public void OnSelect(BaseEventData eventData)
    {
        onSelection.Invoke();
    }
}
