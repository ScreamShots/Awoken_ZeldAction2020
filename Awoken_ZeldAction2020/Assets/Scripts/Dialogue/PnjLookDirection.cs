using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjLookDirection : MonoBehaviour
{
    private enum WatchDirection {right, left, up, down};

    [SerializeField]
    WatchDirection baseDirection;

    [SerializeField]
    DialogueTrigger[] allLinkedDialogueTrigger;
    DialogueTrigger currentDialogueTrigger;
    Animator pnjAnimator;
    bool alreadyOriented;



    private void Start()
    {
        pnjAnimator = GetComponentInChildren<Animator>();

        switch (baseDirection)
        {
            case WatchDirection.right:
                pnjAnimator.SetTrigger("Idle_Right");
                break;
            case WatchDirection.left:
                pnjAnimator.SetTrigger("Idle_Left");
                break;
            case WatchDirection.down:
                pnjAnimator.SetTrigger("Idle_Down");
                break;
            default:
                pnjAnimator.SetTrigger("Idle_Down");
                break;

        }
    }
    private void Update()
    {
        if (!alreadyOriented)
        {
            CheckDialogue();
        }
        else
        {
            if(currentDialogueTrigger != null)
            {
                if (currentDialogueTrigger.dialogueEnded)
                {
                    alreadyOriented = false;
                }
            }
        }
        
        
    }

    void CheckDialogue()
    {
        foreach(DialogueTrigger dialogueTrigger in allLinkedDialogueTrigger)
        {
            if (dialogueTrigger.dialogueStarted)
            {
                currentDialogueTrigger = dialogueTrigger;
                OrientatePnj();
                return;
            }
        }
    }

    void OrientatePnj()
    {
        Vector2 playerRelativPos = PlayerManager.Instance.gameObject.transform.position - transform.position;

        if(Mathf.Abs(playerRelativPos.x) >= Mathf.Abs(playerRelativPos.y))
        {
            if(playerRelativPos.x > 0)
            {
                pnjAnimator.SetTrigger("Idle_Right");
            }
            else
            {
                pnjAnimator.SetTrigger("Idle_Left");
            }
        }
        else
        {
            pnjAnimator.SetTrigger("Idle_Down");
        }
    }
}
