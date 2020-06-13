using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_Vegetables : MonoBehaviour
{
    [SerializeField]
    Dialogue endVegeDialogue = null;

    [SerializeField]
    IsEnigma1Done enigmaOne = null;

    [SerializeField]
    bool l_enigmaOneDone = false;

    DialogueTrigger pnjDialogueTrigger = null;
    bool changedDialogue = false;
    bool sceneTransit = false;

    private void Start()
    {
        pnjDialogueTrigger = GetComponentInChildren<DialogueTrigger>();
    }


    private void Update()
    {
        if(enigmaOne.isEnigmaDone && l_enigmaOneDone != enigmaOne.isEnigmaDone)
        {
            l_enigmaOneDone = enigmaOne.isEnigmaDone;
            pnjDialogueTrigger.dialogueEnded = false;
            pnjDialogueTrigger.dialogueToPlay = endVegeDialogue;
            pnjDialogueTrigger.restartGameplayAtTheEnd = false;
            pnjDialogueTrigger.target = "Exclamation";
            pnjDialogueTrigger.canShowReaction = true;
            pnjDialogueTrigger.SetReactionIcon("Exclamation_On");
            changedDialogue = true;
        }

        if (enigmaOne.isEnigmaDone && pnjDialogueTrigger.dialogueEnded && changedDialogue && !sceneTransit)
        {
            sceneTransit = true;
            ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.VegetablesEnd;
            GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
            PlayerMovement.playerRgb.velocity = Vector2.zero;
            GameManager.Instance.areaToLoad = 0;
            GameManager.Instance.sceneToLoad = 1;
            GameManager.Instance.blackMelt.gameObject.SetActive(true);
            GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
            GameManager.Instance.blackMelt.MeltIn();
        }
    }
}
