﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons de Zeus
/// </summary>

public class ZeusSound : MonoBehaviour
{
    private BossState1 bossState1;
    private BossState2 bossState2;

    private bool l_animThunder;
    private bool l_animShockwave;
    private bool l_isPunching;
    private bool l_animShoot;
    private bool l_animWall;
    // Start is called before the first frame update
    void Start()
    {
        bossState1 = GetComponentInParent<BossState1>();
        bossState2 = GetComponentInParent<BossState2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(l_animThunder != bossState1.animThunder)
        {
            if(bossState1.animThunder == true)
            {
                SummonLightning();
            }
            l_animThunder = bossState1.animThunder;
        }

        if(l_animShockwave != bossState1.animShockWave)
        {
            if(bossState1.animShockWave == true)
            {
                SummonShockwave();
            }
            l_animShockwave = bossState1.animShockWave;
        }

        if (l_isPunching != bossState2.isPunching)
        {
            if (bossState2.isPunching == true)
            {
                ZeusPunch();
            }
            l_isPunching = bossState2.isPunching;
        }

        if (l_animShoot != bossState2.animShoot)
        {
            if (bossState2.animShoot == true)
            {
                ShootBolt();
            }
            l_animShoot = bossState2.animShoot;
        }
    }

    void ZeusDamaged()
    {
        SoundManager.Instance.Play("BossTakeHit");
    }

    void SummonLightning()
    {
        SoundManager.Instance.Play("SummonLightning");

    }

    void SummonShockwave()
    {
        SoundManager.Instance.Play("SummonShockwave");
    }

    void ShootBolt()
    {
        SoundManager.Instance.Play("SummonBolt");
    }

    void ZeusPunch()
    {
        SoundManager.Instance.Play("ZeusPunch");
    }
}
