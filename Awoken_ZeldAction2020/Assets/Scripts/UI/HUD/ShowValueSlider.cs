using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowValueSlider : MonoBehaviour
{
    private TextMeshProUGUI percentageText = null;

    void Start()
    {
        percentageText = GetComponent<TextMeshProUGUI>();
    }

    public void textUpdate(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
