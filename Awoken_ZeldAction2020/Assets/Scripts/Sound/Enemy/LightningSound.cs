using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LightningSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Thunder Sound")]
    public AudioClip thunderInstantiate;
    [Range(0f, 1f)] public float thunderInstantiateVolume = 0.5f;

    public AudioClip thunderStruck;
    [Range(0f, 1f)] public float thunderStruckVolume = 0.5f;

    private LightningComportement lightningComportementScript;

    private bool ThunderIsStruck = false;
    private bool ThunderIsLauch = false;

    #endregion

    void Start()
    {
        lightningComportementScript = GetComponentInParent<LightningComportement>();

        ThunderLauch();
    }

    void Update()
    {
        TunderStrucked();
    }

    void ThunderLauch()
    {
        if (!ThunderIsLauch)
        {
            ThunderIsLauch = true;
            SoundManager.Instance.PlaySfx(thunderInstantiate, thunderInstantiateVolume);
        }
    }

    void TunderStrucked()
    {
        if (lightningComportementScript.thunderIsSlam)
        {
            if (!ThunderIsStruck)
            {
                ThunderIsStruck = true;
                SoundManager.Instance.PlaySfx(thunderStruck, thunderStruckVolume);
            }
        }
        else
        {
            ThunderIsStruck = false;
        }
    }
}
