using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleProgressionHandler : BasicProgressionHandler
{
    [SerializeField]
    BasicCutSceneManager CutScene_TempleFirstEntrance = null;
    [SerializeField]
    BasicCutSceneManager CutScene_TempleSecondEntrance = null;

    protected override void OnZeusReveal()
    {
        CutScene_TempleFirstEntrance.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(CutScene_TempleFirstEntrance.StartCutScene);
        CutScene_TempleFirstEntrance.onCutSceneEnd.AddListener(BannerShow);
        
    }

    protected override void OnCaveOut()
    {
        CutScene_TempleSecondEntrance.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(CutScene_TempleSecondEntrance.StartCutScene);
        CutScene_TempleSecondEntrance.onCutSceneEnd.AddListener(BannerShow);

    }
}
