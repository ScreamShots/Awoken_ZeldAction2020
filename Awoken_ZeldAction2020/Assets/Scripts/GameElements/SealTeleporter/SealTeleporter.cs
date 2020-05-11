using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SealTeleporter : MonoBehaviour
{
    #region Variables
    
    public bool playerInZone = false;

    InterractionButton buttonDisplay;

    [HideInInspector] public bool canPlayTPAnimation = false;
    private bool cantTP = false;
    public bool isInOlympe;

    #endregion

    void Start()
    {
        buttonDisplay = GetComponentInChildren<InterractionButton>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.transform != detectedElement.transform.root)
        {
            if(detectedElement.transform.parent.tag == "Player" && detectedElement.tag == "CollisionDetection" && detectedElement != null)
            {
                playerInZone = true;
                buttonDisplay.ShowButton();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.transform != detectedElement.transform.root)
        {
            if (detectedElement.transform.parent.tag == "Player" && detectedElement.tag == "CollisionDetection" && detectedElement != null)
            {
                playerInZone = false;
                buttonDisplay.HideButton();
            }
        }
    }

    private void Update()
    {
        if(playerInZone && Input.GetButtonDown("Interraction") && !cantTP)
        {
            cantTP = true;
            buttonDisplay.HideButton();
            StartCoroutine(TeleportPlayer());
        }
    }

    private void LoadScene()
    {
        if (!isInOlympe)
        {
            if (!ProgressionManager.Instance.firstBattleZeus)
            {
                SceneHandler.Instance.SceneTransition("Olympe_Floor_Boss", 0);
            }
            else if (!ProgressionManager.Instance.transformFirstStatue)
            {
                SceneHandler.Instance.SceneTransition("Olympe_Floor_1", 0);
            }
            else if (!ProgressionManager.Instance.transformSecondStatue)
            {
                SceneHandler.Instance.SceneTransition("Olympe_Floor_2", 0);
            }
            else if(!ProgressionManager.Instance.unlockCharge && !ProgressionManager.Instance.openThirdFloorGate)
            {
                SceneHandler.Instance.SceneTransition("Olympe_Floor_Boss", 0);
            }
            else if (ProgressionManager.Instance.openThirdFloorGate)
            {
                SceneHandler.Instance.SceneTransition("Olympe_Floor_Boss", 0);
            }
        }
        else
        {
            if (!ProgressionManager.Instance.transformFirstStatue)
            {
                ProgressionManager.Instance.transformFirstStatue = true;
            }
            else if (!ProgressionManager.Instance.transformSecondStatue)
            {
                ProgressionManager.Instance.transformSecondStatue = true;
            }
            else if (ProgressionManager.Instance.openThirdFloorGate)
            {
                SceneHandler.Instance.SceneTransition("Olympe_Floor_Boss", 0);
                return;
            }

            SceneHandler.Instance.SceneTransition("Region_1", 4);
        }

    }

    IEnumerator TeleportPlayer()
    {
        GameManager.Instance.gameState = GameManager.GameState.Dialogue;
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        canPlayTPAnimation = true;
        
        yield return new WaitForSeconds(0.1f);
        canPlayTPAnimation = false;

        yield return new WaitForSeconds(0.8f);
        GameManager.Instance.gameState = GameManager.GameState.Running;
        cantTP = false;

        LoadScene();
    }
}
