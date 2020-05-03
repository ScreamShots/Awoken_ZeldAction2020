using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du Lightning Strike
/// </summary>


public class LightningSound : MonoBehaviour
{
    private PrefabSoundManager lightningManager;
    private LightningComportement lightning;

    private bool l_thunderIsSlam;

    // Start is called before the first frame update
    void Start()
    {
        lightningManager = GetComponent<PrefabSoundManager>();
        lightning = GetComponentInParent<LightningComportement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(l_thunderIsSlam != lightning.thunderIsSlam)
        {
            if(lightning.thunderIsSlam == true)
            {
                ThunderStruck();
            }
        }
        l_thunderIsSlam = lightning.thunderIsSlam;
    }

    void ThunderStruck()
    {
        if(lightning.thunderIsSlam == true)
        {
            SoundManager.Instance.Play("ThunderStruck");
        }
    }
}
