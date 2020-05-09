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
        startTrigger.StartDialogue();
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
            //LaunchCinematic
        }
    }

}
