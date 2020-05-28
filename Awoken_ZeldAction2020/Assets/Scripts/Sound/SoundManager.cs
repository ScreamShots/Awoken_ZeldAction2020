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
    public AudioSource parrySource;

    private bool fadeOut = false;
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

    // Start playing ambiance sound.
    public void PlayAmbiance(AudioClip ambiance, float volume = 1f)
    {
        ambianceSource.Stop();

        ambianceSource.clip = ambiance;
        ambianceSource.volume = (ambiancesDefaultVolume * volume) * globalDefaultVolume;
        ambianceSource.Play();

        return;
    }

    // Stop all ambiance sound.
    public void StopAmbiance()
    {
        ambianceSource.Stop();
    }

    // Start playing a given music.
    public void PlayMusic(AudioClip music, float volume = 1f)
    {
        musicSource.clip = music;
        musicSource.volume = (musicDefaultVolume * volume) * globalDefaultVolume;
        musicSource.Play();

        return;
    }

    // Fade in a given music.
    public void FadeInMusic(AudioClip music, float volume = 1f, float fadeTime = 1f)
    {
        StopAllCoroutines();
        fadeOut = false;

        musicSource.Stop();
        StartCoroutine(FadeIn(music, volume, fadeTime));

        return;
    }

    // Fade out a given music.
    public void FadeOutMusic(float fadeTime = 1f)
    {
        StopAllCoroutines();
        fadeOut = false;

        StartCoroutine(FadeOut(fadeTime));

        return;
    }

    // Fade out and Fade in a given music.
    public void FadeOutFadeInMusic(AudioClip music, float volume = 1f, float fadeInTime = 1f, float fadeOutTime = 1f)
    {
        StopAllCoroutines();
        fadeOut = false;

        StartCoroutine(FadeOutThenFadeIn(music, volume, fadeInTime, fadeOutTime));

        return;
    }

    // Plays a given sfx. Specific volume and pitch can be specified in parameters.
    public void PlaySfx(AudioClip sfx, float volume = 1f, float pitch = 1f)
    {
        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(sfx, (sfxDefaultVolume * volume) * globalDefaultVolume);

        sfxSource.pitch = 1;

        return;
    }

    // Plays a given sfx 3D. Specific 3D settings can be specified in GameObject AudioSource.
    public void PlaySfx3D(AudioSource sfx3D, float volume = 1f, float pitch = 1f)
    {
        sfx3D.pitch = pitch;
        sfx3D.PlayOneShot(sfx3D.clip, (sfxDefaultVolume * volume) * globalDefaultVolume);

        sfx3D.pitch = 1;

        return;
    }

    // Plays a random sfx from a list.
    public void PlayRandomSfx(AudioClip[] sfxRandom, float volume = 1f, float pitch = 1f)
    {
        int randomIndex = Random.Range(0, sfxRandom.Length);

        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(sfxRandom[randomIndex], (sfxDefaultVolume * volume) * globalDefaultVolume);

        sfxSource.pitch = 1;

        return;
    }

    // Plays footSteps only once.  
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

    // Plays cube pushed only once.  
    public void PlayCubePushed(AudioSource cubePushSource, float volume = 1f, float pitch = 1f)
    {
        cubePushSource.pitch = pitch;
        if (!cubePushSource.isPlaying)
        {
            cubePushSource.PlayOneShot(cubePushSource.clip, (sfxDefaultVolume * volume) * globalDefaultVolume);
        }

        cubePushSource.pitch = 1;

        return;
    }

    // Stop cube pushed sound.  
    public void StopCubePushed(AudioSource cubePushSource)
    {
        cubePushSource.Stop();

        return;
    }

    // Plays parry sound.  
    public void PlayParry(AudioClip parry, float volume = 1f, float pitch = 1f)
    {
        parrySource.pitch = pitch;
        parrySource.PlayOneShot(parry, (sfxDefaultVolume * volume) * globalDefaultVolume);

        parrySource.pitch = 1;

        return;
    }

    // Stop parry sound.  
    public void StopParry()
    {
        parrySource.Stop();

        return;
    }

// = = =

// = = = [ IENUMERATOR METHODS ] = = =

    IEnumerator FadeOut(float fadeTime)
    {
        fadeOut = true;
        float currentVolume = musicSource.volume;
        float currentTime = 0;
        while (currentTime < fadeTime)
        {
            currentTime += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(currentVolume, 0, currentTime / fadeTime);

            yield return null;
        }
        musicSource.Stop();
        fadeOut = false;
    }

    IEnumerator FadeIn(AudioClip music, float volume, float fadeTime)
    {
        PlayMusic(music, volume);
        musicSource.volume = 0f;

        float currentTime = 0;
        while(currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, (musicDefaultVolume * volume) * globalDefaultVolume, currentTime / fadeTime);

            yield return null;
        }
    }

    IEnumerator FadeOutThenFadeIn(AudioClip music, float volume, float fadeInTime, float fadeOutTime)
    {
        StartCoroutine(FadeOut(fadeOutTime));
        yield return new WaitUntil(() => fadeOut == false);
        StartCoroutine(FadeIn(music, volume, fadeInTime));
    }

// = = =

}
