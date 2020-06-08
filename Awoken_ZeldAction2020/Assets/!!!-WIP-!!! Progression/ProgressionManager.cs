using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    public int numberOfFragmentsInR1 = 0;

    public int numberOfFragmentsInR2 = 0;

    public int numberOfFragmentsInR3 = 0;

    public int numberOfFragmentsInF1 = 0;

    public int numberOfFragmentsInF2 = 0;

    public int numberOfFragmentsInF3 = 0;

    [HideInInspector]
    public int totalFragmentsIG = 0;

    public Dictionary<int, bool> R1Fragments = new Dictionary<int, bool>();
    public Dictionary<int, bool> R2Fragments = new Dictionary<int, bool>();
    public Dictionary<int, bool> R3Fragments = new Dictionary<int, bool>();

    public Dictionary<int, bool> F1Fragments = new Dictionary<int, bool>();
    public Dictionary<int, bool> F2Fragments = new Dictionary<int, bool>();
    public Dictionary<int, bool> F3Fragments = new Dictionary<int, bool>();

    public Dictionary<string, bool> PlayerCapacity = new Dictionary<string, bool>();

    [HideInInspector]
    public float maxHp = 80;
    [HideInInspector]
    public float playerHp = 80;
    [HideInInspector]
    public float maxStamina = 40;
    [HideInInspector]
    public float playerStamina = 40;
    [HideInInspector]
    public float playerFury = 100;

    [HideInInspector]
    public int currentSceneIndex = 1;
    [HideInInspector]
    public int currentAreaIndex = 0;

    public int availableFragments = 0;
    public int totalFragments = 0;

    public int lvlOfHealUpgrade = 0;
    public int lvlOfShieldUpgrade = 0;

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

        #region Fragment Dictionaries Initialization
               
        for(int i = 0; i < numberOfFragmentsInR1; i++)
        {
            R1Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInR2; i++)
        {
            R2Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInR3; i++)
        {
            R3Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInF1; i++)
        {
            F1Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInF2; i++)
        {
            F2Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInF3; i++)
        {
            F3Fragments.Add(i, false);
        }

        totalFragmentsIG = numberOfFragmentsInR1 + numberOfFragmentsInR2 + numberOfFragmentsInR3 + numberOfFragmentsInF1 + numberOfFragmentsInF2 + numberOfFragmentsInF3;
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
                maxHp = 100f;
                maxStamina = 50f;
                break;
            case ProgressionTimeLine.SecondRegionOut:
                SetPlayerCapacity(true, true, true, false);
                maxHp = 100f;
                maxStamina = 50f;
                break;
            case ProgressionTimeLine.OlympeFloorTwoStart:
                SetPlayerCapacity(true, true, true, false);
                maxHp = 100f;
                maxStamina = 50f;
                break;
            case ProgressionTimeLine.OlympeFloorTwoLREntrance:
                SetPlayerCapacity(true, true, true, false);
                maxHp = 100f;
                maxStamina = 50f;
                break;
            case ProgressionTimeLine.OlympeFloorTwoEnd:
                SetPlayerCapacity(true, true, true, false);
                maxHp = 100f;
                maxStamina = 50f;
                break;
            case ProgressionTimeLine.ThirdRegionEntrance:
                SetPlayerCapacity(true, true, true, false);
                maxHp = 100f;
                maxStamina = 50f;
                break;
            case ProgressionTimeLine.ShieldChargeUnlock:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
            case ProgressionTimeLine.ThirdRegionOut:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
            case ProgressionTimeLine.OlympeFloorThreeStart:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
            case ProgressionTimeLine.OlympeFloorThreeLREntrance:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
            case ProgressionTimeLine.OlympeFloorThreeEnded:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
            case ProgressionTimeLine.ZeusFightStarted:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;                
            case ProgressionTimeLine.EndAdventure:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
            default:
                SetPlayerCapacity(true, true, true, true);
                maxHp = 120f;
                maxStamina = 60f;
                break;
        }

        playerHp = maxHp;
        playerStamina = maxStamina;

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

        maxHp = data.maxHp;
        playerHp = data.playerHp;
        maxStamina = data.maxStamina;
        playerStamina = data.playerStamina;
        playerFury = data.playerFury;

        for (int i = 0; i < R1Fragments.Count; i++)
        {
            R1Fragments[i] = data.r1Fragments[i];
        }
        for (int i = 0; i < R2Fragments.Count; i++)
        {
            R2Fragments[i] = data.r2Fragments[i];
        }
        for (int i = 0; i < R3Fragments.Count; i++)
        {
            R3Fragments[i] = data.r3Fragments[i];
        }
        for (int i = 0; i < F1Fragments.Count; i++)
        {
            F1Fragments[i] = data.f1Fragments[i];
        }
        for (int i = 0; i < F2Fragments.Count; i++)
        {
            F2Fragments[i] = data.f2Fragments[i];
        }
        for (int i = 0; i < F3Fragments.Count; i++)
        {
            F3Fragments[i] = data.f3Fragments[i];
        }

        currentAreaIndex = data.currentAreaIndex;
        currentSceneIndex = data.currentSceneIndex;

        availableFragments = data.availableFragments;
        totalFragments = data.totalFragemnts;

        GameManager.Instance.sceneToLoad = currentSceneIndex;
        GameManager.Instance.areaToLoad = currentAreaIndex;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();
    }

    public void ResetProgressionManager()
    {
        #region Vegetables Dictionary Initialization

        R1Vegetables = new Dictionary<int, bool>();

        R1Vegetables.Add(1, false);
        R1Vegetables.Add(2, false);
        R1Vegetables.Add(3, false);
        #endregion

        #region Fragment Dictionaries Initialization

        R1Fragments = new Dictionary<int, bool>();
        R2Fragments = new Dictionary<int, bool>();
        R3Fragments = new Dictionary<int, bool>();

        F1Fragments = new Dictionary<int, bool>();
        F2Fragments = new Dictionary<int, bool>();
        F3Fragments = new Dictionary<int, bool>();

        for (int i = 0; i < numberOfFragmentsInR1; i++)
        {
            R1Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInR2; i++)
        {
            R2Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInR3; i++)
        {
            R3Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInF1; i++)
        {
            F1Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInF2; i++)
        {
            F2Fragments.Add(i, false);
        }
        for (int i = 0; i < numberOfFragmentsInF3; i++)
        {
            F3Fragments.Add(i, false);
        }
        #endregion

        #region Player

        PlayerCapacity = new Dictionary<string, bool>();

        PlayerCapacity.Add("Shield", false);
        PlayerCapacity.Add("Block", false);
        PlayerCapacity.Add("Pary", false);
        PlayerCapacity.Add("Charge", false);

        SetPlayerCapacity(false, false, false, false);

        maxHp = 80f;
        playerHp = maxHp;
        maxStamina = 40f;
        playerStamina = maxStamina;

        #endregion
    }

#if UNITY_EDITOR

    public void SetFrgValue(int nbrFrg, int sceneIndex)
    {
        Undo.RecordObject(this, "apply Fragment Number To Prefab");

        switch (sceneIndex)
        {
            case 4:
                numberOfFragmentsInR1 = nbrFrg;
                break;
            case 5:
                numberOfFragmentsInR2 = nbrFrg;
                break;
            case 6:
                numberOfFragmentsInR3 = nbrFrg;
                break;
            case 7:
                numberOfFragmentsInF1 = nbrFrg;
                break;
            case 8:
                numberOfFragmentsInF2 = nbrFrg;
                break;
            case 9:
                numberOfFragmentsInF3 = nbrFrg;
                break;
            default:
                break;
        }        
    }

    public void ApplyToPrefab(int sceneIndex)
    {
        SerializedObject thisObjSerialized = new SerializedObject(this);
        SerializedProperty numberOfFragmentValue = null;

        switch (sceneIndex)
        {
            case 4:
                numberOfFragmentValue = thisObjSerialized.FindProperty("numberOfFragmentsInR1");
                break;
            case 5:
                numberOfFragmentValue = thisObjSerialized.FindProperty("numberOfFragmentsInR2");
                break;
            case 6:
                numberOfFragmentValue = thisObjSerialized.FindProperty("numberOfFragmentsInR3");
                break;
            case 7:
                numberOfFragmentValue = thisObjSerialized.FindProperty("numberOfFragmentsInF1");
                break;
            case 8:
                numberOfFragmentValue = thisObjSerialized.FindProperty("numberOfFragmentsInF2");
                break;
            case 9:
                numberOfFragmentValue = thisObjSerialized.FindProperty("numberOfFragmentsInF3");
                break;
            default:
                break;
        }
         
        PrefabUtility.ApplyPropertyOverride(numberOfFragmentValue, "Assets/Prefabs/GameManager.prefab", InteractionMode.UserAction);
        
    }

#endif

}
