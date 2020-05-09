using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPassageRegion3 : MonoBehaviour
{
    ProgressionManager progressionManagerScript;

    [SerializeField] DialogueTrigger thisTrigger;
    [SerializeField] GameObject altarCam;
    [SerializeField] GameObject dialogueCam;

    bool altarCamActivated;
    bool dialogueCamDesactivated;

    private void Start()
    {
        progressionManagerScript = GameManager.Instance.GetComponent<ProgressionManager>();

        if (!progressionManagerScript.openThirdRegion)
        {
            progressionManagerScript.openThirdRegion = true;
            thisTrigger.StartDialogue();
        }
    }

    private void Update()
    {
        if (DialogueManager.Instance.dialoguePhaseIndex == 1 && thisTrigger.dialogueStarted && !altarCamActivated)
        {
            dialogueCam.SetActive(false);
            altarCam.SetActive(true);
            altarCamActivated = true;
        }
        if (thisTrigger.dialogueEnded && !dialogueCamDesactivated)
        {
            ProgressionManager.Instance.firstReachTheTemple = true;
            altarCam.SetActive(false);
            dialogueCamDesactivated = true;
        }

    }
}
