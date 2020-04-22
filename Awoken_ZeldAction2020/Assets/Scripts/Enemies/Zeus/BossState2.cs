using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent / Update by Antoine
/// All the pattern in the boss state 2 fight
/// </summary>

public class BossState2 : MonoBehaviour
{
    #region Patern 1

    private GameObject player;
    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    [Space][Header("Pattern1 - Shoot 3 bullets")]

    [SerializeField]
    private bool bossIsShooting;
    private bool willShoot;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject bossStrike;

    [Header("Stats")]

    [SerializeField]
    private float timeBeforeShoot;
    [SerializeField]
    private float timeBtwStrike;
    [SerializeField]
    private float strikeSpeed;
    [SerializeField]
    private float quickShoot;
    private Vector2 direction;
    #endregion

    #region Pattern 2

    [Space]
    [Header("Pattern2 - Instantiate wall")]

    public Transform rightArena;
    public Transform leftArena;
    public Transform downArena;
    [SerializeField]
    private GameObject protectionWall;
    [SerializeField]
    private Transform wallTransform;

    [Header("Stats")]

    [SerializeField]
    private float wallspawnTime;
    [SerializeField]
    private float destroyWallTIme;
    #endregion

    #region Pattern 3

    [Space][Header("Pattern3 - Lightning")]

    public Transform throneArena;
    public GameObject Lightning;

    [Header("Stats")]

    public float timeBtwLightning;
    private float timeLeft;
    [HideInInspector] public bool animThunder;
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
            transform.position = rightArena.position;
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

    void BossAttackStrike()
    {
        direction = (player.transform.position - transform.position).normalized;

        GameObject bulletInstance = Instantiate(bossStrike, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = direction;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = strikeSpeed;
    }

    #region Pattern1
    void Pattern1()
    {
        if (!bossIsShooting)
        {
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot1());
        }
    }

    IEnumerator PrepareForShoot1()
    {
        willShoot = true;
        SetDirectionAttack();

        yield return new WaitForSeconds(timeBeforeShoot);
        willShoot = false;
        StartCoroutine(Pattern1Shoot());
    }

    IEnumerator Pattern1Shoot()
    {
        BossAttackStrike();

        yield return new WaitForSeconds(quickShoot);
        BossAttackStrike();

        yield return new WaitForSeconds(quickShoot);
        BossAttackStrike();

        yield return new WaitForSeconds(timeBtwStrike);
        bossIsShooting = false;
    }
    #endregion

    #region Pattern2
    void Pattern2()
    {
        if (!bossIsShooting)
        {
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot2());
        }
    }

    IEnumerator PrepareForShoot2()
    {
        willShoot = true;
        SetDirectionAttack();

        yield return new WaitForSeconds(timeBeforeShoot);
        willShoot = false;
        StartCoroutine(Pattern2Shoot());
    }

    IEnumerator Pattern2Shoot()
    {
        BossAttackStrike();
        yield return new WaitForSeconds(wallspawnTime);
        StartCoroutine(ProtectionWall());

        yield return new WaitForSeconds(timeBtwStrike);
        bossIsShooting = false;
    }

    IEnumerator ProtectionWall()
    {
        direction = (player.transform.position - transform.position).normalized;

        GameObject wallInstance = Instantiate(protectionWall, wallTransform.position, wallTransform.rotation);

        yield return new WaitForSeconds(destroyWallTIme);

        Destroy(wallInstance);
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
            Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
        }
    }
    #endregion
}
