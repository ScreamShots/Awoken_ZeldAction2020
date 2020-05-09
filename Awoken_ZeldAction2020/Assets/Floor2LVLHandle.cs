using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor2LVLHandle : MonoBehaviour
{
    public ZeusBigor thisStatue;
    public DialogueTrigger dial1;
    public DialogueTrigger dial2;

    private void Start()
    {
        if (ProgressionManager.Instance.transformSecondStatue == true)
        {
            thisStatue.isStatueActivated = true;
        }

        if (ProgressionManager.Instance.openSecondFloorGate == true)
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
            ProgressionManager.Instance.openSecondFloorGate = true;
        }
    }
}
