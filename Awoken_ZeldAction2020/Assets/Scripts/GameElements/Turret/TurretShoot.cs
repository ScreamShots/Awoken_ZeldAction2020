using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    #region Variables
    [Header("Turret Settings")]

    [SerializeField] bool TurretIsIndestructible = false;

    [Space] [SerializeField] float timeBtwFirstShot = 1;

    [Min(0.8f)]
    [SerializeField] float timeBtwShots = 0;

    [SerializeField] bool hasAggroZone = false;

    [Header("Bullet initiate")]
    public GameObject Bullet;
    [SerializeField] private float bulletSpeed = 0;

    public Transform shootPoint;

    private Vector2 direction;

    private float timerPlayerZone;

    [HideInInspector]
    public bool turretIsShooting;

    [HideInInspector]
    public bool turretFire;                     //for detect when the turret shoot for animation

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

        if (!TurretIsIndestructible)
        {
            CheckTurretBroken();
        }

        if (hasAggroZone)
        {
            if (!turretIsShooting && canShootPlayer)                                                   //if turret isn't shooting
            {
                StartCoroutine(TimeBeforeShoot());
                StartCoroutine(ShootAnimation());
                StartCoroutine(CooldownShoot());
            }
        }
        else
        {
            if (!turretIsShooting)                                                                      //if turret isn't shooting
            {
                StartCoroutine(TimeBeforeShoot());
                StartCoroutine(ShootAnimation());
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

    IEnumerator TimeBeforeShoot()
    {
        yield return new WaitForSeconds(0.6f);
        Shoot();
    }

    IEnumerator CooldownShoot()                                                                         //Time between shoots
    {
        turretIsShooting = true;
        yield return new WaitForSeconds(timeBtwShots);
        turretIsShooting = false;
    }

    IEnumerator ShootAnimation()                                                                         //Lauch shoot animation
    {
        turretFire = true;
        yield return new WaitForSeconds(0.6f);
        turretFire = false;
    }
}
