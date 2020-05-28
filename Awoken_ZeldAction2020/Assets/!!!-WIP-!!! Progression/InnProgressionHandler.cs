using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnProgressionHandler : BasicProgressionHandler
{
    [SerializeField]
    GameObject PNJ_Customers = null;

    [SerializeField]
    BasicCutSceneManager CutScene_ZeusReveal = null;


    protected override void OnVegetablesEnd()
    {
        PNJ_Customers.SetActive(true);
        CutScene_ZeusReveal.gameObject.SetActive(true);
        CutScene_ZeusReveal.StartCutScene();
    }

}
