using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AubergeLvlHandler : MonoBehaviour
{
    public GameObject Pnj;
    public GameObject CutSceneZeus;

    private void Start()
    {
        if (!ProgressionManager.Instance.vegetablesDone)
        {
            Pnj.SetActive(false);
        }
        else if(!ProgressionManager.Instance.zeusRevealCutsceneDone)
        {
            CutSceneZeus.SetActive(true);
        }
    }
}
