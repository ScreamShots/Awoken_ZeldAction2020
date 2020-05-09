using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWalkInBoss : MonoBehaviour
{
    [SerializeField] DialogueTrigger thisTrigger;

    public Transform BossPlaceFirstWalk;
    public Transform BossPlaceArena;

    private void Start()
    {
        thisTrigger.StartDialogue();
    }

    private void Update()
    {
        if (DialogueManager.Instance.dialoguePhaseIndex == 1 && thisTrigger.dialogueStarted)
        {
            //BossManager.Instance.gameObject.transform.localScale =
        }
        if (thisTrigger.dialogueEnded)
        {
            
        }

    }
}
