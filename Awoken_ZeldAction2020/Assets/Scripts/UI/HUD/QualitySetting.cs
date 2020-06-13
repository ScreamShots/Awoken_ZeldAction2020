using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySetting : MonoBehaviour
{
    Dropdown dropdownScript = null;
    Toggle toggleScript = null;

    public enum enumType { Checkbox, Dropdown, GodMode };
    public enumType whichType;

    void Start()
    {
        if (whichType == enumType.Checkbox)
        {
            toggleScript = GetComponent<Toggle>();
            toggleScript.isOn = GameManager.Instance.gameFullscren;
        }
        else if (whichType == enumType.Dropdown)
        {
            dropdownScript = GetComponent<Dropdown>();
            dropdownScript.value = GameManager.Instance.gameQuality;
        }
        else if (whichType == enumType.GodMode)
        {
            toggleScript = GetComponent<Toggle>();
            toggleScript.isOn = GameManager.Instance.godModeOn;
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        GameManager.Instance.gameFullscren = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex == 0)
        {
            QualitySettings.SetQualityLevel(0);
            GameManager.Instance.gameQuality = 0;
        }
        else if (qualityIndex == 1)
        {
            QualitySettings.SetQualityLevel(2);
            GameManager.Instance.gameQuality = 1;
        }
        else if (qualityIndex == 2)
        {
            QualitySettings.SetQualityLevel(5);
            GameManager.Instance.gameQuality = 2;
        }
    }
}
