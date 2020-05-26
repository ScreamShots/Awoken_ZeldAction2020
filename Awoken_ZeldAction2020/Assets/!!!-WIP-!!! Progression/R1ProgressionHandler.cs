using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class R1ProgressionHandler : BasicProgressionHandler
{

    [Header("Intro")]
    [Header("RequieredElement")]

    [SerializeField] bool showIntroElements;
    [Space]

    #region Serialize var

    [SerializeField] [ConditionalHide("showIntroElements", true)]
    GameObject blockingTreeA2A3 = null;
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    GameObject[] allVegetables = null;
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    GameObject enigmaOneCube = null;
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    Vector3 enigmaOneCubePosition = new Vector3(0,0,0);
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    InstantPressurePlate enigmaOnePressurePlate = null;
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    ActionLever enigmaOneLever = null;
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    GameObject PNJ_Vegetables = null;
    [SerializeField]
    [ConditionalHide("showIntroElements", true)]
    IsEnigma1Done enigmaOne = null; 
    [SerializeField] [ConditionalHide("showIntroElements", true)]
    GameObject CutScene_VegetablesStart = null;

    #endregion

    [Header("ZeusReveal")]

    #region Serialize Var

    [SerializeField] bool showZeusRevealElements;
    [Space]

    [SerializeField] [ConditionalHide("showZeusRevealElements", true)]
    GameObject[] allLightningsTraces = null;
    [SerializeField] [ConditionalHide("showZeusRevealElements", true)]
    GameObject[] blockingElementsA6A7 = null;

    #endregion

    [Header("Block Unlocked")]

    [SerializeField] bool showBlockUnlockElements;
    [Space]

    #region Serialize Var

    [SerializeField] [ConditionalHide("showBlockUnlockElements", true)]
    GameObject CutScene_CaveOut = null;

    #endregion

    [Header("Door")]

    [SerializeField] bool showDoorElements;
    [Space]

    #region Serialize Var

    [SerializeField] [ConditionalHide("showDoorElements", true)]
    DoorBehavior doorToRegionTwo = null;
    [SerializeField] [ConditionalHide("showDoorElements", true)]
    DoorBehavior doorToRegionThree = null;

    #endregion

    void EnigmaOneDone()
    {
        enigmaOneCube.transform.position = enigmaOneCubePosition;
        enigmaOnePressurePlate.isPressed = true;
        enigmaOneLever.isPressed = true;
        enigmaOne.isEnigmaDone = true;
    }

    protected override void OnNewAdventure()
    {
        CutScene_VegetablesStart.SetActive(true);
        PNJ_Vegetables.SetActive(true);
        blockingTreeA2A3.SetActive(true);

        foreach (GameObject vegetable in allVegetables)
        {
            vegetable.SetActive(true);
        }

        foreach (GameObject lightningTrace in allLightningsTraces)
        {
            lightningTrace.SetActive(false);
        }

        CutScene_VegetablesStart.GetComponent<BasicCutSceneManager>().StartCutScene();
    }

    protected override void OnVegetablesStart()
    {
        PNJ_Vegetables.SetActive(true);
        blockingTreeA2A3.SetActive(true);

        foreach (GameObject vegetable in allVegetables)
        {
            vegetable.SetActive(true);
        }

        foreach (GameObject lightningTrace in allLightningsTraces)
        {
            lightningTrace.SetActive(false);
        }
    }

    protected override void OnVegetablesEnd()
    {
        PNJ_Vegetables.SetActive(true);
        blockingTreeA2A3.SetActive(true);

        EnigmaOneDone();

        foreach (GameObject lightningTrace in allLightningsTraces)
        {
            lightningTrace.SetActive(false);
        }
    }

    protected override void OnZeusReveal()
    {
        foreach (GameObject blockingElement in blockingElementsA6A7)
        {
            blockingElement.SetActive(true);
        }

        EnigmaOneDone();
    }

    protected override void OnTempleFirstEntrance()
    {
        foreach (GameObject blockingElement in blockingElementsA6A7)
        {
            blockingElement.SetActive(true);
        }

        EnigmaOneDone();
    }

    protected override void OnShieldBlockUnlock()
    {
        CutScene_CaveOut.SetActive(true);
        EnigmaOneDone();
        CutScene_CaveOut.GetComponent<BasicCutSceneManager>().StartCutScene();
    }

    protected override void OnCaveOut()
    {
        EnigmaOneDone();
    }

    protected override void OnTempleSecondEntrance()
    {
        EnigmaOneDone();
    }

    protected override void OnOlympeFloorOneEnd()
    {
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnSecondRegionEntrance()
    {
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnSecondRegionBrazeros()
    {
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnSecondRegionOut()
    {
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnOlympeFloorTwoEnd()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnThirdRegionEntrance()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnThirdRegionOut()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnEndAdventure()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }
}
