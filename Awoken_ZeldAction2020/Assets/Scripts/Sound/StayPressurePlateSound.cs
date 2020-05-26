using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StayPressurePlateSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Stay Plate")]
    public AudioClip StayPlate;
    [Range(0f, 1f)] public float stayPlateVolume = 0.5f;

    private StayPressurePlate stayPressurePlateScript;

    private bool plateActivate = false;
    #endregion

    void Start()
    {
        stayPressurePlateScript = GetComponentInParent<StayPressurePlate>();
    }

    void Update()
    {
        StayPlateActivate();
    }

    void StayPlateActivate()
    {
        if (stayPressurePlateScript.isPressed)
        {
            if (!plateActivate)
            {
                plateActivate = true;
                SoundManager.Instance.PlaySfx(StayPlate, stayPlateVolume);
            }
        }
        else
        {
            plateActivate = false;
        }
    }
}
