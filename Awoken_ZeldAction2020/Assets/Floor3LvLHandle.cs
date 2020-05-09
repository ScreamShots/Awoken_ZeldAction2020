using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor3LvLHandle : MonoBehaviour
{
    public DialogueTrigger dial1;
    public DialogueTrigger dial2;

    private void Start()
    {
        if (ProgressionManager.Instance.openThirdFloorGate == true)
        {
            dial1.gameObject.SetActive(false);
            dial2.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (dial2.dialogueEnded && dial2.gameObject.activeInHierarchy)
        {
            ProgressionManager.Instance.openThirdFloorGate = true;
        }
    }
}
