using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ZeusWallSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Wall Sound")]
    public AudioClip wallLauch;
    [Range(0f, 1f)] public float wallLauchVolume = 0.5f;

    public AudioSource wallAlive;
    [Range(0f, 1f)] public float wallAliveVolume = 0.5f;

    private bool wallIsAlive = false;
    private bool wallIsLauched = false;

    #endregion

    void Start()
    {
        LauchWall();
        WallDuration();

        wallAlive.volume = wallAliveVolume;
    }

    void LauchWall()
    {
        if (!wallIsLauched)
        {
            wallIsLauched = true;
            SoundManager.Instance.PlaySfx(wallLauch, wallLauchVolume);
        }
    }

    void WallDuration()
    {
        if (!wallIsAlive)
        {
            wallIsAlive = true;
            SoundManager.Instance.PlayCubePushed(wallAlive, wallAliveVolume);
        }
    }

    void OnDestroy()
    {
        SoundManager.Instance.StopCubePushed(wallAlive);
    }
}
