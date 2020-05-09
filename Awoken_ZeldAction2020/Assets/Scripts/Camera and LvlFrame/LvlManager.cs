﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LvlManager : MonoBehaviour
{
    public static LvlManager Instance;

    CinemachineBrain lvlCamBrain;

    public bool canEndTransition;

    public AreaManager[] LvlStarts;

    public AreaManager currentArea = null;

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
        if (SceneHandler.Instance.alreadyLoadAScene)
        {
            currentArea = LvlStarts[SceneHandler.Instance.zoneToLoad];
                }
        StartCoroutine(LvlStarts[SceneHandler.Instance.zoneToLoad].InitializeFirstCam());
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
