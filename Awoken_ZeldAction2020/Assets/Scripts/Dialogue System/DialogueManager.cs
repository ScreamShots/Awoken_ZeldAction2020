using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueUINoFace;
    public GameObject dialogueUI;

    public Image faceDisplay;

    public TextMeshProUGUI textBoxNoFace;
    public TextMeshProUGUI textBox;
    TextMeshProUGUI targetedTextBox;

    public int dialoguePhaseIndex;
    public bool dialogueWithFace;
    public float typeSpeed;

    public Dialogue currentDialogue;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(Dialogue thisDialogue)
    {
        if(thisDialogue.faceImage == null)
        {
            dialogueUINoFace.SetActive(true);
            targetedTextBox = textBoxNoFace;
            dialogueWithFace = false;
        }
        else
        {
            dialogueUI.SetActive(true);
            targetedTextBox = textBox;
            faceDisplay.sprite = thisDialogue.faceImage;
            dialogueWithFace = true;
        }

        currentDialogue = thisDialogue;
        dialoguePhaseIndex = -1;
        

        NextDialoguePhase();
    }

    public void EndDialogue()
    {
        StopAllCoroutines();

        if (dialogueWithFace)
        {                       
            dialogueUI.SetActive(false);
        }
        else
        {
            dialogueUINoFace.SetActive(false);
        }
    }

    public void NextDialoguePhase()
    {
        dialoguePhaseIndex += 1;
        if(dialoguePhaseIndex >= currentDialogue.talkPhases.Length)
        {
            EndDialogue();
            return;
        }
        targetedTextBox.text = "<color=#00000000>" + currentDialogue.talkPhases[dialoguePhaseIndex] + "<color=#00000000>";
        StartCoroutine(TypeDialogePhase());
    }

    IEnumerator TypeDialogePhase()
    {
        int charIndex = 1;

        foreach (char letter in currentDialogue.talkPhases[dialoguePhaseIndex])
        {
            targetedTextBox.text = currentDialogue.talkPhases[dialoguePhaseIndex].Substring(0, charIndex) +"<color=#00000000>" + currentDialogue.talkPhases[dialoguePhaseIndex].Substring(0, charIndex) + "<color=#00000000>";
            yield return new WaitForSeconds(typeSpeed);
            charIndex += 1;
        }
    }
}
