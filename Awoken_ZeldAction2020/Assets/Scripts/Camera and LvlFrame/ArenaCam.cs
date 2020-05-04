using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Made by Antoine
/// This script is use to switch camera between Player camera and Boss fight camera
/// </summary>

public class ArenaCam : MonoBehaviour
{
    #region Variables
    TransitionArena transitionArenaScript;

    #endregion

    #region Inspector Settings
    public GameObject playerCam;
    public GameObject bossFightCamera;
    public GameObject groupComponer;

    #endregion

    void Start()
    {
        transitionArenaScript = GetComponentInChildren<TransitionArena>();
    }

    private void Update()
    {
        if (transitionArenaScript.playerInZone)
        {
            bossFightCamera.SetActive(true);
            groupComponer.SetActive(true);

            playerCam.SetActive(false);
        }
        else
        {
            bossFightCamera.SetActive(false);
            groupComponer.SetActive(false);

            playerCam.SetActive(true);
        }
    }
}
