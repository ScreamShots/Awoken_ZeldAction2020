using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerRegion2Open : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject Cinemachine = null;

    override public void EndOfCutScene()
    {
        Cinemachine.SetActive(false);

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneRegion2Open")]
    public override void StartCutScene()
    {
        GameManager.Instance.securityChangeState = true;
        Cinemachine.SetActive(true);

        base.StartCutScene();
    }


}
