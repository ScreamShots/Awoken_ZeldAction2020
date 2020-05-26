using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_Vegetables : MonoBehaviour
{
    [SerializeField]
    Dialogue endVegeDialogue = null;

    [SerializeField]
    IsEnigma1Done enigmaOne = null;

    bool l_enigmaOneDone = false;

    private void Start()
    {
        l_enigmaOneDone = enigmaOne.isEnigmaDone;

        if (enigmaOne.isEnigmaDone)
        {
            GetComponentInChildren<DialogueTrigger>().dialogueToPlay = endVegeDialogue;
        }
    }

    private void Update()
    {
        if(enigmaOne.isEnigmaDone && l_enigmaOneDone != enigmaOne.isEnigmaDone)
        {
            l_enigmaOneDone = enigmaOne.isEnigmaDone;
            GetComponentInChildren<DialogueTrigger>().dialogueToPlay = endVegeDialogue;
        }
    }
}
