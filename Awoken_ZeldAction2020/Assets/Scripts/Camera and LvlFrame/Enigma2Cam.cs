using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Made by Antoine
/// This script gather all transition and switch cameras for enigma 2
/// </summary>

public class Enigma2Cam : MonoBehaviour
{
    #region Variables

    ProjectileDetectionZone projectileDetectionZoneScript;
    Enigma2 enigma2Script;

    private bool isBlending = false;
    private bool leverIsActivate = false;
    private bool pressurePlate = false;
    
    private bool transitionDoneLever = false;
    private bool transitionDonePlate = false;
    private bool lightEnable1 = false;
    private bool lightEnable2 = false;

    #endregion

    #region Inspector Settings

    [Header("Enigma")]
    public GameObject enigma2 = null;

    [Header("Cams")]
    public CinemachineBrain mainCamera;
    
    [Space ]public GameObject camArena3 = null;
    public GameObject globalArenaCam = null;

    [Space] public CinemachineVirtualCamera camArena2 = null;
    public CinemachineVirtualCamera ArenaDoorCam = null;

    [Space] public CinemachineVirtualCamera camArena4 = null;

    [Header("Time")]
    public float timeStayInGlobalCam = 0;
    public float timeStayInDoorCam = 0;

    #endregion

    void Start()
    {
        projectileDetectionZoneScript = GetComponentInChildren<ProjectileDetectionZone>();
        enigma2Script = enigma2.GetComponent<Enigma2>();

        globalArenaCam.SetActive(false);
        ArenaDoorCam.gameObject.SetActive(false);
    }

    void Update()
    {
        if (projectileDetectionZoneScript.bulletDetection && !isBlending)                           //If bullet pass the first room, start the camera transition
        {
            isBlending = true;
            StartCoroutine(DeZoomArena());
        }

        if (enigma2Script.isBrazeroOn1 && !pressurePlate)                                          //if pressure plate is activate, start the transitation cam to the double door and disable player control
        {
            PlayerMovement.playerRgb.velocity = Vector2.zero;
            pressurePlate = true;
            GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;

            StartCoroutine(ZoomOnDoorAfterPressurePlate());
        }
        else if (enigma2Script.isBrazeroOn2 && !leverIsActivate)                                    //if lever is activate, start the transitation cam to the double door and disable player control
        {
            PlayerMovement.playerRgb.velocity = Vector2.zero;
            leverIsActivate = true;
            GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;

            StartCoroutine(ZoomOnDoorAfterLever());
        }

        if (!CinemachineCore.Instance.IsLive(camArena2) && leverIsActivate && !lightEnable2)  //If Door cam is active and not blending : enable light on first brazer
        {
            lightEnable2 = true;
            enigma2Script.EnableLightAfterBlending();
            enigma2Script.OpenDoorDoublePlate();
        }
        if (!CinemachineCore.Instance.IsLive(camArena4) && pressurePlate && !lightEnable1)
        {
            lightEnable1 = true;
            enigma2Script.EnableLightAfterBlending();
            enigma2Script.OpenDoorDoublePlate();
        }

        if (!CinemachineCore.Instance.IsLive(ArenaDoorCam) && camArena2.gameObject.activeSelf && leverIsActivate && !transitionDoneLever)          //If lever cam is active and not blending : enable light on second brazer
        {
            transitionDoneLever = true;
            GameManager.Instance.gameState = GameManager.GameState.Running;
        }
        if (!CinemachineCore.Instance.IsLive(ArenaDoorCam) && camArena4.gameObject.activeSelf && pressurePlate && !transitionDonePlate)          //If pressure plate cam is active and not blending : enable light on second brazer
        {
            transitionDonePlate = true;
            GameManager.Instance.gameState = GameManager.GameState.Running;
        }
    }

    IEnumerator DeZoomArena()
    {
        camArena3.SetActive(false);
        globalArenaCam.SetActive(true);

        yield return new WaitForSeconds(timeStayInGlobalCam);

        camArena3.SetActive(true);
        globalArenaCam.SetActive(false);
        isBlending = false;
    }

    IEnumerator ZoomOnDoorAfterLever()
    {
        camArena2.gameObject.SetActive(false);
        ArenaDoorCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeStayInDoorCam);

        camArena2.gameObject.SetActive(true);
        ArenaDoorCam.gameObject.SetActive(false);
    }

    IEnumerator ZoomOnDoorAfterPressurePlate()
    {
        camArena4.gameObject.SetActive(false);
        ArenaDoorCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeStayInDoorCam);

        camArena4.gameObject.SetActive(true);
        ArenaDoorCam.gameObject.SetActive(false);
    }
}
