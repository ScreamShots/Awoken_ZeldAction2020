using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TransitionZone : MonoBehaviour
{
    public AreaManager nextAreaManager;
    AreaManager linkedAreaManager;
    PlayerShield playerShieldScript;

    bool isTheStart = false;
    bool needToTransit = false;


    private void Start()
    {

        playerShieldScript = PlayerManager.Instance.gameObject.GetComponent<PlayerShield>();
        linkedAreaManager = GetComponentInParent<AreaManager>();
    }

    private void Update()
    {
        if (needToTransit == true)
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Running)
            {
                if (!PlayerStatusManager.Instance.isAttacking && !PlayerStatusManager.Instance.isKnockBacked)
                {
                    OutTransition();
                    needToTransit = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection"))
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Running)
            {
                if (!PlayerStatusManager.Instance.isAttacking && !PlayerStatusManager.Instance.isKnockBacked)
                {
                    OutTransition();
                }
                else if (PlayerStatusManager.Instance.isKnockBacked || PlayerStatusManager.Instance.isAttacking)
                {
                    needToTransit = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection"))
        {
            if (GameManager.Instance.gameState == GameManager.GameState.LvlFrameTransition)
            {
                if (!isTheStart)
                {
                    LvlManager.Instance.canEndTransition = true;
                    PlayerMovement.playerRgb.velocity = Vector2.zero;
                }
                else
                {
                    isTheStart = false;
                }
            }
        }
    }

    void OutTransition()
    {
        if (PlayerStatusManager.Instance.isBlocking)
        {
            playerShieldScript.DesactivateBlock();
        }

        GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
        EnemyManager.Instance.DestroyAllProjectile();
        linkedAreaManager.UnLoadArea();
        isTheStart = true;
        nextAreaManager.ActivateCam();
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        PlayerMovement.playerRgb.velocity = transform.up * PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>().speed * Time.fixedDeltaTime;
    }
}
