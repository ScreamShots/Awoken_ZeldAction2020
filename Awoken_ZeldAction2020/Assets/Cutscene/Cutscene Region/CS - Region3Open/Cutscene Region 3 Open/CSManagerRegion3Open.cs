using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerRegion3Open : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject Cinemachine = null;

    override public void EndOfCutScene()
    {
        Cinemachine.SetActive(false);

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneRegion3Open")]
    public override void StartCutScene()
    {
        Cinemachine.SetActive(true);

        base.StartCutScene();
    }
}
