using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPary : MonoBehaviour
{
    [SerializeField] DialogueTrigger thisTrigger;

    private bool dialogueEnd = false;
    private bool dialogueStart = false;

    public GameObject distanceLever;
    private DistanceLever distanceLeverScript;

    private void Start()
    {
        distanceLeverScript = distanceLever.GetComponent<DistanceLever>();
    }

    private void Update()
    {
        if (distanceLeverScript.isPressed && !dialogueStart)
        {
            dialogueStart = true;
            thisTrigger.StartDialogue();
            PlayerMovement.playerRgb.velocity = Vector2.zero;
        }
    }
}
