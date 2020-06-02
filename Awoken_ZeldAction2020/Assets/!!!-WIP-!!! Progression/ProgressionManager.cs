using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance = null;

    public enum ProgressionTimeLine
    {
        NewAdventure = 1,               // When the player launch a new game
        VegeteablesStart = 2,           //After he leave the inn for the first time
        VegetablesEnd = 3,              //After he collected all the vegetables
        ZeusReveal = 4,
        //SAVE HERE
        TempleFirstEntrance = 5,        //When he walk in the temple for the first time     
        //SAVE HERE
        ShieldBlockUnlock = 6,          //After the CutScene in the Cave
        CaveOut = 7,                    //After the cutscene Outside the Cave
        //SAVE HERE
        TempleSecondEntrance = 8,       //When he walk in the temple for the second time 
        //SAVE HERE
        OlympeFloorOneStart = 9,        //After being tp into the first Olympus' Floor 1
        //SAVE HERE
        OlympeFloorOneLREntrance = 10,  //After Walk in the last Room of the first Floor
        OlympeFloorOneEnd = 11,         //When coming back to R1 after using Tp in Floor 1
        //SAVE HERE
        SecondRegionEntrance = 12,      //When enterring into region2 for the first time;
        //SAVE HERE
        SecondRegionBrazeros = 13,      //When the brazero enigma is done and the door is open
        //SAVE HERE
        ShieldParyUnlocked = 14,        //After activating autel and unlocking pary
        //SAVE HERE
        SecondRegionOut = 15,           //After finishing enigma with pipes
        //SAVE HERE
        OlympeFloorTwoStart = 16,       //After being tp into the first Olympus' Floor 2
        //SAVE HERE
        OlympeFloorTwoLREntrance = 17,  //After Walk in the last Room of the second Floor
        OlympeFloorTwoEnd = 18,         //When coming back to R1 after using Tp in Floor 2
        //SAVE HERE
        ThirdRegionEntrance = 19,       //When enterring into region3 for the first time;
        ShieldChargeUnlock = 20,        //After activating autel and unlocking charge
        //SAVE HERE
        ThirdRegionOut = 21,            //After Destroying all obstacle that block your way throuh R1
        //SAVE HERE
        OlympeFloorThreeStart = 22,     //After being tp into the first Olympus' Floor 3
        //SAVE HERE
        OlympeFloorThreeLREntrance = 23, //After Walk in the last Room of the first Floor
        OlympeFloorThreeEnded = 24,     // After Being Tp into the Boss Room 
        //SAVE HERE
        ZeusFightStarted = 25,          //After the first CutScene of Zeus Battle
        //SAVE HERE
        EndAdventure = 26,              //After Defeating Zeus
        //SAVE HERE
    }

    public ProgressionTimeLine thisSessionTimeLine = ProgressionTimeLine.NewAdventure;

    public Dictionary<int, bool> R1Vegetables = new Dictionary<int, bool>();

    public Dictionary<string, bool> PlayerCapacity = new Dictionary<string, bool>();

    public float playerHp = 100;
    public float playerStamina = 40;
    public float playerFury = 100;

    public int currentSceneIndex = 1;
    public int currentAreaIndex = 0;

    private void Awake()
    {
        SaveSystem.DebugPath();

        #region MakeSingleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion

        #region Vegetables Dictionary Initialization
        R1Vegetables.Add(1, false);
        R1Vegetables.Add(2, false);
        R1Vegetables.Add(3, false);
        #endregion

        #region Player

        PlayerCapacity.Add("Shield", false);
        PlayerCapacity.Add("Block", false);
        PlayerCapacity.Add("Pary", false);
        PlayerCapacity.Add("Charge", false);

        switch (thisSessionTimeLine)
        {
            case ProgressionTimeLine.NewAdventure:
                SetPlayerCapacity(false, false, false, false);
                break;
            case ProgressionTimeLine.VegeteablesStart:
                SetPlayerCapacity(false, false, false, false);
                break;
            case ProgressionTimeLine.VegetablesEnd:
                SetPlayerCapacity(false, false, false, false);
                break;
            case ProgressionTimeLine.ZeusReveal:
                SetPlayerCapacity(true, false, false, false);
                break;
            case ProgressionTimeLine.TempleFirstEntrance:
                SetPlayerCapacity(true, false, false, false);
                break;
            case ProgressionTimeLine.ShieldBlockUnlock:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.CaveOut:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.TempleSecondEntrance:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.OlympeFloorOneStart:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.OlympeFloorOneLREntrance:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.OlympeFloorOneEnd:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.SecondRegionEntrance:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.SecondRegionBrazeros:
                SetPlayerCapacity(true, true, false, false);
                break;
            case ProgressionTimeLine.ShieldParyUnlocked:
                SetPlayerCapacity(true, true, true, false);
                break;
            case ProgressionTimeLine.SecondRegionOut:
                SetPlayerCapacity(true, true, true, false);
                break;
            case ProgressionTimeLine.OlympeFloorTwoStart:
                SetPlayerCapacity(true, true, true, false);
                break;
            case ProgressionTimeLine.OlympeFloorTwoLREntrance:
                SetPlayerCapacity(true, true, true, false);
                break;
            case ProgressionTimeLine.OlympeFloorTwoEnd:
                SetPlayerCapacity(true, true, true, false);
                break;
            case ProgressionTimeLine.ThirdRegionEntrance:
                SetPlayerCapacity(true, true, true, false);
                break;
            case ProgressionTimeLine.ShieldChargeUnlock:
                SetPlayerCapacity(true, true, true, true);
                break;
            case ProgressionTimeLine.ThirdRegionOut:
                SetPlayerCapacity(true, true, true, true);
                break;
            case ProgressionTimeLine.OlympeFloorThreeStart:
                SetPlayerCapacity(true, true, true, true);
                break;
            case ProgressionTimeLine.OlympeFloorThreeLREntrance:
                SetPlayerCapacity(true, true, true, true);
                break;
            case ProgressionTimeLine.OlympeFloorThreeEnded:
                SetPlayerCapacity(true, true, true, true);
                break;
            case ProgressionTimeLine.ZeusFightStarted:
                SetPlayerCapacity(true, true, true, true);
                break;
            case ProgressionTimeLine.EndAdventure:
                SetPlayerCapacity(true, true, true, true);
                break;
            default:
                SetPlayerCapacity(true, true, true, true);
                break;
        }

        #endregion
    }

    void SetPlayerCapacity(bool shield, bool block, bool pary, bool charge)
    {
        PlayerCapacity["Shield"] = shield;
        PlayerCapacity["Block"] = block;
        PlayerCapacity["Pary"] = pary;
        PlayerCapacity["Charge"] = charge;
    }

    public void SaveTheProgression()
    {
        switch (thisSessionTimeLine)
        {
            case ProgressionTimeLine.NewAdventure:
                currentSceneIndex = 1;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.VegeteablesStart:
                currentSceneIndex = 4;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.VegetablesEnd:
                currentSceneIndex = 1;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.ZeusReveal:
                currentSceneIndex = 1;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.TempleFirstEntrance:
                currentSceneIndex = 3;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.ShieldBlockUnlock:
                currentSceneIndex = 2;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.CaveOut:
                currentSceneIndex = 4;
                currentAreaIndex = 3;
                break;
            case ProgressionTimeLine.TempleSecondEntrance:
                currentSceneIndex = 3;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.OlympeFloorOneStart:
                currentSceneIndex = 7;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.OlympeFloorOneLREntrance:
                currentSceneIndex = 7;
                currentAreaIndex = 1;
                break;
            case ProgressionTimeLine.OlympeFloorOneEnd:
                currentSceneIndex = 4;
                currentAreaIndex = 4;
                break;
            case ProgressionTimeLine.SecondRegionEntrance:
                currentSceneIndex = 5;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.SecondRegionBrazeros:
                currentSceneIndex = 5;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.ShieldParyUnlocked:
                currentSceneIndex = 5;
                currentAreaIndex = 1;
                break;
            case ProgressionTimeLine.SecondRegionOut:
                currentSceneIndex = 4;
                currentAreaIndex = 1;
                break;
            case ProgressionTimeLine.OlympeFloorTwoStart:
                currentSceneIndex = 8;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.OlympeFloorTwoLREntrance:
                currentSceneIndex = 8;
                currentAreaIndex = 1;
                break;
            case ProgressionTimeLine.OlympeFloorTwoEnd:
                currentSceneIndex = 4;
                currentAreaIndex = 4;
                break;
            case ProgressionTimeLine.ThirdRegionEntrance:
                currentSceneIndex = 6;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.ShieldChargeUnlock:
                currentSceneIndex = 6;
                currentAreaIndex = 1;
                break;
            case ProgressionTimeLine.ThirdRegionOut:
                currentSceneIndex = 4;
                currentAreaIndex = 2;
                break;
            case ProgressionTimeLine.OlympeFloorThreeStart:
                currentSceneIndex = 9;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.OlympeFloorThreeLREntrance:
                currentSceneIndex = 9;
                currentAreaIndex = 1;
                break;
            case ProgressionTimeLine.OlympeFloorThreeEnded:
                currentSceneIndex = 10;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.ZeusFightStarted:
                currentSceneIndex = 10;
                currentAreaIndex = 0;
                break;
            case ProgressionTimeLine.EndAdventure:
                currentSceneIndex = 1;
                currentAreaIndex = 0;
                break;
            default:
                currentSceneIndex = 1;
                currentAreaIndex = 0;
                break;
        }

        SaveSystem.SaveData(this);
    }

    public void LoadTheProgression()
    {
        ProgressionFile data = SaveSystem.LoadData();

        thisSessionTimeLine = (ProgressionTimeLine)data.progressionTimeLineValue;

        for (int i =0; i < data.r1VegetablesValue.Length; i++)
        {
            R1Vegetables[i + 1] = data.r1VegetablesValue[i];
        }

        for (int i = 0; i < data.playerCapacityKey.Length; i++)
        {
            PlayerCapacity[data.playerCapacityKey[i]] = data.playerCapacityValue[i];
        }

        playerHp = data.playerHp;
        playerStamina = data.playerStamina;
        playerFury = data.playerFury;

        currentAreaIndex = data.currentAreaIndex;
        currentSceneIndex = data.currentSceneIndex;

        GameManager.Instance.sceneToLoad = currentSceneIndex;
        GameManager.Instance.areaToLoad = currentAreaIndex;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();
    }

}
