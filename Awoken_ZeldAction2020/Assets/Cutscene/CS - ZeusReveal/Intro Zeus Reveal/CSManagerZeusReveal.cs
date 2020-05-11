using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerZeusReveal : MonoBehaviour
{
    [Header("CutScene Parts")]
    [SerializeField]
    GameObject[] allCutScenePart = null;
    [SerializeField]
    DialogueTrigger[] allDialogue = null;

    [Header("CutScene Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject shield = null;
    [SerializeField]
    GameObject lightning = null;
    [SerializeField]
    GameObject cutSceneUI = null;


    int dialogueIndex;
    GameObject nextCutScenePart;

    private void Update()
    {
        if(dialogueIndex < allDialogue.Length)
        {
            LaunchDialogue();
        }
    }

    private void Start()
    {
        dialogueIndex = 0;
        PlayerManager.Instance.StartCutScene();
        GameManager.Instance.gameState = GameManager.GameState.Dialogue;
        allCutScenePart[0].SetActive(true);
    }

    public void LaunchNextPart(GameObject nextPart)
    {
        allDialogue[dialogueIndex].StartDialogue();
        nextCutScenePart = nextPart;
    }

    public void EndOfCutScene()
    {
        foreach(GameObject cutscenePart in allCutScenePart)
        {
            cutscenePart.SetActive(false);
        }
        zeus.SetActive(false);
        shield.SetActive(false);
        lightning.SetActive(false);
        cutSceneUI.SetActive(false);
        PlayerManager.Instance.EndCutScene();
    }

    void LaunchDialogue()
    {
        if (allDialogue[dialogueIndex].dialogueEnded)
        {
            nextCutScenePart.SetActive(true);
            dialogueIndex++;
        }
    }
}
