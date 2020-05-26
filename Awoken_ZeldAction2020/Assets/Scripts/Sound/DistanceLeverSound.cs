using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DistanceLeverSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Spawning Sound")]
    public AudioClip distanceLever;
    [Range(0f, 1f)] public float distanceLeverVolume = 0.5f;

    private DistanceLever distanceLeverScript;

    private bool isActivated = false;
    #endregion

    void Start()
    {
        distanceLeverScript = GetComponentInParent<DistanceLever>();
    }

    void Update()
    {
        LeverActivate();
    }

    void LeverActivate()
    {
        if (distanceLeverScript.isPressed)
        {
            if (!isActivated)
            {
                isActivated = true;
                SoundManager.Instance.PlaySfx(distanceLever, distanceLeverVolume);
            }
        }
        else
        {
            isActivated = false;
        }
    }
}
