using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CubeSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Cube Pushed Sound")]
    public AudioSource cubePush;
    [Range(0f, 1f)] public float cubePushVolume = 0.5f;

    private CubeToPush cubeToPushScript;

    #endregion

    void Start()
    {
        cubeToPushScript = GetComponentInParent<CubeToPush>();
    }

    void FixedUpdate()
    {
        PushCube();
    }

    void PushCube()
    {
        if (cubeToPushScript.playerPushing)
        {
            SoundManager.Instance.PlayCubePushed(cubePush, cubePushVolume);
        }
        else
        {
            SoundManager.Instance.StopCubePushed(cubePush);
        }
    }
}
