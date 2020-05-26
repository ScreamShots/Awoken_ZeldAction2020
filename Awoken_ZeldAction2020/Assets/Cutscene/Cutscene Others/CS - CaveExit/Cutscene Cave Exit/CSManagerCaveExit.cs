using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerCaveExit : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject Cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        Cinemachine.SetActive(false);
        playerUI.SetActive(true);

        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.CaveOut;
    }

    [ContextMenu("StartCutSceneCaveExit")]
    public override void StartCutScene()
    {
        Cinemachine.SetActive(true);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
