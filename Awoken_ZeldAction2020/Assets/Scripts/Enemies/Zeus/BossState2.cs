﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent / Update by Antoine
/// All the pattern in the boss state 2 fight
/// </summary>

public class BossState2 : MonoBehaviour
{
    #region Pattern 1

    [HideInInspector] public bool pattern1IsRunning;

    private GameObject player;
    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    [Space]
    [Header("Pattern1 - Shoot 3 bullets")]

    [SerializeField]
    private bool bossIsShooting;
    [SerializeField]
    private bool shoot3bullets;
    
    [SerializeField]
    private Transform shootPoint = null;
    [SerializeField]
    private GameObject bossStrike = null;

    [Header("Stats")]

    [SerializeField]
    private float timeBeforeShoot = 0;
    [SerializeField]
    private float timeBtwStrike = 0;
    [SerializeField]
    private float strikeSpeed = 0;
    [SerializeField]
    private float quickShoot = 0;
    private Vector2 direction;
    private Vector2 directionBullet;
    private float playerDistance;

    [HideInInspector] public bool animShoot;                            //anim of shooting
    #endregion

    #region Pattern 2

    [HideInInspector] public bool pattern2IsRunning;

    [Space]
    [Header("Pattern2 - Instantiate wall")]

    [SerializeField]
    private GameObject protectionWall = null;
    [SerializeField]
    private Transform wallTransform = null;

    [Header("Stats")]

    [SerializeField]
    private float knockbackIntensity = 0;
    [SerializeField]
    private float wallspawnTime = 0;
    [SerializeField]
    private float destroyWallTIme = 0;
    [SerializeField]
    private bool shoot1bullets;

    [HideInInspector] public bool animWall;                             //anim of instantiate wall
    [HideInInspector] public bool isPunching;                           //anim of Zeus punching player
    #endregion

    #region Pattern 3

    [HideInInspector] public bool pattern3IsRunning;

    [Space][Header("Pattern3 - Lightning")]

    public Transform throneArena;
    public GameObject Lightning;
    private GameObject newLightning;

    [Header("Stats")]

    public float timeBtwLightning;
    private float timeLeft;
    [HideInInspector] public bool animThunder;                         //anim of lightning
    #endregion

    void Start()
    {
        player = PlayerManager.Instance.gameObject;

        timeLeft = timeBtwLightning;
    }

    void Update()
    {
        AttackState2();
        Move();
        CheckPatternRunning();
    }

    void CheckPatternRunning()
    {
        if (shoot3bullets)
        {
            pattern1IsRunning = true;
        }
        else
        {
            pattern1IsRunning = false;
        }

        if (shoot1bullets)
        {
            pattern2IsRunning = true;
        }
        else
        {
            pattern2IsRunning = false;
        }

        if (newLightning != null)
        {
            pattern3IsRunning = true;
        }
        else
        {
            pattern3IsRunning = false;
        }
    }

    void AttackState2()
    {
        if (BossManager.Instance.s2_Pattern1)
        {
            animThunder = false;
            Pattern1();
        }
        else if (BossManager.Instance.s2_Pattern2)
        {
            Pattern2();
        }
        else if (BossManager.Instance.s2_Pattern3)
        {
            animWall = false;
            Pattern3();
        }
    }

    void Move()
    {
        if (BossManager.Instance.s2_Pattern1)
        {
            transform.position = throneArena.position;
        }
        else if (BossManager.Instance.s2_Pattern2)
        {
            transform.position = throneArena.position;
        }
        else if (BossManager.Instance.s2_Pattern3)
        {
            transform.position = throneArena.position;
        }
    }

    void SetDirectionAttack()
    {
        direction = (player.transform.position - transform.position).normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                watchDirection = Direction.right;
            }
            else if (direction.x < 0)
            {
                watchDirection = Direction.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                watchDirection = Direction.up;
            }
            else if (direction.y < 0)
            {
                watchDirection = Direction.down;
            }
        }
    }

    void SetWallDirection()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;

        wallTransform.localPosition = new Vector3(0, - playerDistance / 2, 0);
    }

    void KickPlayerOutZone()
    {
        if (throneArena.GetComponent<ZeusTeleportZone>().playerInZone)
        {
            if (!isPunching)
            {
                isPunching = true;
                StartCoroutine(KickPlayer());
            }
        }
    }

    void BossAttackStrike()
    {
        directionBullet = (player.transform.position - shootPoint.transform.position).normalized;
        float rotationZ = Mathf.Atan2(directionBullet.y, directionBullet.x) * Mathf.Rad2Deg;

        GameObject bulletInstance = Instantiate(bossStrike, shootPoint.position, Quaternion.Euler(0, 0, rotationZ + 90f));
        bulletInstance.GetComponent<BulletComportement>().aimDirection = directionBullet;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = strikeSpeed;
    }

    #region Pattern1
    void Pattern1()
    {
        if (!bossIsShooting)
        {
            shoot3bullets = true;
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot1());
        }
    }

    IEnumerator PrepareForShoot1()
    {

        SetDirectionAttack();
        StartCoroutine(StartAnimationShoot());

        yield return new WaitForSeconds(timeBeforeShoot);

        StartCoroutine(Pattern1Shoot());
    }

    IEnumerator Pattern1Shoot()
    {
        BossAttackStrike();
        animShoot = false;
        StartCoroutine(StartAnimationShootBtwStrike());

        yield return new WaitForSeconds(quickShoot);
        BossAttackStrike();
        StartCoroutine(StartAnimationShootBtwStrike());
        animShoot = false;

        yield return new WaitForSeconds(quickShoot);
        BossAttackStrike();
        animShoot = false;

        yield return new WaitForSeconds(timeBtwStrike - 0.1f);
        shoot3bullets = false;

        yield return new WaitForSeconds(0.1f);
        bossIsShooting = false;
    }

    IEnumerator StartAnimationShoot()
    {
        yield return new WaitForSeconds(timeBeforeShoot - 0.4f);
        animShoot = true;
    }

    IEnumerator StartAnimationShootBtwStrike()
    {
        yield return new WaitForSeconds(quickShoot - 0.4f);
        animShoot = true;
    }
    #endregion

    #region Pattern2
    void Pattern2()
    {
        if (!bossIsShooting)
        {
            shoot1bullets = true;
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot2());
        }
    }

    IEnumerator PrepareForShoot2()
    {
        KickPlayerOutZone();
        SetDirectionAttack();
        StartCoroutine(StartAnimationShoot());

        yield return new WaitForSeconds(timeBeforeShoot);

        StartCoroutine(Pattern2Shoot());
    }

    IEnumerator Pattern2Shoot()
    {
        animShoot = false;
        BossAttackStrike();
        StartCoroutine(StartAnimationWall());

        yield return new WaitForSeconds(wallspawnTime);
        StartCoroutine(ProtectionWall());

        yield return new WaitForSeconds(timeBtwStrike -0.1f);
        shoot1bullets = false;

        yield return new WaitForSeconds(0.1f);
        bossIsShooting = false;
    }

    IEnumerator ProtectionWall()
    {
        animWall = false;
        SetWallDirection();
        GameObject wallInstance = Instantiate(protectionWall, wallTransform.position, wallTransform.rotation);

        yield return new WaitForSeconds(destroyWallTIme);
        Destroy(wallInstance);
    }

    IEnumerator StartAnimationWall()
    {
        yield return new WaitForSeconds(wallspawnTime - 0.2f);
        animWall = true;
    }

    IEnumerator KickPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.playerRgb.AddForce(new Vector2(0, -20) * knockbackIntensity);
        isPunching = false;
    }
    #endregion

    #region Pattern3
    void Pattern3()
    {
        timeLeft -= Time.deltaTime;
        animThunder = false;

        if (timeLeft <= 0)
        {
            animThunder = true;
            timeLeft += timeBtwLightning;
            GameObject lightningInstance = Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
            newLightning = lightningInstance;
        }
    }
    #endregion
}
