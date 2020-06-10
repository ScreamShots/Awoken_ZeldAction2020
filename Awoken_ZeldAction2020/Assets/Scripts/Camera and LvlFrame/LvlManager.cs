using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LvlManager : MonoBehaviour
{
    public static LvlManager Instance;

    [HideInInspector]
    public CinemachineBrain lvlCamBrain;
    [HideInInspector]
    public float defaultblendTime = 0;

    public bool canEndTransition;

    public AreaManager[] LvlStarts;

    [SerializeField]
    AreaManager[] allAreaManager = null;

    [HideInInspector]
    public AreaManager currentArea = null;
    [HideInInspector]
    public int lastLoadedstartZoneIndex = 0;

    [HideInInspector]
    CinemachineVirtualCamera activeVCam = null;
    CinemachineBasicMultiChannelPerlin activePerlinProfil = null;

    private void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    private void Start()
    {
        lvlCamBrain = GetComponentInChildren<CinemachineBrain>();
        defaultblendTime = lvlCamBrain.m_DefaultBlend.m_Time;
        //InitializeLvl(0);
    }

    public void InitializeLvl(int startZoneIndex)
    {
        lastLoadedstartZoneIndex = startZoneIndex;
        foreach (AreaManager areaManager in allAreaManager)
        {
            areaManager.UnLoadArea();
        }
        StartCoroutine(LvlStarts[startZoneIndex].InitializeFirstCam());
        PlayerManager.Instance.transform.position = LvlStarts[startZoneIndex].pointOfRespawn.position;
    }
    

    private void Update()
    {
        if(canEndTransition && GameManager.Instance.gameState == GameManager.GameState.LvlFrameTransition)
        {
            if (!lvlCamBrain.IsBlending)
            {
                canEndTransition = false;
                GameManager.Instance.gameState = GameManager.GameState.Running;
                currentArea.LoadArea();
            }
        }
    }

    public void LaunchScreenShake(float intensity = 1, float duration = 1, float frequency = 1, bool autoStop = true)
    {
        activeVCam = lvlCamBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        activePerlinProfil = activeVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if(activePerlinProfil != null)
        {
            activePerlinProfil.m_AmplitudeGain += intensity;
            activePerlinProfil.m_FrequencyGain += frequency;
        }

        if (autoStop)
        {
            StartCoroutine(StopScreenShake(intensity, duration, frequency, activePerlinProfil));
        }

    }

    public void StopScreenShakeExt(float intensity = 1, float frequency = 1)
    {
        activeVCam = lvlCamBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        activePerlinProfil = activeVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (activePerlinProfil != null)
        {
            activePerlinProfil.m_AmplitudeGain -= intensity;
            activePerlinProfil.m_FrequencyGain -= frequency;

            if (activePerlinProfil.m_AmplitudeGain < 0)
            {
                activePerlinProfil.m_AmplitudeGain = 0;
            }
            if (activePerlinProfil.m_FrequencyGain < 0)
            {
                activePerlinProfil.m_FrequencyGain = 0;
            }
        }
    }

    public IEnumerator StopScreenShake(float intensity, float duration, float frequency, CinemachineBasicMultiChannelPerlin targetShakeComponent)
    {
        yield return new WaitForSeconds(duration);

        if(targetShakeComponent != null)
        {
            targetShakeComponent.m_AmplitudeGain -= intensity;
            targetShakeComponent.m_FrequencyGain -= frequency;

            if(targetShakeComponent.m_AmplitudeGain < 0)
            {
                targetShakeComponent.m_AmplitudeGain = 0;
            }
            if(targetShakeComponent.m_FrequencyGain < 0)
            {
                targetShakeComponent.m_FrequencyGain = 0;
            }
        }
    }
}
