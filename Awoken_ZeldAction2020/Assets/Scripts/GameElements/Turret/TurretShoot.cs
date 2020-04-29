using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    #region Variables
    [Header("Turret Settings")]

    [SerializeField] float timeBtwFirstShot = 1;

    [SerializeField] float timeBtwShots = 0;

    [SerializeField] bool hasAggroZone;

    [Header("Bullet initiate")]
    public GameObject Bullet;
    [SerializeField] private float bulletSpeed = 0;

    public Transform shootPoint;

    private Vector2 direction;

    private float timerPlayerZone;

    [HideInInspector]
    public bool turretIsShooting;

    [HideInInspector]
    public bool inZoneAnim;

    private bool canShootPlayer;                 //for detect player in zone

    [HideInInspector]
    public bool turretIsBroken;                 //for detect mid life of turret --> animator

    TurretDetectionZone detectionZoneScript;
    GameElementsHealthSystem turretHealthScript;
    #endregion

    private void Start()
    {
        detectionZoneScript = GetComponentInChildren<TurretDetectionZone>();
        turretHealthScript = GetComponent<GameElementsHealthSystem>();
        
        timerPlayerZone = timeBtwFirstShot;
    }

    private void Update()
    {
        AggroZone();
        CheckTurretBroken();

        if (hasAggroZone)
        {
            if (!turretIsShooting && canShootPlayer)                                                   //if turret isn't shooting
            {
                Shoot();
                StartCoroutine(CooldownShoot());
            }
        }
        else
        {
            if (!turretIsShooting)                                                                      //if turret isn't shooting
            {
                Shoot();
                StartCoroutine(CooldownShoot());
            }
        }
    }

    void AggroZone()
    {
        if (hasAggroZone)
        {
            if (detectionZoneScript.playerInZone)
            {
                inZoneAnim = true;

                timerPlayerZone += Time.deltaTime;
                if (timerPlayerZone >= timeBtwFirstShot)
                {
                    canShootPlayer = true;
                }
            }
            else
            {
                inZoneAnim = false;
                canShootPlayer = false;

                timerPlayerZone = 0;
            }
        }
        else
        {
            inZoneAnim = true;
        }
    }

    void CheckTurretBroken()
    {
        if (turretHealthScript.currentHp <= turretHealthScript.maxHp / 2)
        {
            turretIsBroken = true;
        }
        else
        {
            turretIsBroken = false;
        }
    }

    void Shoot()
    {

        direction = shootPoint.rotation * Vector2.right;

        GameObject bulletInstance = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = direction;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = bulletSpeed;
    }

    IEnumerator CooldownShoot()                                                                         //Time between shoots
    {
        turretIsShooting = true;
        yield return new WaitForSeconds(timeBtwShots);
        turretIsShooting = false;
    }
}
