﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Variables
    public SoundProperties[] music;
    public SoundProperties[] ambiance;
    public AudioSource[] audioSource;
    public static MusicManager instance;
    public SoundProperties currentMusic;
    public SoundProperties currentAmbiance;
    [Range(0f, 1f)]
    public float MusicVolume;
    private Scene currentScene;
    public string sceneName;
    #endregion

    private void Awake()
    {
        #region Make Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        foreach (SoundProperties soundProperties in music)
        {
            soundProperties.source = gameObject.AddComponent<AudioSource>();
            soundProperties.source.clip = soundProperties.clip;

            soundProperties.source.volume = soundProperties.volume;
            soundProperties.source.pitch = soundProperties.pitch;
            soundProperties.source.loop = soundProperties.loop;
        }

        foreach (SoundProperties soundProperties in ambiance)
        {
            soundProperties.source = gameObject.AddComponent<AudioSource>();
            soundProperties.source.clip = soundProperties.clip;

            soundProperties.source.volume = soundProperties.volume;
            soundProperties.source.pitch = soundProperties.pitch;
            soundProperties.source.loop = soundProperties.loop;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Créer un référence temporaire à la scène actuelle
        currentScene = SceneManager.GetActiveScene();

        //Retrouve le nom de la scène
        sceneName = currentScene.name;


        PlayMusic();
        PlayAmbiance();

    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = MusicVolume;
        if(audioSource != null)
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float FinalVolume)
    {
        if(audioSource != null)
        {
            audioSource.volume = 0;
            audioSource.Play();
            FinalVolume = MusicVolume;

            while (audioSource.volume < FinalVolume)
            {
                audioSource.volume += FinalVolume * Time.deltaTime / FadeTime;

                yield return null;
            }


            audioSource.volume = FinalVolume;
        }
    }

    void PlayMusic()
    {
        if(sceneName == "Region_1")
        {
            if(currentMusic.name != "Region1")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Region1");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
        else if (sceneName == "Region_2")
        {
            if(currentMusic.name != "Region2")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Region2");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
        else if (sceneName == "Region_3")
        {
            if (currentMusic.name != "Region3")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Region3");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
        else if (sceneName == "Olympe_Floor_1" || sceneName == "Olympe_Floor_2" || sceneName == "Olympe_Floor_3")
        {
            if (currentMusic.name != "Olympe")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Olympe");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
        else if (sceneName == "MAH_Auberge")
        {
            if (currentMusic.name != "Auberge")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Auberge");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
        else if (sceneName == "MAH_Caverne")
        {
            if (currentMusic.name != "Caverne")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Caverne");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
        else if (sceneName == "MAH_Temple")
        {
            if (currentMusic.name != "Temple")
            {
                StartCoroutine(FadeOut(currentMusic.source, 1));
                currentMusic = Array.Find(music, s => s.name == "Temple");
                StartCoroutine(FadeIn(currentMusic.source, 1, 1));
            }
        }
    }

    void PlayAmbiance()
    {
        if (sceneName == "MAH_Auberge")
        {
            if (currentAmbiance.name != "TalkingPeople")
            {
                StartCoroutine(FadeOut(currentAmbiance.source, 1));
                currentAmbiance = Array.Find(ambiance, s => s.name == "TalkingPeople");
                StartCoroutine(FadeIn(currentAmbiance.source, 1, 1));
            }
        }
        else if (sceneName == "Region_1" || sceneName == "Region_2")
        {
            if (currentAmbiance.name != "Forest")
            {
                StartCoroutine(FadeOut(currentAmbiance.source, 1));
                currentAmbiance = Array.Find(ambiance, s => s.name == "Forest");
                StartCoroutine(FadeIn(currentAmbiance.source, 1, 1));
            }
        }
        else if (sceneName == "Region_3" || sceneName == "MAH_Caverne")
        {
            if (currentAmbiance.name != "Cave")
            {
                StartCoroutine(FadeOut(currentAmbiance.source, 1));
                currentAmbiance = Array.Find(ambiance, s => s.name == "Cave");
                StartCoroutine(FadeIn(currentAmbiance.source, 1, 1));
            }
        }
    }

    public void Play(string name)
    {
        if(currentMusic.name != name)
        {
            StartCoroutine(FadeOut(currentMusic.source, 1));
            currentMusic = Array.Find(music, s => s.name == name);
            StartCoroutine(FadeIn(currentMusic.source, 1, 1));
        }
        
    }

}
