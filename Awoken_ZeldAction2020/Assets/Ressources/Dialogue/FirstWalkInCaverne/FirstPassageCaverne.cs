using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPassageCaverne : MonoBehaviour
{
    ProgressionManager progressionManagerScript;

    [SerializeField] DialogueTrigger thisTrigger;

    private bool dialogueEnd = false;

    private void Start()
    {
        progressionManagerScript = GameManager.Instance.GetComponent<ProgressionManager>();
        
        if (!progressionManagerScript.undergroudCutSceneDone && !dialogueEnd)
        {
            progressionManagerScript.undergroudCutSceneDone = true;
            thisTrigger.StartDialogue();
        }
    }

    private void Update()
    {
        if (thisTrigger.dialogueEnded && !dialogueEnd)
        {
            dialogueEnd = true;
        }
    }
}
