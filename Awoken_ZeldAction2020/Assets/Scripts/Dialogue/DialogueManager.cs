using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Dialogue currentDialogue;

    [Space]
    [Header("Requiered Elements")]
    
    [SerializeField]
    GameObject dialogueUINoFace;
    [SerializeField]
    GameObject dialogueUI;
    [SerializeField]
    Image faceDisplay;
    [SerializeField]
    TextMeshProUGUI textBoxNoFace;
    [SerializeField]
    TextMeshProUGUI textBox;

    [Space]
    [Header("Caracteristics")]
    public float typeSpeed;


    TextMeshProUGUI targetedTextBox;
    DialogueTrigger currentTrigger;

    int dialoguePhaseIndex;
    bool dialogueWithFace;
    [HideInInspector]
    public bool typingAPhase;
    [HideInInspector]
    public bool processingDialogue;

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

    private void Update()
    {
        if (processingDialogue)
        {
            if (Input.GetButtonDown("MenuValidate"))
            {
                NextDialoguePhase();
            }
        }
    }

    public void StartDialogue(Dialogue thisDialogue, DialogueTrigger thisTrigger)
    {
        if (processingDialogue)
        {
            Debug.Log("<b>WARNING!</b>");
            Debug.Log(" A dialogue is Already started and has not finished yet");
            Debug.Log("The new dialogue: <" + thisDialogue.name + ">, tried to start, his launch is canceled");
            return;
        }

        GameManager.Instance.gameState = GameManager.GameState.Dialogue;
        PlayerMovement.playerRgb.velocity = Vector2.zero;

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

        currentTrigger = thisTrigger;
        processingDialogue = true;
        currentDialogue = thisDialogue;
        dialoguePhaseIndex = -1;
        

        NextDialoguePhase();
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        currentTrigger.dialogueStarted = false;
        currentTrigger.dialogueEnded = true;
        currentTrigger = null;
        currentDialogue = null;

        if (dialogueWithFace)
        {                       
            dialogueUI.SetActive(false);
        }
        else
        {
            dialogueUINoFace.SetActive(false);
        }
        processingDialogue = false;
        GameManager.Instance.gameState = GameManager.GameState.Running;
    }

    public void NextDialoguePhase()
    {
        StopAllCoroutines();

        if (typingAPhase)
        {
            targetedTextBox.text = currentDialogue.talkPhases[dialoguePhaseIndex];
            typingAPhase = false;
        }
        else
        {
            dialoguePhaseIndex += 1;
            if (dialoguePhaseIndex >= currentDialogue.talkPhases.Length)
            {
                EndDialogue();
                return;
            }
            targetedTextBox.text = "<color=#00000000>" + currentDialogue.talkPhases[dialoguePhaseIndex] + "<color=#00000000>";
            StartCoroutine(TypeDialogePhase());
        }
    }

    IEnumerator TypeDialogePhase()
    {
        int charIndex = 1;
        typingAPhase = true;

        foreach (char letter in currentDialogue.talkPhases[dialoguePhaseIndex])
        {
            targetedTextBox.text = currentDialogue.talkPhases[dialoguePhaseIndex].Substring(0, charIndex) +"<color=#00000000>" + currentDialogue.talkPhases[dialoguePhaseIndex].Substring(0, charIndex) + "<color=#00000000>";
            yield return new WaitForSeconds(typeSpeed);
            charIndex += 1;
        }
        typingAPhase = false;
    }
}
