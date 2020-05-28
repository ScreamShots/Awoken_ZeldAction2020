using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerCave : BasicCutSceneManager
{
    public override void EndOfCutScene()
    {
        base.EndOfCutScene();

        ProgressionManager.Instance.PlayerCapacity["Block"] = true;
        PlayerManager.Instance.ActivateBlock();
    }

}
