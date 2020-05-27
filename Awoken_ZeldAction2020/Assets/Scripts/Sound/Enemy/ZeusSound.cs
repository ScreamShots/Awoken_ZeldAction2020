using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ZeusSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Damage Sound")]
    public AudioClip[] zeusDamage;
    [Range(0f, 1f)] public float zeusDamageVolume = 0.5f;

    BossManager bossManagerScript;

    private bool zeusTakingDmg = false;

    #endregion

    void Start()
    {
        bossManagerScript = GetComponentInParent<BossManager>();
    }

    void Update()
    {
        ZeusTakeDamage();
    }

    void ZeusTakeDamage()
    {
        if (bossManagerScript.canFlash)
        {
            if (!zeusTakingDmg)
            {
                zeusTakingDmg = true;
                SoundManager.Instance.PlayRandomSfx(zeusDamage, zeusDamageVolume);
            }
        }
        else
        {
            zeusTakingDmg = false;
        }
    }
}
