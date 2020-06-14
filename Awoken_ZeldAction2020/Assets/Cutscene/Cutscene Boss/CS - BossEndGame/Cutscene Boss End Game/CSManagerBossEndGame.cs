using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossEndGame : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject shield = null;
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;
    [SerializeField]
    GameObject bossUI = null;
    [SerializeField]
    GameObject realBossRendering = null;
    [SerializeField]
    VideoStream outro = null;

    override public void EndOfCutScene()
    {
        zeus.SetActive(false);
        shield.SetActive(false);      
        cinemachine.SetActive(false);
        outro.onVideoEnd.AddListener(PostCreditAction);
        StartCoroutine(outro.PlayVideo());
        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.EndAdventure;
        ProgressionManager.Instance.SaveTheProgression();

        GameManager.Instance.gameState = GameManager.GameState.Dialogue;

       
    }

    [ContextMenu("StartCutSceneBossEndGame")]
    public override void StartCutScene()
    {
        realBossRendering.SetActive(false);
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        zeus.SetActive(true);

        base.StartCutScene();
    }

    public void StopBossMusic()
    {
        SoundManager.Instance.FadeOutMusic(3f);
    }

    public void PostCreditAction()
    {
        GameManager.Instance.areaToLoad = 0;
        GameManager.Instance.sceneToLoad = 0;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();

    }
}
