using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial1Olympe1 : MonoBehaviour
{
    private DialogueTrigger trigger;
    [SerializeField]
    private Collider2D triggerZone;
    void Start()
    {
        trigger = GetComponentInChildren<DialogueTrigger>();
    }

    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            trigger.StartDialogue();
            PlayerMovement.playerRgb.velocity = Vector2.zero;
            triggerZone.enabled = false;
        }
    }

}
