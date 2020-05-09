using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPassageBoss : MonoBehaviour
{
    [SerializeField] DialogueTrigger thisTrigger;
    [SerializeField] DialogueTrigger thisTriggerState2;
    [SerializeField] DialogueTrigger thisTriggerState3;

    private bool dialogueEnd = false;
    private bool dialogueEnd2 = false;
    private bool dialogueEnd3 = false;

    private bool dialogueCanStart = false;
    private bool dialogueCanStart2 = false;
    private bool dialogueCanStart3 = false;

    BossState3 bossState3Script;

    private void Start()
    {
        bossState3Script = BossManager.Instance.GetComponent<BossState3>();
    }

    private void Update()
    {
        if (BossManager.Instance.canStartDialog && !dialogueCanStart)   
        {
            dialogueCanStart = true;
            thisTrigger.StartDialogue();
            PlayerMovement.playerRgb.velocity = Vector2.zero;
        }

        if (thisTrigger.dialogueEnded && !dialogueEnd)
        {
            dialogueEnd = true;
            BossManager.Instance.canStartBossFight = true;
        }

        if (BossManager.Instance.canStartDialogState2 && !dialogueCanStart2)
        {
            dialogueCanStart2 = true;
            thisTriggerState2.StartDialogue();
            PlayerMovement.playerRgb.velocity = Vector2.zero;
        }

        if (thisTriggerState2.dialogueEnded && !dialogueEnd2)
        {
            dialogueEnd2 = true;
            BossManager.Instance.canPlayNextState = true;
        }

        if (BossManager.Instance.canStartDialogState3 && !dialogueCanStart3)
        {
            dialogueCanStart3 = true;
            thisTriggerState3.StartDialogue();
            PlayerMovement.playerRgb.velocity = Vector2.zero;
        }

        if (thisTriggerState3.dialogueEnded && !dialogueEnd3)
        {
            dialogueEnd3 = true;
            BossManager.Instance.canPlayNextState2 = true;
        }
    }
}
