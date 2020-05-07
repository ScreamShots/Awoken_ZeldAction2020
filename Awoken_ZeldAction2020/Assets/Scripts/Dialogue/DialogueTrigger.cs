﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    Dialogue dialogueToPlay;
    [SerializeField]
    bool triggerByZone;

    [HideInInspector]
    public bool dialogueStarted;
    [HideInInspector]
    public bool dialogueEnded;
    bool canStartDialogue;

    InterractionButton buttonDisplay;

    private void Start()
    {
        buttonDisplay = GetComponentInChildren<InterractionButton>();
        if (!triggerByZone)
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
                StartDialogue();
            }
        }
    }

    [ContextMenu("test")]
    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueToPlay, this);
        dialogueEnded = false;
        dialogueStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (triggerByZone)
            {
                //buttonDisplay.SetActive(true);
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
                //buttonDisplay.SetActive(false);
                buttonDisplay.HideButton();
                canStartDialogue = false;
            }
        }
    }

 





}













