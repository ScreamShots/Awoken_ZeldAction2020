using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ShockWaveSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("ShockWave Sound")]
    public AudioClip ShockWaveInstantiate;
    [Range(0f, 1f)] public float ShockWaveInstantiateVolume = 0.5f;

    public AudioSource ShockWaveAlive;
    [Range(0f, 1f)] public float ShockWaveAliveVolume = 0.5f;

    private bool ShockWaveLauched = false;
    private bool ShockWaveIsAlive = false;

    #endregion

    void Start()
    {
        LauchShockWave();
        SchockWaveDuration();

        ShockWaveAlive.volume = ShockWaveAliveVolume;
    }

    void LauchShockWave()
    {
        if (!ShockWaveLauched)
        {
            ShockWaveLauched = true;
            SoundManager.Instance.PlaySfx(ShockWaveInstantiate, ShockWaveInstantiateVolume);
        }
    }

    void SchockWaveDuration()
    {
        if (!ShockWaveIsAlive)
        {
            ShockWaveIsAlive = true;
            SoundManager.Instance.PlayCubePushed(ShockWaveAlive, ShockWaveAliveVolume);
        }
    }

    void OnDestroy()
    {
        SoundManager.Instance.StopCubePushed(ShockWaveAlive);
    }
}
