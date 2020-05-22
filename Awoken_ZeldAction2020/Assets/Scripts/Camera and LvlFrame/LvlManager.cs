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
        InitializeLvl(0);
    }

    public void InitializeLvl(int startZoneIndex)
    {
        lastLoadedstartZoneIndex = startZoneIndex;
        foreach (AreaManager areaManager in allAreaManager)
        {
            areaManager.UnLoadArea();
        }
        StartCoroutine(LvlStarts[startZoneIndex].InitializeFirstCam());
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
}
