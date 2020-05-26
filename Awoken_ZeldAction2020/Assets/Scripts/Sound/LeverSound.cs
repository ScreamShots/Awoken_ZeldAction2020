using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LeverSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Spawning Sound")]
    public AudioClip actionLever;
    [Range(0f, 1f)] public float actionLeverVolume = 0.5f;

    private ActionLever actionLeverScript;

    private bool isActivated = false;
    #endregion

    void Start()
    {
        actionLeverScript = GetComponentInParent<ActionLever>();
    }

    void Update()
    {
        LeverActivate();
    }

    void LeverActivate()
    {
        if (actionLeverScript.isPressed)
        {
            if (!isActivated)
            {
                isActivated = true;
                SoundManager.Instance.PlaySfx(actionLever, actionLeverVolume);
            }
        }
        else
        {
            isActivated = false;
        }
    }
}
