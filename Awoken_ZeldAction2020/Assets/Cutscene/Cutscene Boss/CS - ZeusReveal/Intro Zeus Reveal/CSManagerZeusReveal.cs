using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerZeusReveal : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject shield = null;
    [SerializeField]
    GameObject lightning = null;
    [SerializeField]
    GameObject cutSceneUI = null;

    override public void EndOfCutScene()
    {        
        zeus.SetActive(false);
        shield.SetActive(false);
        lightning.SetActive(false);
        cutSceneUI.SetActive(false);

        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.ZeusReveal;
        ProgressionManager.Instance.PlayerCapacity["Shield"] = true;
        PlayerManager.Instance.ActivateShield();
    }
}
