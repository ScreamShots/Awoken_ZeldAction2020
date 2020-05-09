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
        /*if ()                                                   //put here save bool for tp Player relative to first passage in Temple || if Player is in the last floor until Boss
        {
            SceneManager.LoadScene("Olympe_Floor_Boss");
        }
        else if ()                                              //put here save bool for tp Player relative to second passage in Temple
        {
            SceneManager.LoadScene("Olympe_Floor_1");
        }
        else if ()                                              //put here save bool for tp Player if he's in first floor
        {
            SceneManager.LoadScene("Olympe_Floor_2");
        }
        else if ()                                               //put here save bool for tp Player if he's in second floor
        {
            SceneManager.LoadScene("Olympe_Floor_3");
        }*/
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
