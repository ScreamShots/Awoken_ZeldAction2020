using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWalkInTemple : MonoBehaviour
{
    [SerializeField] DialogueTrigger thisTrigger;
    [SerializeField] GameObject highLightCam;
    [SerializeField] GameObject dialogueCam;

    bool highLightCAmActivated;
    bool dialogueCamDesactivated;

    private void Start()
    {
        thisTrigger.StartDialogue();
    }

    private void Update()
    {
        if(DialogueManager.Instance.dialoguePhaseIndex == 1 && thisTrigger.dialogueStarted && !highLightCAmActivated)
        {
            dialogueCam.SetActive(false);
            highLightCam.SetActive(true);
            highLightCAmActivated = true;
        }
        if (thisTrigger.dialogueEnded && !dialogueCamDesactivated)
        {
            ProgressionManager.Instance.firstReachTheTemple = true;
            highLightCam.SetActive(false);
            dialogueCamDesactivated = true;
        }

    }


}
