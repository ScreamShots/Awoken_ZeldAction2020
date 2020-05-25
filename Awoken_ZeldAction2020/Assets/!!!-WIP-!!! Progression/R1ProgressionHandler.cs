using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R1ProgressionHandler : BasicProgressionHandler
{
    [Header("RequieredElement")]

    [Header("Intro")]

    [SerializeField] bool showIntroElements;

    #region Serialize var

    [SerializeField] [ConditionalHide("showIntroElement", false)]
    GameObject blockingTreeA2A3 = null;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    GameObject[] allVegetables = null;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    GameObject enigmaOneCube = null;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    Vector3 enigmaOneCubePosition;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    InstantPressurePlate enigmaOnePressurePlate = null;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    ActionLever enigmaOneLever = null;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    GameObject PNJ_Vegetables = null;
    [SerializeField] [ConditionalHide("showIntroElement", false)]
    GameObject CutScene_VegetablesStart = null;

    #endregion

    [Header("ZeusReveal")]

    #region Serialize Var

    [SerializeField] bool showZeusRevealElements;

    [SerializeField] [ConditionalHide("showZeusRevealElements", false)]
    GameObject[] allLightningsTraces = null;
    [SerializeField] [ConditionalHide("showZeusRevealElements", false)]
    GameObject[] blockingElementsA6A7 = null;

    #endregion

    [Header("Block Unlocked")]

    [SerializeField] bool showBlockUnlockElements;

    #region Serialize Var

    [SerializeField] [ConditionalHide("showBlockUnlockElements", false)]
    GameObject CutScene_CaveOut = null;

    #endregion

    [Header("Door")]

    [SerializeField] bool showDoorElements;

    #region Serialize Var

    [SerializeField] [ConditionalHide("showDoorElements", false)]
    DoorBehavior doorToRegionTwo = null;
    [SerializeField] [ConditionalHide("showDoorElements", false)]
    DoorBehavior doorToRegionThree = null;

    #endregion

    protected override void Start()
    {
        base.Start();

    }

    void EnigmaOneDone()
    {
        enigmaOneCube.transform.position = enigmaOneCubePosition;
        enigmaOnePressurePlate.isPressed = true;
        enigmaOneLever.isPressed = true;
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
    }

    protected override void OnOlympeFloorOneEnd()
    {
        doorToRegionTwo.isDoorOpen = true;
    }

    protected override void OnSecondRegionEntrance()
    {
        doorToRegionTwo.isDoorOpen = true;
    }

    protected override void OnSecondRegionBrazeros()
    {
        doorToRegionTwo.isDoorOpen = true;
    }

    protected override void OnSecondRegionOut()
    {
        doorToRegionTwo.isDoorOpen = true;
    }

    protected override void OnOlympeFloorTwoEnd()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
    }

    protected override void OnThirdRegionEntrance()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
    }

    protected override void OnThirdRegionOut()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
    }

    protected override void OnEndAdventure()
    {
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
    }
}
