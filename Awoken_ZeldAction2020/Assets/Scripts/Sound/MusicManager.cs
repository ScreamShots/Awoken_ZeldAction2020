using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/// <summary>
/// Made by Antoine
/// New version of Music Manager
/// </summary>

public class MusicManager : MonoBehaviour
{
// = = = [ VARIABLES DEFINITION ] = = =

    #region Inspector Settings
    [Header("Scene information")]
    public AudioClip currentMusic;
    public enum enumScene { Undefined, Menu, Inn, Cave, Temple, Region1, Region2, Region3, OlympeFloor, BossArena };
    public enumScene whichScene = enumScene.Undefined;

    private int sceneNumber;
    private Scene currentScene;

    [Space]
    [Header("Fade Settings")]
    public float musicFadeIn = 2;
    public float musicFadeOut = 2;

    [Space]
    [Header("Ambiances Sound")]
    public AudioClip aubergeAmbiance;
    [Range(0f, 1f)] public float aubergeAmbianceVolume = 0.5f;

    public AudioClip forestAmbiance;
    [Range(0f, 1f)] public float forestAmbianceVolume = 0.5f;

    public AudioClip caveAmbiance;
    [Range(0f, 1f)] public float caveAmbianceVolume = 0.5f;

    public AudioClip arenaAmbiance;
    [Range(0f, 1f)] public float arenaAmbianceVolume = 0.5f;

    [Space]
    [Header("Music Sound")]
    public AudioClip aubergeMusic;
    [Range(0f, 1f)] public float aubergeMusicVolume = 0.5f;

    public AudioClip region1Music;
    [Range(0f, 1f)] public float region1MusicVolume = 0.5f;

    public AudioClip region2Music;
    [Range(0f, 1f)] public float region2MusicVolume = 0.5f;

    public AudioClip region3Music;
    [Range(0f, 1f)] public float region3MusicVolume = 0.5f;

    public AudioClip caveMusic;
    [Range(0f, 1f)] public float caveMusicVolume = 0.5f;

    public AudioClip arenaMusic;
    [Range(0f, 1f)] public float arenaMusicVolume = 0.5f;

    public AudioClip olympeMusic;
    [Range(0f, 1f)] public float olympeMusicVolume = 0.5f;

    public AudioClip templeMusic;
    [Range(0f, 1f)] public float templeMusicVolume = 0.5f;

    public AudioClip menuMusic;
    [Range(0f, 1f)] public float menuMusicVolume = 0.5f;

    #endregion

 // = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =


// = = =

// = = = [RUNNING STATE] = = =

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneNumber = currentScene.buildIndex;
        WhichSceneWeAre();
        PlayMusic();


    }

    void WhichSceneWeAre()
    {
        switch (sceneNumber)
        {
            case 0:
                whichScene = enumScene.Menu;
                break;
            case 1:
                whichScene = enumScene.Inn;
                break;
            case 2:
                whichScene = enumScene.Cave;
                break;
            case 3:
                whichScene = enumScene.Temple;
                break;
            case 4:
                whichScene = enumScene.Region1;
                break;
            case 5:
                whichScene = enumScene.Region2;
                break;
            case 6:
                whichScene = enumScene.Region3;
                break;
            case 7:
                whichScene = enumScene.OlympeFloor;
                break;
            case 8:
                whichScene = enumScene.OlympeFloor;
                break;
            case 9:
                whichScene = enumScene.OlympeFloor;
                break;
            case 10:
                whichScene = enumScene.BossArena;
                break;
        }
    }

    void PlayMusic()
    {
        switch (whichScene)
        {
                //MENU music
            case enumScene.Menu:
                if (currentMusic != menuMusic)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = menuMusic;
                        SoundManager.Instance.FadeOutFadeInMusic(menuMusic, menuMusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.StopAmbiance();
                    }
                    else
                    {
                        currentMusic = menuMusic;
                        SoundManager.Instance.FadeInMusic(menuMusic, menuMusicVolume, musicFadeIn);
                        SoundManager.Instance.StopAmbiance();
                    }
                }
                break;
                //INN music
            case enumScene.Inn:
                if (currentMusic != aubergeMusic)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = aubergeMusic;
                        SoundManager.Instance.FadeOutFadeInMusic(aubergeMusic, aubergeMusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.PlayAmbiance(aubergeAmbiance, aubergeAmbianceVolume);
                    }
                    else
                    {
                        currentMusic = aubergeMusic;
                        SoundManager.Instance.FadeInMusic(aubergeMusic, aubergeMusicVolume, musicFadeIn);
                        SoundManager.Instance.PlayAmbiance(aubergeAmbiance, aubergeAmbianceVolume);
                    }
                }
                break;
                //CAVE music
            case enumScene.Cave:
                if (currentMusic != caveMusic)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = caveMusic;
                        SoundManager.Instance.FadeOutFadeInMusic(caveMusic, caveMusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.PlayAmbiance(caveAmbiance, caveAmbianceVolume);
                    }
                    else
                    {
                        currentMusic = caveMusic;
                        SoundManager.Instance.FadeInMusic(caveMusic, caveMusicVolume, musicFadeIn);
                        SoundManager.Instance.PlayAmbiance(caveAmbiance, caveAmbianceVolume);
                    }
                }
                break;
                //TEMPLE music
            case enumScene.Temple:
                if (currentMusic != templeMusic)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = templeMusic;
                        SoundManager.Instance.FadeOutFadeInMusic(templeMusic, templeMusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.StopAmbiance();
                    }
                    else
                    {
                        currentMusic = templeMusic;
                        SoundManager.Instance.FadeInMusic(templeMusic, templeMusicVolume, musicFadeIn);
                        SoundManager.Instance.StopAmbiance();
                    }
                }
                break;
                //REGION 1 music
            case enumScene.Region1:
                if (currentMusic != region1Music)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = region1Music;
                        SoundManager.Instance.FadeOutFadeInMusic(region1Music, region1MusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.PlayAmbiance(forestAmbiance, forestAmbianceVolume);
                    }
                    else
                    {
                        currentMusic = region1Music;
                        SoundManager.Instance.FadeInMusic(region1Music, region1MusicVolume, musicFadeIn);
                        SoundManager.Instance.PlayAmbiance(forestAmbiance, forestAmbianceVolume);
                    }
                }
                break;
                //REGION 2 music
            case enumScene.Region2:
                if (currentMusic != region2Music)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = region2Music;
                        SoundManager.Instance.FadeOutFadeInMusic(region2Music, region2MusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.PlayAmbiance(forestAmbiance, forestAmbianceVolume);
                    }
                    else
                    {
                        currentMusic = region2Music;
                        SoundManager.Instance.FadeInMusic(region2Music, region2MusicVolume, musicFadeIn);
                        SoundManager.Instance.PlayAmbiance(forestAmbiance, forestAmbianceVolume);
                    }
                }
                break;
                //REGION 3 music
            case enumScene.Region3:
                if (currentMusic != region3Music)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = region3Music;
                        SoundManager.Instance.FadeOutFadeInMusic(region3Music, region3MusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.StopAmbiance();
                    }
                    else
                    {
                        currentMusic = region3Music;
                        SoundManager.Instance.FadeInMusic(region3Music, region3MusicVolume, musicFadeIn);
                        SoundManager.Instance.StopAmbiance();
                    }
                }
                break;
                //OLYMPE FLOOR music
            case enumScene.OlympeFloor:
                if (currentMusic != olympeMusic)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = olympeMusic;
                        SoundManager.Instance.FadeOutFadeInMusic(olympeMusic, olympeMusicVolume, musicFadeIn, musicFadeOut);
                        SoundManager.Instance.StopAmbiance();
                    }
                    else
                    {
                        currentMusic = olympeMusic;
                        SoundManager.Instance.FadeInMusic(olympeMusic, olympeMusicVolume, musicFadeIn);
                        SoundManager.Instance.StopAmbiance();
                    }
                }
                break;
                //BOSS ARENA music
            case enumScene.BossArena:
                if (currentMusic != arenaMusic)
                {
                    if (currentMusic != null)
                    {
                        currentMusic = arenaMusic;
                        SoundManager.Instance.PlayAmbiance(arenaAmbiance, arenaAmbianceVolume);
                        SoundManager.Instance.FadeOutMusic();
                    }
                    else
                    {
                        currentMusic = arenaMusic;
                        SoundManager.Instance.PlayAmbiance(arenaAmbiance, arenaAmbianceVolume);
                    }
                }
                if (BossManager.Instance.canStartBossFight)
                {
                    SoundManager.Instance.FadeInMusic(arenaMusic, arenaMusicVolume, musicFadeIn);
                }
                break;
        }
    }

// = = =

}
