using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossFirstMeet : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject lightning = null;
    /*[SerializeField]
    GameObject cutSceneUI = null;*/
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        zeus.SetActive(false);
        lightning.SetActive(false);
        //cutSceneUI.SetActive(false);
        cinemachine.SetActive(false);
        //playerUI.SetActive(true);

        // I commented that so it transit rightly with black melt of scene transition (Rémi)

        base.EndOfCutScene();

        GameManager.Instance.gameState = GameManager.GameState.LvlFrameTransition;
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        GameManager.Instance.areaToLoad = 0;
        GameManager.Instance.sceneToLoad = 2;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();
    }

    [ContextMenu("StartCutSceneBossFirstWalk")]
    public override void StartCutScene()
    {
        cinemachine.SetActive(true);
        playerUI.SetActive(false);

        base.StartCutScene();
    }

    public void StartBossMusic()
    {
        SoundManager.Instance.PlayMusic(MusicManager.Instance.arenaMusic, MusicManager.Instance.arenaMusicVolume);
    }

    public void StopBossMusic()
    {
        SoundManager.Instance.FadeOutMusic(3f);
    }
}
