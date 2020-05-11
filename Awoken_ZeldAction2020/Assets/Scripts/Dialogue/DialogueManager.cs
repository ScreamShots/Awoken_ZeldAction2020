using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Dialogue currentDialogue = null;

    [Space]

    #region Requiered Elements
    [Header("Requiered Elements")]
    #pragma warning disable CS0414
    [SerializeField] bool showRequieredElement = false;
    #pragma warning restore CS0414

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    GameObject dialogueUINoFaceDown = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    GameObject dialogueUINoFaceUp = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    GameObject dialogueUIDown = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    GameObject dialogueUIUp = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    Image faceDisplayDown = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    Image faceDisplayUp = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    TextMeshProUGUI textBoxNoFaceDown = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    TextMeshProUGUI textBoxNoFaceUp = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    TextMeshProUGUI textBoxDown = null;

    [SerializeField] [ConditionalHide("showRequieredElement", true)]
    TextMeshProUGUI textBoxUp = null;
    #endregion

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
    bool restartGameplay = true;
    Dialogue.DialogueUIPos currentDialoguePos = Dialogue.DialogueUIPos.Down;

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

    public void StartDialogue(Dialogue thisDialogue, DialogueTrigger thisTrigger, bool RestartGameplay)
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
        currentDialoguePos = thisDialogue.displayPos;

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
            switch (currentDialoguePos)
            {
                case Dialogue.DialogueUIPos.Down:
                    dialogueUIDown.SetActive(false);
                    break;
                case Dialogue.DialogueUIPos.Up:
                    dialogueUIUp.SetActive(false);
                    break;
                default:
                    break;
            }                    
        }
        else
        {
            switch (currentDialoguePos)
            {
                case Dialogue.DialogueUIPos.Down:
                    dialogueUINoFaceDown.SetActive(false);
                    break;
                case Dialogue.DialogueUIPos.Up:
                    dialogueUINoFaceUp.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        processingDialogue = false;
        if (!restartGameplay)
        {
            GameManager.Instance.gameState = GameManager.GameState.Running;
        }
    }

    public void NextDialoguePhase()
    {
        StopAllCoroutines();

        if (typingAPhase)
        {
            targetedTextBox.text = currentDialogue.talkPhases[dialoguePhaseIndex].sentence;
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

            switch (currentDialoguePos)
            {
                case Dialogue.DialogueUIPos.Down:
                    if (currentDialogue.talkPhases[dialoguePhaseIndex].faceImage == null)
                    {
                        if (!dialogueUINoFaceDown.activeInHierarchy)
                        {
                            dialogueUINoFaceDown.SetActive(true);
                            dialogueUIDown.SetActive(false);
                        }
                        targetedTextBox = textBoxNoFaceDown;
                        dialogueWithFace = false;
                    }
                    else if (currentDialogue.talkPhases[dialoguePhaseIndex].faceImage != null)
                    {
                        if (!dialogueUIDown.activeInHierarchy)
                        {
                            dialogueUIDown.SetActive(true);
                            dialogueUINoFaceDown.SetActive(false);
                        }
                        targetedTextBox = textBoxDown;
                        faceDisplayDown.sprite = currentDialogue.talkPhases[dialoguePhaseIndex].faceImage;
                        dialogueWithFace = true;
                    }
                    break;
                case Dialogue.DialogueUIPos.Up:
                    if (currentDialogue.talkPhases[dialoguePhaseIndex].faceImage == null)
                    {
                        if (!dialogueUINoFaceUp.activeInHierarchy)
                        {
                            dialogueUINoFaceUp.SetActive(true);
                            dialogueUIUp.SetActive(false);
                        }
                        targetedTextBox = textBoxNoFaceUp;
                        dialogueWithFace = false;
                    }
                    else if (currentDialogue.talkPhases[dialoguePhaseIndex].faceImage != null)
                    {
                        if (!dialogueUIUp.activeInHierarchy)
                        {
                            dialogueUIUp.SetActive(true);
                            dialogueUINoFaceUp.SetActive(false);
                        }
                        targetedTextBox = textBoxUp;
                        faceDisplayUp.sprite = currentDialogue.talkPhases[dialoguePhaseIndex].faceImage;
                        dialogueWithFace = true;
                    }
                    break;
                default:
                    break;
            }
           
            targetedTextBox.text = "<color=#00000000>" + currentDialogue.talkPhases[dialoguePhaseIndex].sentence + "<color=#00000000>";
            StartCoroutine(TypeDialogePhase());
        }
    }

    IEnumerator TypeDialogePhase()
    {
        int charIndex = 1;
        typingAPhase = true;

        foreach (char letter in currentDialogue.talkPhases[dialoguePhaseIndex].sentence)
        {
            targetedTextBox.text = currentDialogue.talkPhases[dialoguePhaseIndex].sentence.Substring(0, charIndex) +"<color=#00000000>" + currentDialogue.talkPhases[dialoguePhaseIndex].sentence.Substring(charIndex) + "<color=#00000000>";
            yield return new WaitForSeconds(typeSpeed);
            charIndex += 1;
        }
        typingAPhase = false;
    }
}
