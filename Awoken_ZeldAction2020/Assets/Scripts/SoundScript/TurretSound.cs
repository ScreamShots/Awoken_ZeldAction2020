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
    private EnemyHealthSystem turretHealth;
    private TurretShoot shoot;

    private bool l_turretIsShooting;

    // Start is called before the first frame update
    void Start()
    {
        turretManager = GetComponent<PrefabSoundManager>();
        shoot = GetComponentInParent<TurretShoot>();
    }

    // Update is called once per frame
    void Update()
    {

        if (l_turretIsShooting != shoot.isShooting)
        {
            if (shoot.isShooting == true)
            {
                Shoot();
            }
            l_turretIsShooting = shoot.isShooting;
        }
    }

    void Shoot()
    {
        turretManager.Play("TurretShoot");
    }
}
