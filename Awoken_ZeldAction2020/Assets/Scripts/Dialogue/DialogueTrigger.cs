using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueToPlay = null;
    [SerializeField]
    bool triggerByZone = false;
    public bool restartGameplayAtTheEnd = true;

    [HideInInspector]
    public bool dialogueStarted;
    [HideInInspector]
    public bool dialogueEnded;
    bool canStartDialogue = false;
    bool forceHide = false;

    InterractionButton buttonDisplay;

    private void Start()
    {
        buttonDisplay = GetComponentInChildren<InterractionButton>();
        if (!triggerByZone && buttonDisplay != null)
        {
            buttonDisplay.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (triggerByZone)
        {
            if(canStartDialogue && Input.GetButtonDown("Interraction") && !dialogueStarted)
            {
                buttonDisplay.HideButton();
                forceHide = true;
                PlayerMovement.playerRgb.velocity = Vector2.zero;
                StartDialogue();
            }

            if (!dialogueStarted && canStartDialogue && forceHide)
            {
                forceHide = false;
                buttonDisplay.ShowButton();
            }
        }    
    }

    [ContextMenu("StartDialogue")]
    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueToPlay, this, restartGameplayAtTheEnd);
        dialogueEnded = false;
        dialogueStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (triggerByZone)
            {
                buttonDisplay.ShowButton();
                canStartDialogue = true;
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (triggerByZone)
            {
                buttonDisplay.HideButton();
                canStartDialogue = false;
            }
        }
    }

 





}













