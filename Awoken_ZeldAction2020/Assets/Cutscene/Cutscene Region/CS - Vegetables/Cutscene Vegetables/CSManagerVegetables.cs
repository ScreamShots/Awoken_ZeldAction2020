using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerVegetables : BasicCutSceneManager
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

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.VegeteablesStart;
    }

    [ContextMenu("StartCutSceneVegetables")]
    public override void StartCutScene()
    {
        Cinemachine.SetActive(true);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
