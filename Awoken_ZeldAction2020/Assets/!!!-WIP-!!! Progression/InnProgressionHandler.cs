using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnProgressionHandler : BasicProgressionHandler
{
    [SerializeField]
    GameObject PNJ_Customers = null;
    [SerializeField]
    GameObject PNJ_Ulfric = null;
    [SerializeField]
    GameObject PNJ_ZeusDishes = null;

    [SerializeField]
    BasicCutSceneManager CutScene_ZeusReveal = null;


    protected override void OnVegetablesEnd()
    {
        PNJ_Customers.SetActive(true);
        PNJ_Ulfric.SetActive(false);
        CutScene_ZeusReveal.gameObject.SetActive(true);
        CutScene_ZeusReveal.StartCutScene();
        CutScene_ZeusReveal.onCutSceneEnd.AddListener(BannerShow);
    }

    protected override void OnEndAdventure()
    {
        PNJ_ZeusDishes.SetActive(true);
        base.OnEndAdventure();
    }

}
