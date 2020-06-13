using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowValueSlider : MonoBehaviour
{
    private TextMeshProUGUI percentageText = null;
    Slider sliderScript = null;

    public enum enumVolume { Sfx, Music, Voice };
    public enumVolume whichVolume;

    void Start()
    {
        percentageText = GetComponent<TextMeshProUGUI>();

        sliderScript = GetComponentInParent<Slider>();
        percentageText.text = (sliderScript.value * 100) + "%";

        if (whichVolume == enumVolume.Sfx)
        {
            sliderScript.value = SoundManager.Instance.sfxDefaultVolume;
        }
        else if (whichVolume == enumVolume.Music)
        {
            sliderScript.value = SoundManager.Instance.musicDefaultVolume;
        }
        else if (whichVolume == enumVolume.Voice)
        {
            sliderScript.value = SoundManager.Instance.voiceDefaultVolume;
        }
    }

    public void textUpdate(float value)
    {
        if (gameObject.activeInHierarchy)
        {
            percentageText.text = Mathf.RoundToInt(value * 100) + "%";
        }        
    }
}
