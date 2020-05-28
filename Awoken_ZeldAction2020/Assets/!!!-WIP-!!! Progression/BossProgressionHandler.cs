using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProgressionHandler : BasicProgressionHandler
{
    [SerializeField]
    BasicCutSceneManager CutScene_FirstFightZeus = null;

    protected override void OnShieldBlockUnlock()
    {
        CutScene_FirstFightZeus.gameObject.SetActive(true);
        CutScene_FirstFightZeus.StartCutScene();
    }
    
}
