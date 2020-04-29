using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LvlManager : MonoBehaviour
{
    public static LvlManager Instance;

    CinemachineBrain lvlCamBrain;

    public bool canEndTransition;

    public AreaManager[] LvlStarts;

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
        LvlStarts[0].InitializeFirstCam();
    }
    

    private void Update()
    {
        if(canEndTransition && GameManager.Instance.gameState == GameManager.GameState.LvlFrameTransition)
        {
            if (!lvlCamBrain.IsBlending)
            {
                canEndTransition = false;
                GameManager.Instance.gameState = GameManager.GameState.Running;
            }
        }
    }
}
