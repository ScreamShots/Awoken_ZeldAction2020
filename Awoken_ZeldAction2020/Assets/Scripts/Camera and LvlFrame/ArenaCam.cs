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

    private bool switchCamPlayer;
    private bool switchCamBoss;
    private bool switchCamArena;
    #endregion

    #region Inspector Settings
    [Header("Player")]
    public GameObject playerCam;

    [Header("Arena")]
    public GameObject bossFightCamera;
    public GameObject groupComponer;

    [Space] public GameObject globalArenaCam;

    #endregion

    void Start()
    {
        transitionArenaScript = GetComponentInChildren<TransitionArena>();
    }

    private void Update()
    {
        if (transitionArenaScript.playerInZone)                                                                         //if Player is in Area
        {
            if (!switchCamBoss && (!BossManager.Instance.s1_Pattern1 && !BossManager.Instance.s2_Pattern3 && !BossManager.Instance.s3_Pattern1))             //camera Boss fight is active
            {
                switchCamPlayer = false;
                switchCamBoss = true;
                switchCamArena = false;

                bossFightCamera.SetActive(true);
                groupComponer.SetActive(true);

                playerCam.SetActive(false);
            }
            else if (!switchCamArena && (BossManager.Instance.s1_Pattern1 || BossManager.Instance.s2_Pattern3))         //if Thunder pattern is active = Arena camera active 
            {
                switchCamArena = true;
                switchCamBoss = false;

                bossFightCamera.SetActive(false);
                groupComponer.SetActive(false);

                globalArenaCam.SetActive(true);
            }
            /*else if (!switchCamPlayer && BossManager.Instance.s3_Pattern1)                                               //if Boss almost dead : active player cam
            {
                switchCamPlayer = true;
                switchCamBoss = false;
                switchCamArena = false;

                bossFightCamera.SetActive(false);
                groupComponer.SetActive(false);
                globalArenaCam.SetActive(false);

                playerCam.SetActive(true);
            }*/
        }
        else
        {
            if (!switchCamPlayer)                                                                                       //if Player outside arena : player cam is active
            {
                switchCamPlayer = true;
                switchCamBoss = false;

                bossFightCamera.SetActive(false);
                groupComponer.SetActive(false);

                playerCam.SetActive(true);
            }
        }
    }
}
