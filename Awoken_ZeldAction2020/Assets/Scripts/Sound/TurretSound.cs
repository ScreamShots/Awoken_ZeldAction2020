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
    public AudioSource turretShootSource;
    [Range(0f, 1f)] public float turretShootVolume = 0.5f;

    private GameElementsHealthSystem turretHealthScript;
    private TurretShoot turretShootScript;

    private bool turretIsShooting = false;
    #endregion

    void Start()
    {
        turretShootScript = GetComponentInParent<TurretShoot>();
        turretHealthScript = GetComponentInParent<GameElementsHealthSystem>();
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
                //SoundManager.Instance.PlaySfx(turretShoot, turretShootVolume);
                SoundManager.Instance.PlaySfx3D(turretShootSource, turretShootVolume);
                //turretShootSource.PlayOneShot(turretShootSource.clip);
            }
        }
        else
        {
            turretIsShooting = false;
        }
    }
}
