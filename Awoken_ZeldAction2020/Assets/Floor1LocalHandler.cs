using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor1LocalHandler : MonoBehaviour
{
    public ZeusBigor thisStatue;
    public DialogueTrigger dial1;
    public DialogueTrigger dial2;

    private void Start()
    {
        if(ProgressionManager.Instance.transformFirstStatue == true)
        {
            thisStatue.isStatueActivated = true;
        }

        if(ProgressionManager.Instance.openFirstFloorGate == true)
        {
            dial1.gameObject.SetActive(false);
            dial2.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (dial2.dialogueEnded && dial2.gameObject.activeInHierarchy)
        {
            SceneHandler.Instance.SceneTransition("Region_1", 4);
            ProgressionManager.Instance.openFirstFloorGate = true;
        }
    }
}
