using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance = null;

    public enum ProgressionTimeLine
    {
        NewAdventure = 1,               // When the player launch a new game
        //SAVE HERE
        VegeteablesStart = 2,           //After he leave the inn for the first time
        VegetablesEnd = 3,              //After he collected all the vegetables
        ZeusReveal = 4,
        //SAVE HERE
        TempleFirstEntrance = 5,        //When he walk in the temple for the first time     
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

    private void Awake()
    {
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

}
