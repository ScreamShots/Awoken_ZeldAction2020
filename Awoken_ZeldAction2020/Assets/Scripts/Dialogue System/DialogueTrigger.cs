using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue thisDialogue;

    public bool triggerByZone;

    [ConditionalHide("triggerByZone", true)]
    public Collider2D triggerZone;

    bool dialogueStarted;
    bool dialogueEnded;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !dialogueStarted)
        {
            DialogueManager.Instance.StartDialogue(thisDialogue);
            dialogueStarted = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && dialogueStarted)
        {
            DialogueManager.Instance.NextDialoguePhase();
        }
    }

}













