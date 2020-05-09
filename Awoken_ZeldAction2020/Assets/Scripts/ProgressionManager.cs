using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    public string playerScenePresence = "_Scenes/LD 100%/MAH_Auberge";

    public bool vegetablesFirstDialogueDone;
    public bool vegetablesDone;
    public bool zeusRevealCutsceneDone;
    public bool firstReachTheTemple;
    public bool firstBattleZeus;
    public bool undergroudCutSceneDone;
    public bool secondReachTheTemple;
    public bool transformFirstStatue;
    public bool openFirstFloorGate;
    public bool openSecondRegion;
    public bool unlockPary;
    public bool transformSecondStatue;
    public bool openSecondFloorGate;
    public bool openThirdRegion;
    public bool unlockCharge;
    public bool transformThirdStatue;
    public bool openThirdFloorGate;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
