using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InstantPressurePlateSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Instant Plate")]
    public AudioClip instantPlate;
    [Range(0f, 1f)] public float instantPlateVolume = 0.5f;

    private InstantPressurePlate instantPressurePlateScript;

    private bool plateActivate = false;
    #endregion

    void Start()
    {
        instantPressurePlateScript = GetComponentInParent<InstantPressurePlate>();
    }

    void Update()
    {
        InstantPlateActivate();
    }

    void InstantPlateActivate()
    {
        if (instantPressurePlateScript.isPressed)
        {
            if (!plateActivate)
            {
                plateActivate = true;
                SoundManager.Instance.PlaySfx(instantPlate, instantPlateVolume);
            }
        }
        else
        {
            plateActivate = false;
        }
    }
}
