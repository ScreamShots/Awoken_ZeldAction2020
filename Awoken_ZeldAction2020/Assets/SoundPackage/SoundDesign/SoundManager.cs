using UnityEngine.Audio;
using System;
using UnityEngine;

/// <summary>
/// Créateur: Steve Guitton
/// This script is used to have a list of sounds that allow us to add or remove sounds proggresively
/// Each sound have different properties: an AudioClip, Volumme, Pitch, Loop.
/// When we start the game, we go through the list, for each object we add an AudioSource with the right parameters.
/// When we want to play a sound we call a Play() methods in the Audio Manager by putting the name of the sound we want to play, the Audio Manager will find the source with the name and will play the sound
/// </summary>
public class SoundManager : MonoBehaviour
{

    public SoundProperties[] sounds;
    public static SoundManager Instance;
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

        DontDestroyOnLoad(gameObject);

        foreach (SoundProperties soundProperties in sounds)
        {
            soundProperties.source = gameObject.AddComponent<AudioSource>();
            soundProperties.source.clip = soundProperties.clip;

            soundProperties.source.volume = soundProperties.volume;
            soundProperties.source.pitch = soundProperties.pitch;
            soundProperties.source.loop = soundProperties.loop;
        }

    }



    public void Play(string name) //A fonction to play the corresponding sound when called for.
    {
        SoundProperties soundProperties = Array.Find(sounds, sound => sound.name == name);
        if (soundProperties == null)
        {
            Debug.LogWarning("Sound :" + name + " not found!");
            return;
        }
        soundProperties.source.Play();
    }

    public void PlayOnlyOnce(string name) //A fonction to play the corresponding sound when called for once.
    {
        SoundProperties soundProperties = Array.Find(sounds, sound => sound.name == name);
        if (soundProperties == null)
        {
            Debug.LogWarning("Sound :" + name + " not found!");
            return;
        }

        if (!soundProperties.isAlreadyPlayed)
        {
            soundProperties.source.Play();
            soundProperties.isAlreadyPlayed = true;
        }
    }

    public void Stop(string name) //A fonction to play the corresponding sound when called for.
    {
        SoundProperties soundProperties = Array.Find(sounds, sound => sound.name == name);
        if (soundProperties == null)
        {
            Debug.LogWarning("Sound :" + name + " not found!");
            return;
        }
        soundProperties.source.Stop();
        soundProperties.isAlreadyPlayed = false;
    }
}

