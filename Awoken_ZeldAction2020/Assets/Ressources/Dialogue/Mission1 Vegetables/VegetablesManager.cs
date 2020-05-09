using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetablesManager : MonoBehaviour
{
    [SerializeField] Dialogue endDialogue;
    [SerializeField] DialogueTrigger pnjTrigger;
    [SerializeField] DialogueTrigger startTrigger;
    [SerializeField] IsEnigma1Done enigma1;

    bool dialogueChanged;



    private void Start()
    {
        if (!ProgressionManager.Instance.vegetablesFirstDialogueDone)
        {
            startTrigger.StartDialogue();
            ProgressionManager.Instance.vegetablesFirstDialogueDone = true;
        }
        
    }

    private void Update()
    {
        if (enigma1.isEnigmaDone && !dialogueChanged)
        {
            dialogueChanged = true;
            pnjTrigger.dialogueToPlay = endDialogue;
            pnjTrigger.dialogueEnded = false;
        }

        if(dialogueChanged && pnjTrigger.dialogueEnded)
        {
            ProgressionManager.Instance.vegetablesDone = true;
            SceneHandler.Instance.SceneTransition("MAH_Auberge", 0);
        }
    }

}
