using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    #region Variables

    [Space]
    [Header("Select Button")]
    public AudioClip selected;
    [Range(0f, 1f)] public float selectedVolume = 0.5f;

    [Header("Press Button")]
    public AudioClip pressed;
    [Range(0f, 1f)] public float pressedVolume = 0.5f;

    [Header("Start Button")]
    public AudioClip start;
    [Range(0f, 1f)] public float startVolume = 0.5f;

    [Header("Leave Button")]
    public AudioClip leave;
    [Range(0f, 1f)] public float leaveVolume = 0.5f;

    [Header("Pause Button")]
    public AudioClip pause;
    [Range(0f, 1f)] public float pauseVolume = 0.5f;

    [Header("Slider Button")]
    public AudioClip slider;
    [Range(0f, 1f)] public float sliderVolume = 0.5f;

    private bool isPaused = false;

    #endregion

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Pause)
        {
            PauseButton();
        }
    }

    public void ClickButton()
    {
        SoundManager.Instance.PlayButton(pressed, pressedVolume);
    }

    public void SelectButton()
    {
        SoundManager.Instance.PlayButton(selected, selectedVolume);
    }

    public void StartButton()
    {
        SoundManager.Instance.PlayButton(start, startVolume);
    }

    public void QuitButton()
    {
        SoundManager.Instance.PlayButton(leave, leaveVolume);
    }

    public void PauseButton()
    {
        if (!isPaused)
        {
            isPaused = true;
            SoundManager.Instance.PlayButton(pause, pauseVolume);
        }
    }

    public void SliderButton()
    {
        SoundManager.Instance.PlayButton(slider, sliderVolume);
    }

    void OnDisable()
    {
        isPaused = false;
    }
}
