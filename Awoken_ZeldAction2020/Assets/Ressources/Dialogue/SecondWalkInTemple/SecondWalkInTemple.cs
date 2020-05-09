using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWalkInTemple : MonoBehaviour
{
    [SerializeField] DialogueTrigger thisDialogue;
    [SerializeField] GameObject dialogueCam;

    bool camDesactivated;

    private void Start()
    {
        thisDialogue.StartDialogue();
    }

    private void Update()
    {
        if (thisDialogue.dialogueEnded && !camDesactivated)
        {
            dialogueCam.SetActive(false);
            camDesactivated = true;
            ProgressionManager.Instance.secondReachTheTemple = true;
        }
    }
}
