using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSound : MonoBehaviour
{
    #region Variables
    private DialogueManager dialogueManagerScript;

    public enum enumSpeaking {Nobody, Aaron, Aaron_sleep, Aaron_shock, Aaron_jaded, Shield, Shield_angry, Shield_jaded, Shield_wary, Shield_shock, Zeus, Zeus_angry, Zeus_hide, PNJ_1, PNJ_2, PNJ_3, PNJ_4, Siren, Sheep, Sombrage, Cyclope, Grandma};
    public enumSpeaking whoSpeaking = enumSpeaking.Nobody;
    public Sprite currentSprite;

    [Space]
    [Header("Requiered Sprite")]

    public Sprite aaron = null;
    public Sprite aaronSleep = null;
    public Sprite aaronSchok = null;
    public Sprite aaronJaded = null;
    public Sprite shield = null;
    public Sprite shieldAngry = null;
    public Sprite shieldJaded = null;
    public Sprite shieldWary = null;
    public Sprite shieldShock = null;
    public Sprite zeus = null;
    public Sprite zeusAngry = null;
    public Sprite zeusHide = null;
    public Sprite pnj1 = null;
    public Sprite pnj2 = null;
    public Sprite pnj3 = null;
    public Sprite pnj4 = null;
    public Sprite siren = null;
    public Sprite sheep = null;
    public Sprite sombrage = null;
    public Sprite cyclope = null;
    public Sprite grandma = null;

    [Space]
    [Header("Dialogue Sound")]

    public AudioClip aaronVoice;
    [Range(0f, 1f)] public float aaronVoiceVolume = 0.5f;

    public AudioClip aaronVoiceSleep;
    [Range(0f, 1f)] public float aaronVoiceSleepVolume = 0.5f;

    public AudioClip aaronVoiceShock;
    [Range(0f, 1f)] public float aaronVoiceShockVolume = 0.5f;

    public AudioClip aaronVoiceJaded;
    [Range(0f, 1f)] public float aaronVoiceJadedVolume = 0.5f;

    public AudioClip shieldVoice;
    [Range(0f, 1f)] public float shieldVoiceVolume = 0.5f;

    public AudioClip shieldVoiceAngry;
    [Range(0f, 1f)] public float shieldVoiceAngryVolume = 0.5f;

    public AudioClip shieldVoiceJaded;
    [Range(0f, 1f)] public float shieldVoiceJadedVolume = 0.5f;

    public AudioClip shieldVoiceWary;
    [Range(0f, 1f)] public float shieldVoiceWaryVolume = 0.5f;

    public AudioClip shieldVoiceShock;
    [Range(0f, 1f)] public float shieldVoiceShockVolume = 0.5f;

    public AudioClip zeusVoice;
    [Range(0f, 1f)] public float zeusVoiceVolume = 0.5f;

    public AudioClip zeusVoiceAngry;
    [Range(0f, 1f)] public float zeusVoiceAngryVolume = 0.5f;

    public AudioClip zeusVoiceHide;
    [Range(0f, 1f)] public float zeusVoiceHideVolume = 0.5f;

    public AudioClip pnj1Voice;
    [Range(0f, 1f)] public float pnj1VoiceVolume = 0.5f;

    public AudioClip pnj2Voice;
    [Range(0f, 1f)] public float pnj2VoiceVolume = 0.5f;

    public AudioClip pnj3Voice;
    [Range(0f, 1f)] public float pnj3VoiceVolume = 0.5f;

    public AudioClip pnj4Voice;
    [Range(0f, 1f)] public float pnj4VoiceVolume = 0.5f;

    public AudioClip sirenVoice;
    [Range(0f, 1f)] public float sirenVoiceVolume = 0.5f;

    public AudioClip sheepVoice;
    [Range(0f, 1f)] public float sheepVoiceVolume = 0.5f;

    public AudioClip sombrageVoice;
    [Range(0f, 1f)] public float sombrageVoiceVolume = 0.5f;

    public AudioClip cyclopeVoice;
    [Range(0f, 1f)] public float cyclopeVoiceVolume = 0.5f;

    public AudioClip grandmaVoice;
    [Range(0f, 1f)] public float grandmaVoiceVolume = 0.5f;


    private bool aaronIsSpeaking = false;
    private bool aaronSleepIsSpeaking = false;
    private bool aaronShockIsSpeaking = false;
    private bool aaronJadedIsSpeaking = false;
    private bool shieldIsSpeaking = false;
    private bool shieldAngryIsSpeaking = false;
    private bool shieldJadedIsSpeaking = false;
    private bool shieldWaryIsSpeaking = false;
    private bool shieldShockIsSpeaking = false;
    private bool zeusIsSpeaking = false;
    private bool zeusAngryIsSpeaking = false;
    private bool zeusHideIsSpeaking = false;
    private bool pnj1IsSpeaking = false;
    private bool pnj2IsSpeaking = false;
    private bool pnj3IsSpeaking = false;
    private bool pnj4IsSpeaking = false;
    private bool sirenIsSpeaking = false;
    private bool sheepIsSpeaking = false;
    private bool sombrageIsSpeaking = false;
    private bool cyclopeIsSpeaking = false;
    private bool grandmaIsSpeaking = false;

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
            else if (currentSprite == aaronJaded)
            {
                whoSpeaking = enumSpeaking.Aaron_jaded;
            }
            else if (currentSprite == shield)
            {
                whoSpeaking = enumSpeaking.Shield;
            }
            else if (currentSprite == shieldAngry)
            {
                whoSpeaking = enumSpeaking.Shield_angry;
            }
            else if (currentSprite == shieldJaded)
            {
                whoSpeaking = enumSpeaking.Shield_jaded;
            }
            else if (currentSprite == shieldWary)
            {
                whoSpeaking = enumSpeaking.Shield_wary;
            }
            else if (currentSprite == shieldShock)
            {
                whoSpeaking = enumSpeaking.Shield_shock;
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
            else if (currentSprite == siren)
            {
                whoSpeaking = enumSpeaking.Siren;
            }
            else if (currentSprite == sheep)
            {
                whoSpeaking = enumSpeaking.Sheep;
            }
            else if (currentSprite == sombrage)
            {
                whoSpeaking = enumSpeaking.Sombrage;
            }
            else if (currentSprite == cyclope)
            {
                whoSpeaking = enumSpeaking.Cyclope;
            }
            else if (currentSprite == grandma)
            {
                whoSpeaking = enumSpeaking.Grandma;
            }
        }
        else
        {
            whoSpeaking = enumSpeaking.Nobody;

            aaronIsSpeaking = false;
            aaronSleepIsSpeaking = false;
            aaronShockIsSpeaking = false;
            aaronJadedIsSpeaking = false;
            shieldIsSpeaking = false;
            shieldAngryIsSpeaking = false;
            shieldJadedIsSpeaking = false;
            shieldWaryIsSpeaking = false;
            shieldShockIsSpeaking = false;
            zeusIsSpeaking = false;
            zeusAngryIsSpeaking = false;
            zeusHideIsSpeaking = false;
            pnj1IsSpeaking = false;
            pnj2IsSpeaking = false;
            pnj3IsSpeaking = false;
            pnj4IsSpeaking = false;
            sirenIsSpeaking = false;
            sheepIsSpeaking = false;
            sombrageIsSpeaking = false;
            cyclopeIsSpeaking = false;
            grandmaIsSpeaking = false;
}
    }

    void SetSpeakingToTrue(int numberSpeaker)
    {
        if (numberSpeaker == 1) { aaronIsSpeaking = true; } else { aaronIsSpeaking = false; }
        if (numberSpeaker == 2) { aaronSleepIsSpeaking = true; } else { aaronSleepIsSpeaking = false; }
        if (numberSpeaker == 3) { aaronShockIsSpeaking = true; } else { aaronShockIsSpeaking = false; }
        if (numberSpeaker == 4) { aaronJadedIsSpeaking = true; } else { aaronJadedIsSpeaking = false; }
        if (numberSpeaker == 5) { shieldIsSpeaking = true; } else { shieldIsSpeaking = false; }
        if (numberSpeaker == 6) { shieldAngryIsSpeaking = true; } else { shieldAngryIsSpeaking = false; }
        if (numberSpeaker == 7) { shieldJadedIsSpeaking = true; } else { shieldJadedIsSpeaking = false; }
        if (numberSpeaker == 8) { shieldWaryIsSpeaking = true; } else { shieldWaryIsSpeaking = false; }
        if (numberSpeaker == 9) { shieldShockIsSpeaking = true; } else { shieldShockIsSpeaking = false; }
        if (numberSpeaker == 10) { zeusIsSpeaking = true; } else { zeusIsSpeaking = false; }
        if (numberSpeaker == 11) { zeusAngryIsSpeaking = true; } else { zeusAngryIsSpeaking = false; }
        if (numberSpeaker == 12) { zeusHideIsSpeaking = true; } else { zeusHideIsSpeaking = false; }
        if (numberSpeaker == 13) { pnj1IsSpeaking = true; } else { pnj1IsSpeaking = false; }
        if (numberSpeaker == 14) { pnj2IsSpeaking = true; } else { pnj2IsSpeaking = false; }
        if (numberSpeaker == 15) { pnj3IsSpeaking = true; } else { pnj3IsSpeaking = false; }
        if (numberSpeaker == 16) { pnj4IsSpeaking = true; } else { pnj4IsSpeaking = false; }
        if (numberSpeaker == 17) { sirenIsSpeaking = true; } else { sirenIsSpeaking = false; }
        if (numberSpeaker == 18) { sheepIsSpeaking = true; } else { sheepIsSpeaking = false; }
        if (numberSpeaker == 19) { sombrageIsSpeaking = true; } else { sombrageIsSpeaking = false; }
        if (numberSpeaker == 20) { cyclopeIsSpeaking = true; } else { cyclopeIsSpeaking = false; }
        if (numberSpeaker == 21) { grandmaIsSpeaking = true; } else { grandmaIsSpeaking = false; }
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
                        SetSpeakingToTrue(1);
                        SoundManager.Instance.PlayVoice(aaronVoice, aaronVoiceVolume);
                        currentVoiceVolume = aaronVoiceVolume;
                    }
                    break;
                case enumSpeaking.Aaron_sleep:
                    if (!aaronSleepIsSpeaking)
                    {
                        SetSpeakingToTrue(2);
                        SoundManager.Instance.PlayVoice(aaronVoiceSleep, aaronVoiceSleepVolume);
                        currentVoiceVolume = aaronVoiceSleepVolume;
                    }
                    break;
                case enumSpeaking.Aaron_shock:
                    if (!aaronShockIsSpeaking)
                    {
                        SetSpeakingToTrue(3);
                        SoundManager.Instance.PlayVoice(aaronVoiceShock, aaronVoiceShockVolume);
                        currentVoiceVolume = aaronVoiceShockVolume;
                    }
                    break;
                case enumSpeaking.Aaron_jaded:
                    if (!aaronJadedIsSpeaking)
                    {
                        SetSpeakingToTrue(4);
                        SoundManager.Instance.PlayVoice(aaronVoiceJaded, aaronVoiceJadedVolume);
                        currentVoiceVolume = aaronVoiceJadedVolume;
                    }
                    break;
                case enumSpeaking.Shield:
                    if (!shieldIsSpeaking)
                    {
                        SetSpeakingToTrue(5);
                        SoundManager.Instance.PlayVoice(shieldVoice, shieldVoiceVolume);
                        currentVoiceVolume = shieldVoiceVolume;
                    }
                    break;
                case enumSpeaking.Shield_angry:
                    if (!shieldAngryIsSpeaking)
                    {
                        SetSpeakingToTrue(6);
                        SoundManager.Instance.PlayVoice(shieldVoiceAngry, shieldVoiceAngryVolume);
                        currentVoiceVolume = shieldVoiceAngryVolume;
                    }
                    break;
                case enumSpeaking.Shield_jaded:
                    if (!shieldJadedIsSpeaking)
                    {
                        SetSpeakingToTrue(7);
                        SoundManager.Instance.PlayVoice(shieldVoiceJaded, shieldVoiceJadedVolume);
                        currentVoiceVolume = shieldVoiceJadedVolume;
                    }
                    break;
                case enumSpeaking.Shield_wary:
                    if (!shieldWaryIsSpeaking)
                    {
                        SetSpeakingToTrue(8);
                        SoundManager.Instance.PlayVoice(shieldVoiceWary, shieldVoiceWaryVolume);
                        currentVoiceVolume = shieldVoiceWaryVolume;
                    }
                    break;
                case enumSpeaking.Shield_shock:
                    if (!shieldShockIsSpeaking)
                    {
                        SetSpeakingToTrue(9);
                        SoundManager.Instance.PlayVoice(shieldVoiceShock, shieldVoiceShockVolume);
                        currentVoiceVolume = shieldVoiceShockVolume;
                    }
                    break;
                case enumSpeaking.Zeus:
                    if (!zeusIsSpeaking)
                    {
                        SetSpeakingToTrue(10);
                        SoundManager.Instance.PlayVoice(zeusVoice, zeusVoiceVolume);
                        currentVoiceVolume = zeusVoiceVolume;
                    }
                    break;
                case enumSpeaking.Zeus_angry:
                    if (!zeusAngryIsSpeaking)
                    {
                        SetSpeakingToTrue(11);
                        SoundManager.Instance.PlayVoice(zeusVoiceAngry, zeusVoiceAngryVolume);
                        currentVoiceVolume = zeusVoiceAngryVolume;

                    }
                    break;
                case enumSpeaking.Zeus_hide:
                    if (!zeusHideIsSpeaking)
                    {
                        SetSpeakingToTrue(12);
                        SoundManager.Instance.PlayVoice(zeusVoiceHide, zeusVoiceHideVolume);
                        currentVoiceVolume = zeusVoiceHideVolume;
                    }
                    break;
                case enumSpeaking.PNJ_1:
                    if (!pnj1IsSpeaking)
                    {
                        SetSpeakingToTrue(13);
                        SoundManager.Instance.PlayVoice(pnj1Voice, pnj1VoiceVolume);
                        currentVoiceVolume = pnj1VoiceVolume;
                    }
                    break;
                case enumSpeaking.PNJ_2:
                    if (!pnj2IsSpeaking)
                    {
                        SetSpeakingToTrue(14);
                        SoundManager.Instance.PlayVoice(pnj2Voice, pnj2VoiceVolume);
                        currentVoiceVolume = pnj2VoiceVolume;
                    }
                    break;
                case enumSpeaking.PNJ_3:
                    if (!pnj3IsSpeaking)
                    {
                        SetSpeakingToTrue(15);
                        SoundManager.Instance.PlayVoice(pnj3Voice, pnj3VoiceVolume);
                        currentVoiceVolume = pnj3VoiceVolume;
                    }
                    break;
                case enumSpeaking.PNJ_4:
                    if (!pnj4IsSpeaking)
                    {
                        SetSpeakingToTrue(16);
                        SoundManager.Instance.PlayVoice(pnj4Voice, pnj4VoiceVolume);
                        currentVoiceVolume = pnj4VoiceVolume;
                    }
                    break;
                case enumSpeaking.Siren:
                    if (!sirenIsSpeaking)
                    {
                        SetSpeakingToTrue(17);
                        SoundManager.Instance.PlayVoice(sirenVoice, sirenVoiceVolume);
                        currentVoiceVolume = sirenVoiceVolume;
                    }
                    break;
                case enumSpeaking.Sheep:
                    if (!sheepIsSpeaking)
                    {
                        SetSpeakingToTrue(18);
                        SoundManager.Instance.PlayVoice(sheepVoice, sheepVoiceVolume);
                        currentVoiceVolume = sheepVoiceVolume;
                    }
                    break;
                case enumSpeaking.Sombrage:
                    if (!sombrageIsSpeaking)
                    {
                        SetSpeakingToTrue(19);
                        SoundManager.Instance.PlayVoice(sombrageVoice, sombrageVoiceVolume);
                        currentVoiceVolume = sombrageVoiceVolume;
                    }
                    break;
                case enumSpeaking.Cyclope:
                    if (!cyclopeIsSpeaking)
                    {
                        SetSpeakingToTrue(20);
                        SoundManager.Instance.PlayVoice(cyclopeVoice, cyclopeVoiceVolume);
                        currentVoiceVolume = cyclopeVoiceVolume;
                    }
                    break;
                case enumSpeaking.Grandma:
                    if (!grandmaIsSpeaking)
                    {
                        SetSpeakingToTrue(21);
                        SoundManager.Instance.PlayVoice(grandmaVoice, grandmaVoiceVolume);
                        currentVoiceVolume = grandmaVoiceVolume;
                    }
                    break;
            }
        }
    }
}
