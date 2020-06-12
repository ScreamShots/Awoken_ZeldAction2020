using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class BasicCutSceneManager : MonoBehaviour
{
    [Header("CutScene Parts")]

    [SerializeField]
    GameObject[] allCutScenePart = null;
    [SerializeField]
    DialogueTrigger[] allDialogue = null;

    GameObject nextCutScenePart = null;
    [HideInInspector] public PlayableDirector currentPlayableDirector = null;
    [HideInInspector] public bool inDialogue = false;
    [HideInInspector] public bool inQTE = false;
    int dialogueIndex;

    [HideInInspector]
    public UnityEvent onCutSceneEnd = null;

    private void Awake()
    {
        if(onCutSceneEnd == null)
        {
            onCutSceneEnd = new UnityEvent();
        }
    }

    public virtual void Update()
    {
        if (dialogueIndex < allDialogue.Length)
        {
            LaunchDialogue();
        }

        if (Input.GetButtonDown("MenuValidate") && inDialogue && !inQTE)
        {
            if(currentPlayableDirector.playableGraph.IsValid())
            {
                currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(2);
            }
        }
        else if (Input.GetButtonUp("MenuValidate") && inDialogue && !inQTE)
        {
            if (currentPlayableDirector.playableGraph.IsValid())
            {
                currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
            }
        }
    }

    [ContextMenu("StartCutScene")]
    public virtual void StartCutScene()
    {
        PlayerManager.Instance.PlayerInitializeCutScene();
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        GameManager.Instance.gameState = GameManager.GameState.Dialogue;
        nextCutScenePart = null;
        inDialogue = true;
        dialogueIndex = 0;
        currentPlayableDirector = allCutScenePart[0].GetComponent<PlayableDirector>();
        allCutScenePart[0].SetActive(true);
    }
    public virtual void LaunchNextPart(GameObject nextPart)
    {
        allDialogue[dialogueIndex].StartDialogue();
        nextCutScenePart = nextPart;
        currentPlayableDirector = nextCutScenePart.GetComponent<PlayableDirector>();
    }
    protected virtual void LaunchDialogue()
    {
        if (allDialogue[dialogueIndex].dialogueEnded)
        {
            nextCutScenePart.SetActive(true);
            dialogueIndex++;
        }
    }
    public virtual void EndOfCutScene()
    {
        foreach (GameObject cutscenePart in allCutScenePart)
        {
            cutscenePart.SetActive(false);
        }
        foreach (DialogueTrigger dialogue in allDialogue)
        {
            dialogue.dialogueEnded = false;
            dialogue.gameObject.SetActive(false);
        }
        PlayerManager.Instance.PlayerEndCutScene();
        GameManager.Instance.gameState = GameManager.GameState.Running;
        inDialogue = false;
        onCutSceneEnd.Invoke();
        onCutSceneEnd.RemoveAllListeners();
    }
}
