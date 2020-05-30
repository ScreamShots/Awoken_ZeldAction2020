using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryPlateSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Fury Plate")]
    public AudioClip furyPlate;
    [Range(0f, 1f)] public float stayPlateVolume = 0.5f;

    private FuryPlate furyPlateScript;

    private bool plateActivate = false;
    #endregion

    void Start()
    {
        furyPlateScript = GetComponentInParent<FuryPlate>();
    }

    void Update()
    {
        StayPlateActivate();
    }

    void StayPlateActivate()
    {
        if (furyPlateScript.isPressed)
        {
            if (!plateActivate)
            {
                plateActivate = true;
                SoundManager.Instance.PlaySfx(furyPlate, stayPlateVolume);
            }
        }
        else
        {
            plateActivate = false;
        }
    }
}
