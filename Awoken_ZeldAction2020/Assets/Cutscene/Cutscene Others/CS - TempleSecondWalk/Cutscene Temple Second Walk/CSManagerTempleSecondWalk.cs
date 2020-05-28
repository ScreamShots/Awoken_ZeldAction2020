using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerTempleSecondWalk : BasicCutSceneManager
{
    public override void EndOfCutScene()
    {
        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.TempleSecondEntrance;
    }
}
