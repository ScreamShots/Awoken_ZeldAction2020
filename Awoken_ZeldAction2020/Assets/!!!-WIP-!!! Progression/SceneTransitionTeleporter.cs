using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransitionTeleporter : MonoBehaviour
{
    [SerializeField] [Range(0,10)]
    int targetedSceneIndex = 0;
    [SerializeField] [Range(0, 5)]
    int targetedStartZoneIndex = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            LaunchTransition();
        }
    }

    [ContextMenu("Do Transition")]
    void LaunchTransition()
    {
        SavePlayerInfos();

        GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        GameManager.Instance.areaToLoad = targetedStartZoneIndex;
        GameManager.Instance.sceneToLoad = targetedSceneIndex;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();
    }

    void SavePlayerInfos()
    {
        ProgressionManager.Instance.playerFury = PlayerManager.Instance.GetComponent<PlayerAttack>().currentFury;
        ProgressionManager.Instance.playerHp = PlayerManager.Instance.GetComponent<PlayerHealthSystem>().currentHp;
        ProgressionManager.Instance.playerStamina = PlayerManager.Instance.GetComponent<PlayerShield>().currentStamina;
    }


}
