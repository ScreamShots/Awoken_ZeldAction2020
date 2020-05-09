using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AubergeLvlHandler : MonoBehaviour
{
    public GameObject Pnj;
    public GameObject CutSceneZeus;
    public AnimatorOverrideController noShieldPJ;
    public Animator PlayerAnimator;

    public Transform SpawnPointDoor;

    private void Start()
    {
        if(SceneHandler.Instance.zoneToLoad == 1)
        {
            PlayerManager.Instance.gameObject.transform.position = SpawnPointDoor.position;
        }

        if (!ProgressionManager.Instance.vegetablesDone)
        {
            Pnj.SetActive(false);
            PlayerAnimator.runtimeAnimatorController = noShieldPJ;
        }
        else if(!ProgressionManager.Instance.zeusRevealCutsceneDone)
        {
            CutSceneZeus.SetActive(true);
        }
        
    }
}
