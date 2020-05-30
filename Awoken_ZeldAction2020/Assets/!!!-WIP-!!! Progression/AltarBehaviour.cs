using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBehaviour : MonoBehaviour
{   
    InterractionButton linkedInterractionButton = null;
    bool playerIsHere = false;
    [HideInInspector]
    public bool buttonActivated = false;

    [SerializeField] BasicCutSceneManager linkedCutsceneManager = null;
    [SerializeField] Animator altarAnimator = null;



    private void Start()
    {

        linkedInterractionButton = GetComponentInChildren<InterractionButton>();
    }

    private void Update()
    {
        if (playerIsHere && Input.GetButtonDown("Interraction") && buttonActivated)
        {
            buttonActivated = false;
            playerIsHere = false;
            linkedInterractionButton.HideButton();
            linkedCutsceneManager.StartCutScene();   
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player") && buttonActivated)
        {
            linkedInterractionButton.ShowButton();
            playerIsHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player") && buttonActivated)
        {
            linkedInterractionButton.HideButton();
            playerIsHere = false;
        }            
    }


    public void DesactivateAltarGraph()
    {
        altarAnimator.SetTrigger("Altar_Broken_noShield");
    }
}
