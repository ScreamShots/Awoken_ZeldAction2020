using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Made by Antoine
/// New version of Sound Manager
/// </summary>

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

// = = = [ VARIABLES DEFINITION ] = = =

    #region Inspector Settings
    [Space]
    [Header("Global")]
    [Range(0f, 1f)] public float globalDefaultVolume = 0.5f;

    [Space]
    [Header("Musics")]
    [Range(0f, 1f)] public float musicDefaultVolume = 0.5f;

    [Space]
    [Header("Ambiances")]
    [Range(0f, 1f)] public float ambiancesDefaultVolume = 0.5f;

    [Space]
    [Header("SFX")]
    [Range(0f, 1f)] public float sfxDefaultVolume = 0.5f;

    [Space]
    [Header("References")]
    public AudioSource musicSource;
    public AudioSource ambianceSource;
    public AudioSource sfxSource;
    public AudioSource footStepsSource;
    #endregion

 // = = =

 // = = = [ MONOBEHAVIOR METHODS ] = = =

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

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Start playing a given music.
    /// </summary>
    public void PlayMusic(AudioClip music, float volume = 1f)
    {
        musicSource.clip = music;
        musicSource.volume = (musicDefaultVolume * volume) * globalDefaultVolume;

        musicSource.Play();

        return;
    }

    /// <summary>
    /// Plays a given sfx. Specific volume and pitch can be specified in parameters.
    /// </summary>
    public void PlaySfx(AudioClip sfx, float volume = 1f, float pitch = 1f)
    {
        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(sfx, (sfxDefaultVolume * volume) * globalDefaultVolume);

        sfxSource.pitch = 1;

        return;
    }

    /// <summary>
    /// Plays a given sfx 3D. Specific 3D settings can be specified in GameObject AudioSource.
    /// </summary>
    public void PlaySfx3D(AudioSource sfx3D, float volume = 1f, float pitch = 1f)
    {
        sfx3D.pitch = pitch;
        sfx3D.PlayOneShot(sfx3D.clip, (sfxDefaultVolume * volume) * globalDefaultVolume);

        sfx3D.pitch = 1;

        return;
    }

    /// <summary>
    /// Plays a random sfx from a list.
    /// </summary>
    public void PlayRandomSfx(AudioClip[] sfxRandom, float volume = 1f, float pitch = 1f)
    {
        int randomIndex = Random.Range(0, sfxRandom.Length);

        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(sfxRandom[randomIndex], (sfxDefaultVolume * volume) * globalDefaultVolume);

        sfxSource.pitch = 1;

        return;
    }

    /// <summary>
    /// Plays footSteps only once.  
    /// </summary>
    public void PlayFootSteps(AudioClip footSteps, float volume = 1f, float pitch = 1f)
    {
        footStepsSource.pitch = pitch;
        if (!footStepsSource.isPlaying)
        {
            footStepsSource.PlayOneShot(footSteps, (sfxDefaultVolume * volume) * globalDefaultVolume);
        }

        footStepsSource.pitch = 1;

        return;
    }

 // = = =
}
