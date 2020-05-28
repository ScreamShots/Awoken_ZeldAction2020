using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerTempleFirstWalk : BasicCutSceneManager
{
    public override void EndOfCutScene()
    {
        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.TempleFirstEntrance;
    }
}
