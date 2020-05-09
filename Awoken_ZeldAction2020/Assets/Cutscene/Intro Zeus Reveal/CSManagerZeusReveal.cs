using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerZeusReveal : MonoBehaviour
{
    [SerializeField]
    GameObject[] allCutScenePart;
    [SerializeField]
    DialogueTrigger[] allDialogue;
    [SerializeField]
    GameObject zeus;
    [SerializeField]
    GameObject shield;
    [SerializeField]
    GameObject lightning;
    [SerializeField]
    GameObject cutSceneUI;

    int dialogueIndex;
    GameObject nextCutScenePart;

    private void Update()
    {
        if(dialogueIndex < allDialogue.Length)
        {
            TestDialogue();
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
        StartCoroutine(SecurityTime());
        nextCutScenePart = nextPart;
    }

    IEnumerator SecurityTime()
    {
        yield return new WaitForSeconds(0.5f);
        allDialogue[dialogueIndex].StartDialogue();
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

    void TestDialogue()
    {
        if (allDialogue[dialogueIndex].dialogueEnded)
        {
            nextCutScenePart.SetActive(true);
            dialogueIndex++;
        }
    }
}
