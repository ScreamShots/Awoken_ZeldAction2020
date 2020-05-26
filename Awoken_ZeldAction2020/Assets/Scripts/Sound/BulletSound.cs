using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BulletSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Lauch Bullet Parry Sound")]
    public AudioClip bulletParry;
    [Range(0f, 1f)] public float bulletParryVolume = 0.5f;

    [Space]
    [Header("Bullet Destroy on Turret Sound")]
    public AudioClip bulletHitEnemy;
    [Range(0f, 1f)] public float bulletHitEnemyVolume = 0.5f;

    private BlockHandler blockHandlerScript;

    private bool bulletLauchedBack = false;
    #endregion

    void Start()
    {
        blockHandlerScript = GetComponentInParent<BlockHandler>();
    }

    void Update()
    {
        BulletLauched();
    }

    void BulletLauched()
    {
        if (blockHandlerScript.hasBeenLaunchBack)
        {
            if (!bulletLauchedBack)
            {
                bulletLauchedBack = true;
                SoundManager.Instance.PlaySfx(bulletParry, bulletParryVolume);
            }
        }
        else
        {
            bulletLauchedBack = false;
        }
    }
}
