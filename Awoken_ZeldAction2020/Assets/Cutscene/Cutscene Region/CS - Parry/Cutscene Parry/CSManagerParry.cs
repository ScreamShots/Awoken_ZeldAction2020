using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerParry : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject cinemachineAltar = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        cinemachineAltar.SetActive(false);
        playerUI.SetActive(true);

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneParry")]
    public override void StartCutScene()
    {
        cinemachineAltar.SetActive(true);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
