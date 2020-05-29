using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TuyauEnterSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Tuyau Sound")]
    public AudioClip tuyauEnter;
    [Range(0f, 1f)] public float tuyauEnterVolume = 0.5f;

    public AudioClip tuyauExit;
    [Range(0f, 1f)] public float tuyauExitVolume = 0.5f;

    private TuyauEnter tuyauEnterScript;

    private bool bulletIsEnter = false;
    private bool bulletIsExit = false;
    #endregion

    void Start()
    {
        tuyauEnterScript = GetComponentInParent<TuyauEnter>();
    }

    void Update()
    {
        BulletEnter();
    }

    void BulletEnter()
    {
        if (tuyauEnterScript.bulletInsideZone)
        {
            if (tuyauEnterScript.bulletIsEnter)
            {
                if (!bulletIsEnter)
                {
                    bulletIsEnter = true;
                    SoundManager.Instance.PlaySfx(tuyauEnter, tuyauEnterVolume);
                }

                bulletIsExit = false;
            }
            else
            {
                if (!bulletIsExit)
                {
                    bulletIsExit = true;
                    if (tuyauEnterScript.bulletTimeTravel  * tuyauEnterScript.nbrOfPipes >= 0.2)
                    {
                        SoundManager.Instance.PlaySfx(tuyauExit, tuyauExitVolume);
                    }
                }

                bulletIsEnter = false;
            }
        }
    }
}
