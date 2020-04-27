using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TransitionZone : MonoBehaviour
{
    CinemachineVirtualCamera thisCam;
    public CinemachineVirtualCamera nextCam;
    bool isTheStart;
    bool needToTransit;

    private void Start()
    {
        thisCam = transform.parent.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
    }


    private void Update()
    {
        if(needToTransit && !PlayerStatusManager.Instance.isKnockBacked && !PlayerStatusManager.Instance.isAttacking)
        {
            Transition();
            needToTransit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection"))
        {
            if (!GameManager.Instance.inTransition && !PlayerStatusManager.Instance.isAttacking && !PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isKnockBacked)
            {
                GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
                Debug.Log("test");
                isTheStart = true;
                GameManager.Instance.inTransition = true;
                thisCam.Priority = 0;
                nextCam.Priority = 1;
                PlayerMovement.playerRgb.velocity = Vector2.zero;
                Debug.Log(PlayerMovement.playerRgb.velocity);
                PlayerMovement.playerRgb.velocity = transform.up * PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>().speed * Time.fixedDeltaTime;
            }
            else if (PlayerStatusManager.Instance.isBlocking && !PlayerStatusManager.Instance.isBlocking)
            {
                PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().DesactivateBlock();
                GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
                Debug.Log("test");
                isTheStart = true;
                GameManager.Instance.inTransition = true;
                thisCam.Priority = 0;
                nextCam.Priority = 1;
                PlayerMovement.playerRgb.velocity = Vector2.zero;
                Debug.Log(PlayerMovement.playerRgb.velocity);
                PlayerMovement.playerRgb.velocity = transform.up * PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>().speed * Time.fixedDeltaTime;
            }
            else if(PlayerStatusManager.Instance.isAttacking || PlayerStatusManager.Instance.isKnockBacked)
            {
                if (PlayerStatusManager.Instance.isBlocking)
                {
                    PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().DesactivateBlock();
                }
                needToTransit = true;
            }

        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection"))
        {
            if (GameManager.Instance.inTransition && !isTheStart)
            {
                Debug.Log("test2");
                GameManager.Instance.gameState = GameManager.GameState.Running;
                PlayerMovement.playerRgb.velocity = Vector2.zero;
                GameManager.Instance.inTransition = false;
            }
            else if (isTheStart)
            {
                isTheStart = false;
            }
        }
    }

    void Transition()
    {
        GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
        Debug.Log("test");
        isTheStart = true;
        GameManager.Instance.inTransition = true;
        thisCam.Priority = 0;
        nextCam.Priority = 1;
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        Debug.Log(PlayerMovement.playerRgb.velocity);
        PlayerMovement.playerRgb.velocity = transform.up * PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>().speed * Time.fixedDeltaTime;
    }
}
