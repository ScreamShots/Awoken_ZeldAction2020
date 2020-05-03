using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons des tourelles
/// </summary>

public class TurretSound : MonoBehaviour
{
    private PrefabSoundManager turretManager;
    private TurretShoot shoot;

    private bool l_turretIsShooting;
    private bool l_turretIsBroken;

    // Start is called before the first frame update
    void Start()
    {
        turretManager = GetComponent<PrefabSoundManager>();
        shoot = GetComponentInParent<TurretShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (l_turretIsBroken != shoot.turretIsBroken)
        {
            if (shoot.turretIsBroken == true)
            {
                Shoot();
            }
            l_turretIsBroken = shoot.turretIsBroken;
        }

        if (l_turretIsShooting != shoot.turretIsShooting)
        {
            if (shoot.turretIsShooting == true)
            {
                Shoot();
            }
            l_turretIsShooting = shoot.turretIsShooting;
        }
    }

    void Death()
    {
        turretManager.Play("TurretDeath");
    }

    void Shoot()
    {
        turretManager.Play("TurretShoot");
    }
}
