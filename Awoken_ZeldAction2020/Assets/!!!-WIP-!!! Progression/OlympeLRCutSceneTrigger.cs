using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlympeLRCutSceneTrigger : MonoBehaviour
{
    [SerializeField]
    BasicCutSceneManager cutsceneToTrigger = null;

    PlayerCharge playerChargeScript;

    void Start()
    {
        playerChargeScript = PlayerManager.Instance.GetComponent<PlayerCharge>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        if (cutsceneToTrigger.gameObject.activeInHierarchy)
        {
            if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.charge)
            {
                playerChargeScript.FastEndCharge();
            }

            cutsceneToTrigger.StartCutScene();
        }
    }
}
