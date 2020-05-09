using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPassageRegion2 : MonoBehaviour
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

        if (!progressionManagerScript.openSecondRegion)
        {
            progressionManagerScript.openSecondRegion = true;
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
