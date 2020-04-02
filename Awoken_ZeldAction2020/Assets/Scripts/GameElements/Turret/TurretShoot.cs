using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    #region Variables
    [Header("Turret Settings")]
    [SerializeField] float timeBtwShots = 0;

    [Header("Bullet initiate")]
    public GameObject Bullet;
    [SerializeField] private float bulletSpeed = 0;

    public Transform shootPoint;

    private Vector2 direction;

    private bool turretIsShooting;

    #endregion

    private void Update()
    {
        if (!turretIsShooting)                                                   //if turret isn't shooting
        {
            Shoot();
            StartCoroutine(CooldownShoot());
        }
    }

    void Shoot()
    {
        direction = Vector2.left;

        GameObject bulletInstance = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = direction;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = bulletSpeed;
    }

    IEnumerator CooldownShoot()                                                                     //Time between shoots
    {
        turretIsShooting = true;
        yield return new WaitForSeconds(timeBtwShots);
        turretIsShooting = false;
    }
}
