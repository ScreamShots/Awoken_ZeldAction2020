using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlympeTeleportPlateform : MonoBehaviour
{
    [SerializeField]
    InterractionButton thisButton = null;
    [SerializeField]
    Animator sealAnimator = null;

    bool playerIsHere = false;
    bool canUseTp = false;


    private void Update()
    {
        switch (ProgressionManager.Instance.thisSessionTimeLine)
        {
            case ProgressionManager.ProgressionTimeLine.TempleFirstEntrance:
                canUseTp = true;
                break;
            case ProgressionManager.ProgressionTimeLine.TempleSecondEntrance:
                canUseTp = true;
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorOneLREntrance:
                canUseTp = true;
                break;
            case ProgressionManager.ProgressionTimeLine.SecondRegionOut:
                canUseTp = true;
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorTwoLREntrance:
                canUseTp = true;
                break;
            case ProgressionManager.ProgressionTimeLine.ThirdRegionOut:
                canUseTp = true;
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorThreeLREntrance:
                canUseTp = true;
                break;
            default:
                canUseTp = false;
                break;
        }

        if (playerIsHere && Input.GetButtonDown("Interraction"))
        {
            StartCoroutine(Teleport());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canUseTp)
        {
            if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
            {
                thisButton.ShowButton();
                playerIsHere = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canUseTp)
        {
            if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
            {
                thisButton.HideButton();
                playerIsHere = false;
            }
        }
    }

    public IEnumerator Teleport()
    {
        GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
        PlayerMovement.playerRgb.velocity = Vector2.zero;

        thisButton.HideButton();
        playerIsHere = false;
        sealAnimator.SetTrigger("Teleporter_Activate");

        yield return new WaitForSeconds(0.7f);

        PlayerManager.Instance.gameObject.GetComponentInChildren<PlayerAnimator>().Tp();

        yield return new WaitForSeconds(0.5f);


        switch (ProgressionManager.Instance.thisSessionTimeLine)
        {
            case ProgressionManager.ProgressionTimeLine.TempleFirstEntrance:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.ShieldBlockUnlock; 
                LaunchTransition(10, 0);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            case ProgressionManager.ProgressionTimeLine.TempleSecondEntrance:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorOneStart;
                LaunchTransition(7, 0);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorOneLREntrance:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorOneEnd;
                LaunchTransition(4, 4);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            case ProgressionManager.ProgressionTimeLine.SecondRegionOut:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorTwoStart;
                LaunchTransition(8, 0);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorTwoLREntrance:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorTwoEnd;
                LaunchTransition(4, 4);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            case ProgressionManager.ProgressionTimeLine.ThirdRegionOut:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorThreeStart;
                LaunchTransition(9, 0);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorThreeLREntrance:
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorThreeEnded;
                LaunchTransition(10, 0);
                ProgressionManager.Instance.SaveTheProgression();
                break;
            default:
                Debug.Log("We can't use teleporter right now");
                break;
        }
    }

    void LaunchTransition(int targetedSceneIndex , int targetedStartZoneIndex)
    {
        SavePlayerInfos();
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
