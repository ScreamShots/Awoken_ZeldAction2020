using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState2Bis : MonoBehaviour
{
    private GameObject player;
    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    [Space]
    [Header("Pattern1 - Shoot 1 bullet")]

    private bool bossIsShooting;

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
    private Vector2 direction;
    private Vector2 directionBullet;
    private float playerDistance;

    [HideInInspector] public bool animShoot;                            //anim of shooting

    #region Pattern 1

    ZeusWallZone zeusWallZoneScript;

    [HideInInspector] public bool pattern1IsRunning;

    [Space]
    [Header("Pattern1 - Instantiate wall")]

    [SerializeField]
    private GameObject protectionWall = null;
    [SerializeField]
    private Transform wallTransform = null;

    [Header("Stats")]

    [SerializeField]
    private float destroyWallTIme = 0;
    private bool shoot1bullets;

    [HideInInspector] public bool animWall;                             //anim of instantiate wall
    [HideInInspector] public bool isPunching;                           //anim of Zeus punching player
    private bool canInstancieWall;
    #endregion

    #region Pattern 2

    [HideInInspector] public bool pattern2IsRunning;

    [Space]
    [Header("Pattern2 - Lightning")]

    public Transform throneArena;
    public GameObject Lightning;
    private GameObject newLightning;

    [Header("Stats")]

    public float timeBtwLightning;
    private float timeLeft;
    [HideInInspector] public bool animThunder;                         //anim of lightning
    #endregion

    private GameObject wallInstance = null;
    PlayerHealthSystem playerHealthScript;

    [HideInInspector] public bool ZeusTp;
    private bool CoroutinePlayOnce;

    void Start()
    {
        player = PlayerManager.Instance.gameObject;
        playerHealthScript = player.GetComponent<PlayerHealthSystem>();

        timeLeft = timeBtwLightning;

        zeusWallZoneScript = GetComponentInChildren<ZeusWallZone>();
    }

    void Update()
    {
        AttackState2();
        Move();
        CheckPatternRunning();

        InstancieWall();
        SetWallDistance();

        if (playerHealthScript.currentHp <= 0)
        {
            Destroy(wallInstance);
        }
    }

    void CheckPatternRunning()
    {
        if (shoot1bullets)
        {
            pattern1IsRunning = true;
        }
        else
        {
            pattern1IsRunning = false;
        }

        if (newLightning != null)
        {
            pattern2IsRunning = true;
        }
        else
        {
            pattern2IsRunning = false;
        }
    }

    void AttackState2()
    {
        if (BossManager.Instance.s2Bis_Pattern1)
        {
            animThunder = false;
            Pattern1();
        }
        else if (BossManager.Instance.s2Bis_Pattern2)
        {
            animWall = false;
            Pattern2();
        }
        else if (BossManager.Instance.currentHp <= 50)
        {
            StopAllCoroutines();
            animShoot = false;
        }
        else
        {
            animThunder = false;
        }
    }

    void Move()
    {
        if (BossManager.Instance.s2Bis_Pattern1)
        {
            transform.position = throneArena.position;
        }
        else if (BossManager.Instance.s2Bis_Pattern2)
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

    void SetWallDistance()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;

        wallTransform.localPosition = new Vector3(0, (-playerDistance / 2), 0);

        if (playerDistance <= 3)
        {
            wallTransform.localPosition = new Vector3(0, -1, 0);
        }
    }

    void InstancieWall()
    {
        if (canInstancieWall)
        {
            if (zeusWallZoneScript.bulletDetection)
            {                                                                                                           //For spawning wall between player and Boss
                zeusWallZoneScript.bulletDetection = false;
                canInstancieWall = false;
                animWall = true;

                StartCoroutine(StartAnimationWall());
                wallInstance = Instantiate(protectionWall, wallTransform.position, wallTransform.rotation);

                Destroy(wallInstance, destroyWallTIme);
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
            shoot1bullets = true;
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot2());
        }
    }

    IEnumerator PrepareForShoot2()
    {
        SetDirectionAttack();
        StartCoroutine(StartAnimationShoot());

        yield return new WaitForSeconds(timeBeforeShoot);

        StartCoroutine(Pattern1Shoot());
    }

    IEnumerator StartAnimationShoot()
    {
        yield return new WaitForSeconds(timeBeforeShoot - 0.4f);
        animShoot = true;
    }

    IEnumerator Pattern1Shoot()
    {
        animShoot = false;
        BossAttackStrike();
        canInstancieWall = true;

        yield return new WaitForSeconds(timeBtwStrike - 0.1f + 0.5f);
        shoot1bullets = false;

        yield return new WaitForSeconds(0.1f);
        bossIsShooting = false;
        canInstancieWall = false;
    }

    IEnumerator StartAnimationWall()
    {
        yield return new WaitForSeconds(0.1f);
        animWall = false;
    }
    #endregion

    #region Pattern2
    void Pattern2()
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
