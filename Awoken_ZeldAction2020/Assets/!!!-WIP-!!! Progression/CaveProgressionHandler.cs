using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveProgressionHandler : BasicProgressionHandler
{
    [SerializeField]
    BasicCutSceneManager CutScene_FallFromOlympus = null;

    protected override void OnShieldBlockUnlock()
    {
        CutScene_FallFromOlympus.gameObject.SetActive(true);
        CutScene_FallFromOlympus.StartCutScene();
        CutScene_FallFromOlympus.onCutSceneEnd.AddListener(BannerShow);
    }
}
