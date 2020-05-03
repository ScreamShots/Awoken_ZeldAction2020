using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// Créateur: Steve Guitton
/// This script manage to control the datas stocked in each sounds
/// </summary>

[System.Serializable]
public class SoundProperties
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool isAlreadyPlayed;

    [HideInInspector]
    public AudioSource source;
}
