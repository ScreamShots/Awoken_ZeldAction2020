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
    [Header("Destroy Sound")]
    public AudioClip turretDestroy;
    [Range(0f, 1f)] public float turretDestroyVolume = 0.5f;

    private TurretShoot turretShootScript;

    private bool turretIsShooting = false;
    #endregion

    void Start()
    {
        turretShootScript = GetComponentInParent<TurretShoot>();
    }

    void Update()
    {
        TurretShoot();
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

    void OnDestroy()
    {
        SoundManager.Instance.PlaySfx(turretDestroy, turretDestroyVolume);
    }
}
