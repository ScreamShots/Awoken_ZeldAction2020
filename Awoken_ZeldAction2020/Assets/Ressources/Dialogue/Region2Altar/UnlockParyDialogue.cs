using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockParyDialogue : MonoBehaviour
{
    [SerializeField] DialogueTrigger thisTrigger;

    ProgressionManager progressionManagerScript;
    private Autel autelScript;
    private ProjectileParyBehaviour paryScript;
    public GameObject autel;
    public GameObject playerParyZone;

    private bool dialogueEnd = false;
    private bool dialogueStart = false;

    private void Start()
    {
        progressionManagerScript = GameManager.Instance.GetComponent<ProgressionManager>();

        autelScript = autel.GetComponent<Autel>();
        paryScript = PlayerManager.Instance.GetComponent<ProjectileParyBehaviour>();
    }

    private void Update()
    {
        if (autelScript.isAutelActivated && !dialogueStart)
        {
            dialogueStart = true;
            thisTrigger.StartDialogue();
            PlayerMovement.playerRgb.velocity = Vector2.zero;

            paryScript.enabled = true;
            progressionManagerScript.unlockPary = true;
            playerParyZone.SetActive(true);
        }
    }
}
