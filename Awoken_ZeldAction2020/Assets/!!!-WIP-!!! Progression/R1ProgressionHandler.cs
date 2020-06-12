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
        CutScene_VegetablesStart.GetComponent<BasicCutSceneManager>().onCutSceneEnd.AddListener(BannerShow);
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
        base.OnVegetablesStart();

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

        bool enigmaDone = true;
        foreach (KeyValuePair<int, bool>  vegatables in ProgressionManager.Instance.R1Vegetables)
        {
            if(vegatables.Value == false)
            {
                enigmaDone = false;
                break;
            }
        }

        if (enigmaDone)
        {
            EnigmaOneDone();
        }
    }

    protected override void OnVegetablesEnd()
    {
        base.OnVegetablesEnd();

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
        base.OnTempleFirstEntrance();

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
        CutScene_CaveOut.GetComponent<BasicCutSceneManager>().onCutSceneEnd.AddListener(BannerShow);
    }

    protected override void OnCaveOut()
    {
        base.OnCaveOut();
        EnigmaOneDone();
    }

    protected override void OnTempleSecondEntrance()
    {
        base.OnTempleSecondEntrance();
        EnigmaOneDone();
    }

    protected override void OnOlympeFloorOneEnd()
    {
        base.OnOlympeFloorOneEnd();
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnSecondRegionEntrance()
    {
        base.OnSecondRegionEntrance();
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnSecondRegionBrazeros()
    {
        base.OnSecondRegionBrazeros();
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnSecondRegionOut()
    {
        base.OnSecondRegionOut();
        doorToRegionTwo.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnOlympeFloorTwoEnd()
    {
        base.OnOlympeFloorTwoEnd();
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnThirdRegionEntrance()
    {
        base.OnThirdRegionEntrance();
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnThirdRegionOut()
    {
        base.OnThirdRegionOut();
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }

    protected override void OnEndAdventure()
    {
        base.OnEndAdventure();
        doorToRegionTwo.isDoorOpen = true;
        doorToRegionThree.isDoorOpen = true;
        EnigmaOneDone();
    }
}
