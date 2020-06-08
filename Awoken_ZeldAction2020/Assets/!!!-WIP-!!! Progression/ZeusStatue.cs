using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusStatue : MonoBehaviour
{
    [SerializeField]
    InterractionButton thisButton = null;
    [SerializeField]
    Animator thisAnimator = null;
    [SerializeField]
    bool canBeActivated = false;
    bool isActivated = false;
    bool playerIsHere = false;

    private void Update()
    {
        if(playerIsHere && !isActivated && Input.GetButtonDown("Interraction") && canBeActivated)
        {
            StartCoroutine(ActivateStatue());
        }
        else if (playerIsHere && isActivated && Input.GetButtonDown("Interraction") && canBeActivated)
        {
            if(GameManager.Instance.gameState == GameManager.GameState.Running && PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.neutral)
            {
                GameManager.Instance.ActiveUpgradeMenu();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (canBeActivated)
            {
                thisButton.ShowButton();
            }

            playerIsHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (canBeActivated)
            {
                thisButton.HideButton();
            }

            playerIsHere = false;
        }

    }

    IEnumerator ActivateStatue()
    {
        thisButton.HideButton();
        canBeActivated = false;
        thisAnimator.SetTrigger("Transform");
        isActivated = true;

        yield return new WaitForSeconds(0.8f);

        if (playerIsHere)
        {
            thisButton.ShowButton();
        }

        canBeActivated = true;
    }

}
