using UnityEngine.Audio;
using System;
using UnityEngine;

/// <summary>
/// Créateur: Steve Guitton
/// This script is a copy of the global sound Manager for specific items and ennemies
public class PrefabSoundManager : MonoBehaviour
{

    public SoundProperties[] sounds;
    public static PrefabSoundManager Instance;

    private void Awake()
    {

        foreach (SoundProperties soundProperties in sounds) //Get the datas from SoundProperties
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