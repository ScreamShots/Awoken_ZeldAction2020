using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmaParryCam : MonoBehaviour
{
    #region Variables
    public Enigma2 enigma2Script;
    ProjectileDetectionZone projectileDetectionZoneScript;
    TurretShoot turretShootScript;

    private bool isBlending = false;

    #endregion

    #region Inspector Settings
    public GameObject parryTurret = null;

    [Header("Cams")]
    [Space] public GameObject camArena3 = null;
    public GameObject globalArenaCam = null;

    [Header("Time")]
    public float timeStayInGlobalCam = 0;

    #endregion

    void Start()
    {
        projectileDetectionZoneScript = GetComponentInChildren<ProjectileDetectionZone>();
        turretShootScript = parryTurret.GetComponent<TurretShoot>();
        globalArenaCam.SetActive(false);
    }

    void Update()
    {
        if (projectileDetectionZoneScript.bulletDetection && !isBlending)                           //If bullet pass the first room, start the camera transition
        {
            isBlending = true;
            turretShootScript.isActivated = false;
            GameManager.Instance.gameState = GameManager.GameState.Dialogue;
            PlayerMovement.playerRgb.velocity = Vector2.zero;
            StartCoroutine(DeZoomArena());
        }

        if (enigma2Script.distanceLever1.isPressed)
        {
            turretShootScript.isActivated = false;
        }
    }

    IEnumerator DeZoomArena()
    {
        camArena3.SetActive(false);
        globalArenaCam.SetActive(true);

        yield return new WaitForSeconds(timeStayInGlobalCam);

        camArena3.SetActive(true);
        globalArenaCam.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        if (!enigma2Script.distanceLever1.isPressed)
        {
            turretShootScript.isActivated = true;
            GameManager.Instance.gameState = GameManager.GameState.Running;
        }
        isBlending = false;
    }
}
