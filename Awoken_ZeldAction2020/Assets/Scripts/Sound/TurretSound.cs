using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TurretSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Shooting Sound")]
    public AudioClip turretShoot;
    [Range(0f, 1f)] public float turretShootVolume = 0.5f;

    [Space]
    [Header("Damage Sound")]
    public AudioClip[] turretDamage;
    [Range(0f, 1f)] public float turretDamageVolume = 0.5f;

    [Space]
    [Header("Destroy Sound")]
    public AudioClip turretDestroy;
    [Range(0f, 1f)] public float turretDestroyVolume = 0.5f;

    private GameElementsHealthSystem turretHealthScript;
    private TurretShoot turretShootScript;

    private bool turretIsShooting = false;
    private bool turretTakingDamage = false;
    #endregion

    void Start()
    {
        turretShootScript = GetComponentInParent<TurretShoot>();
        turretHealthScript = GetComponentInParent<GameElementsHealthSystem>();
    }

    void Update()
    {
        TurretShoot();
        TurretDamage();
    }

    void TurretShoot()
    {
        if (turretShootScript.isShooting)
        {
            if (!turretIsShooting)
            {
                turretIsShooting = true;
                SoundManager.Instance.PlaySfx(turretShoot, turretShootVolume);
            }
        }
        else
        {
            turretIsShooting = false;
        }
    }

    void TurretDamage()
    {
        if (turretHealthScript.takingDmg)
        {
            if (!turretTakingDamage)
            {
                turretTakingDamage = true;
                SoundManager.Instance.PlayRandomSfx(turretDamage, turretDamageVolume);
            }
        }
        else
        {
            turretTakingDamage = false;
        }
    }

    void OnDestroy()
    {
        SoundManager.Instance.PlaySfx(turretDestroy, turretDestroyVolume);
    }
}
