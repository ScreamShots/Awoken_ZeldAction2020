using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather attacks relative to state 2 Bis of Zeus
/// </summary>

public class BossState2Bis : MonoBehaviour
{
    private GameObject player;
    PlayerHealthSystem playerHealthScript;
    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    #region Pattern 1

    [Space(30)]
    [Header("PATTERN 1 - Shoot 1 bullet")]

    [SerializeField]
    private Transform shootPoint = null;
    [SerializeField]
    private GameObject bossStrike = null;

    [HideInInspector] public bool pattern1IsRunning;
    private bool bossIsShooting;

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

    [Space(30)]
    [Header("PATTERN 1 - Instantiate wall")]

    [SerializeField]
    private GameObject protectionWall = null;
    [SerializeField]
    private Transform wallTransform = null;
    private GameObject wallInstance = null;
    ZeusWallZone zeusWallZoneScript;

    [Header("Stats")]

    [SerializeField]
    private float destroyWallTIme = 0;
    private bool shoot1bullets;

    [HideInInspector] public bool animWall;                             //anim of instantiate wall
    [HideInInspector] public bool isPunching;                           //anim of Zeus punching player
    private bool canInstancieWall;

    [Space(30)]
    [Header("PATTERN 1 - Spawner ")]
    public Transform spawnerPlace;
    public Transform spawnerPlace2;
    public GameObject spawner;
    [HideInInspector] public GameObject spawnerA;
    [HideInInspector] public GameObject spawnerB;
    public float timeBeforePlaceSpawner;
    private float time;

    [Header("Stats")]

    [Space] [SerializeField] private float spawnRadius = 0;
    [SerializeField] private float timeBtwSpawn = 0;
    [SerializeField] private float enemySpawnLimit = 0;

    [Space] public GameObject[] enemiesToSpawn;

    [HideInInspector] public bool spawnerExist;

    [Space(30)]
    [Header("PATTERN 1 - Instantiate Blocs")]
    public GameObject indestructibleBloc;
    [SerializeField] private GameObject spawnCloud = null;

    [HideInInspector] public GameObject bloc1;
    [HideInInspector] public GameObject bloc2;
    [HideInInspector] public GameObject bloc3;

    public Transform bloc1Place;
    public Transform bloc2Place;
    public Transform bloc3Place;

    private bool spawnerIsDisable = false;
    #endregion

    #region Pattern 2

    [Space(30)]
    [Header("PATTERN 2 - Thunderbolt")]

    public Transform throneArena;
    public GameObject Lightning;
    private GameObject newLightning;

    [HideInInspector] public bool pattern2IsRunning;

    [Header("Stats")]

    public float timeBtwLightning;
    private float timeLeft;
    [HideInInspector] public bool animThunder;                         //anim of lightning
    #endregion

    [HideInInspector] public bool ZeusTp;

    void Start()
    {
        player = PlayerManager.Instance.gameObject;

        playerHealthScript = player.GetComponent<PlayerHealthSystem>();
        zeusWallZoneScript = GetComponentInChildren<ZeusWallZone>();
        spawner.GetComponent<EnemySpawner>().spawnRadius = spawnRadius;
        spawner.GetComponent<EnemySpawner>().timeBtwSpawn = timeBtwSpawn;
        spawner.GetComponent<EnemySpawner>().enemySpawnLimit = enemySpawnLimit;
        spawner.GetComponent<EnemySpawner>().enemiesToSpawn = enemiesToSpawn;

        timeLeft = timeBtwLightning;
        time = timeBeforePlaceSpawner;
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

        if (spawnerExist)
        {
            if (!spawnerIsDisable)
            {               
                if (bloc1.GetComponent<ChargableElement>().isDestroyed && bloc2.GetComponent<ChargableElement>().isDestroyed && bloc3.GetComponent<ChargableElement>().isDestroyed)
                {
                    spawnerIsDisable = true;
                    spawnerA.GetComponent<EnemySpawner>().spawnEnable = false;
                    spawnerB.GetComponent<EnemySpawner>().spawnEnable = false;
                }
            }
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

            time -= Time.deltaTime;

            if (time <= 0)
            {
                if (!spawnerExist)
                {
                    spawnerExist = true;
                    SpawnSpawner();                                 //Instantiate a spawner to a position
                    SpawnIndestructibleBloc();
                }
            }
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

    void SpawnSpawner()
    {
        spawnerA = Instantiate(spawner, spawnerPlace.position, spawner.transform.rotation);
        spawnerB = Instantiate(spawner, spawnerPlace2.position, spawner.transform.rotation);
    }

    void SpawnIndestructibleBloc()
    {
        bloc1 = Instantiate(indestructibleBloc, bloc1Place.position, indestructibleBloc.transform.rotation);
        Instantiate(spawnCloud, bloc1Place.position, Quaternion.identity);
        bloc2 = Instantiate(indestructibleBloc, bloc2Place.position, indestructibleBloc.transform.rotation);
        Instantiate(spawnCloud, bloc2Place.position, Quaternion.identity);
        bloc3 = Instantiate(indestructibleBloc, bloc3Place.position, indestructibleBloc.transform.rotation);
        Instantiate(spawnCloud, bloc3Place.position, Quaternion.identity);
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
