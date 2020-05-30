using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSound : MonoBehaviour
{
    #region Variables
    private DialogueManager dialogueManagerScript;

    public enum enumSpeaking {Nobody, Aaron, Aaron_sleep, Aaron_shock, Shield, Shield_angry, Zeus, Zeus_angry, Zeus_hide, PNJ_1, PNJ_2, PNJ_3, PNJ_4};
    public enumSpeaking whoSpeaking = enumSpeaking.Nobody;
    public Sprite currentSprite;

    [Space]
    [Header("Requiered Sprite")]

    public Sprite aaron = null;
    public Sprite aaronSleep = null;
    public Sprite aaronSchok = null;
    public Sprite shield = null;
    public Sprite shieldAngry = null;
    public Sprite zeus = null;
    public Sprite zeusAngry = null;
    public Sprite zeusHide = null;
    public Sprite pnj1 = null;
    public Sprite pnj2 = null;
    public Sprite pnj3 = null;
    public Sprite pnj4 = null;

    [Space]
    [Header("Dialogue Sound")]

    public AudioClip aaronVoice;
    [Range(0f, 1f)] public float aaronVoiceVolume = 0.5f;

    public AudioClip aaronVoiceSleep;
    [Range(0f, 1f)] public float aaronVoiceSleepVolume = 0.5f;

    public AudioClip aaronVoiceShock;
    [Range(0f, 1f)] public float aaronVoiceShockVolume = 0.5f;

    public AudioClip shieldVoice;
    [Range(0f, 1f)] public float shieldVoiceVolume = 0.5f;

    public AudioClip shieldVoiceAngry;
    [Range(0f, 1f)] public float shieldVoiceAngryVolume = 0.5f;

    public AudioClip zeusVoice;
    [Range(0f, 1f)] public float zeusVoiceVolume = 0.5f;

    public AudioClip zeusVoiceAngry;
    [Range(0f, 1f)] public float zeusVoiceAngryVolume = 0.5f;

    public AudioClip zeusVoiceHide;
    [Range(0f, 1f)] public float zeusVoiceHideVolume = 0.5f;

    public AudioClip pnjVoice;
    [Range(0f, 1f)] public float pnjVoiceVolume = 0.5f;

    private bool aaronIsSpeaking = false;
    private bool aaronSleepIsSpeaking = false;
    private bool aaronShockIsSpeaking = false;
    private bool shieldIsSpeaking = false;
    private bool shieldAngryIsSpeaking = false;
    private bool zeusIsSpeaking = false;
    private bool zeusAngryIsSpeaking = false;
    private bool zeusHideIsSpeaking = false;
    private bool pnj1IsSpeaking = false;
    private bool pnj2IsSpeaking = false;
    private bool pnj3IsSpeaking = false;
    private bool pnj4IsSpeaking = false;

    #endregion

    public static DialogueSound Instance;

    [HideInInspector] public float currentVoiceVolume;

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    void Start()
    {
        dialogueManagerScript = GetComponent<DialogueManager>();
    }

    void Update()
    {
        ImageActive();
        WhoSpeak();
        PlaySpeakSound();
    }

    void ImageActive()
    {
        if (dialogueManagerScript.currentDialogue != null && dialogueManagerScript.dialogueWithFace)
        {
            if (dialogueManagerScript.currentDialoguePos == Dialogue.DialogueUIPos.Down)
            {
                currentSprite = dialogueManagerScript.faceDisplayDown.sprite;
            }
            else
            {
                currentSprite = dialogueManagerScript.faceDisplayUp.sprite;
            }
        }
        else
        {
            currentSprite = null;
        }
    }

    void WhoSpeak()
    {
        if (dialogueManagerScript.currentDialogue != null)
        {
            if (currentSprite == aaron)
            {
                whoSpeaking = enumSpeaking.Aaron;
            }
            else if (currentSprite == aaronSleep)
            {
                whoSpeaking = enumSpeaking.Aaron_sleep;
            }
            else if (currentSprite == aaronSchok)
            {
                whoSpeaking = enumSpeaking.Aaron_shock;
            }
            else if (currentSprite == shield)
            {
                whoSpeaking = enumSpeaking.Shield;
            }
            else if (currentSprite == shieldAngry)
            {
                whoSpeaking = enumSpeaking.Shield_angry;
            }
            else if (currentSprite == zeus)
            {
                whoSpeaking = enumSpeaking.Zeus;
            }
            else if (currentSprite == zeusAngry)
            {
                whoSpeaking = enumSpeaking.Zeus_angry;
            }
            else if (currentSprite == zeusHide)
            {
                whoSpeaking = enumSpeaking.Zeus_hide;
            }
            else if (currentSprite == pnj1)
            {
                whoSpeaking = enumSpeaking.PNJ_1;
            }
            else if (currentSprite == pnj2)
            {
                whoSpeaking = enumSpeaking.PNJ_2;
            }
            else if (currentSprite == pnj3)
            {
                whoSpeaking = enumSpeaking.PNJ_3;
            }
            else if (currentSprite == pnj4)
            {
                whoSpeaking = enumSpeaking.PNJ_4;
            }        
        }
        else
        {
            whoSpeaking = enumSpeaking.Nobody;

            aaronIsSpeaking = false;
            aaronSleepIsSpeaking = false;
            aaronShockIsSpeaking = false;
            shieldIsSpeaking = false;
            shieldAngryIsSpeaking = false;
            zeusIsSpeaking = false;
            zeusAngryIsSpeaking = false;
            zeusHideIsSpeaking = false;
            pnj1IsSpeaking = false;
            pnj2IsSpeaking = false;
            pnj3IsSpeaking = false;
            pnj4IsSpeaking = false;
        }
    }

    void PlaySpeakSound()
    {
        if (dialogueManagerScript.currentDialogue != null)
        {
            switch (whoSpeaking)
            {
                case enumSpeaking.Aaron:
                    if (!aaronIsSpeaking)
                    {
                        aaronIsSpeaking = true;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(aaronVoice, aaronVoiceVolume);
                        currentVoiceVolume = aaronVoiceVolume;
                    }
                    break;
                case enumSpeaking.Aaron_sleep:
                    if (!aaronSleepIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = true;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(aaronVoiceSleep, aaronVoiceSleepVolume);
                        currentVoiceVolume = aaronVoiceSleepVolume;
                    }
                    break;
                case enumSpeaking.Aaron_shock:
                    if (!aaronShockIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = true;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(aaronVoiceShock, aaronVoiceShockVolume);
                        currentVoiceVolume = aaronVoiceShockVolume;
                    }
                    break;
                case enumSpeaking.Shield:
                    if (!shieldIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = true;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(shieldVoice, shieldVoiceVolume);
                        currentVoiceVolume = shieldVoiceVolume;
                    }
                    break;
                case enumSpeaking.Shield_angry:
                    if (!shieldAngryIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = true;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(shieldVoiceAngry, shieldVoiceAngryVolume);
                        currentVoiceVolume = shieldVoiceAngryVolume;
                    }
                    break;
                case enumSpeaking.Zeus:
                    if (!zeusIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = true;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(zeusVoice, zeusVoiceVolume);
                        currentVoiceVolume = zeusVoiceVolume;
                    }
                    break;
                case enumSpeaking.Zeus_angry:
                    if (!zeusAngryIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = true;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(zeusVoiceAngry, zeusVoiceAngryVolume);
                        currentVoiceVolume = zeusVoiceAngryVolume;

                    }
                    break;
                case enumSpeaking.Zeus_hide:
                    if (!zeusHideIsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = true;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(zeusVoiceHide, zeusVoiceHideVolume);
                        currentVoiceVolume = zeusVoiceHideVolume;
                    }
                    break;
                case enumSpeaking.PNJ_1:
                    if (!pnj1IsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = true;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(pnjVoice, pnjVoiceVolume);
                        currentVoiceVolume = pnjVoiceVolume;
                    }
                    break;
                case enumSpeaking.PNJ_2:
                    if (!pnj2IsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = true;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(pnjVoice, pnjVoiceVolume);
                        currentVoiceVolume = pnjVoiceVolume;
                    }
                    break;
                case enumSpeaking.PNJ_3:
                    if (!pnj3IsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = true;
                        pnj4IsSpeaking = false;
                        SoundManager.Instance.PlayVoice(pnjVoice, pnjVoiceVolume);
                        currentVoiceVolume = pnjVoiceVolume;
                    }
                    break;
                case enumSpeaking.PNJ_4:
                    if (!pnj4IsSpeaking)
                    {
                        aaronIsSpeaking = false;
                        aaronSleepIsSpeaking = false;
                        aaronShockIsSpeaking = false;
                        shieldIsSpeaking = false;
                        shieldAngryIsSpeaking = false;
                        zeusIsSpeaking = false;
                        zeusAngryIsSpeaking = false;
                        zeusHideIsSpeaking = false;
                        pnj1IsSpeaking = false;
                        pnj2IsSpeaking = false;
                        pnj3IsSpeaking = false;
                        pnj4IsSpeaking = true;
                        SoundManager.Instance.PlayVoice(pnjVoice, pnjVoiceVolume);
                        currentVoiceVolume = pnjVoiceVolume;
                    }
                    break;
            }
        }
    }
}
