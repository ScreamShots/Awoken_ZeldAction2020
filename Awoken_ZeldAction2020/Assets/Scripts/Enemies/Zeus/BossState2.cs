using System.Collections;
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
    private float playerDistance;
    #endregion

    #region Pattern 2

    [HideInInspector] public bool pattern2IsRunning;

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
    [SerializeField]
    private bool shoot1bullets;
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
            Pattern3();
        }
    }

    void Move()
    {
        if (BossManager.Instance.s2_Pattern1)
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
        direction = (player.transform.position - transform.position).normalized;

        playerDistance = (transform.position - player.transform.position).magnitude;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                wallTransform.localPosition = new Vector3(playerDistance / 2, 0);
                wallTransform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (direction.x < 0)
            {
                wallTransform.localPosition = new Vector3(-playerDistance / 2, 0);
                wallTransform.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                wallTransform.localPosition = new Vector3(0, playerDistance / 2);
                wallTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.y < 0)
            {
                wallTransform.localPosition = new Vector3(0, -playerDistance / 2);
                wallTransform.localRotation = Quaternion.Euler(0, 0, 0);
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

    void RandomPositionTP()
    {
        float randomPosition;
        randomPosition = Random.Range(0, 3);

        if (randomPosition == 0)
        {
            if (!throneArena.GetComponent<ZeusTeleportZone>().playerInZone)
            {
                transform.position = throneArena.position;
            }
            else
            {
                randomPosition = Random.Range(0, 3);
            }
            
        }
        else if (randomPosition == 1)
        {
            if (!leftArena.GetComponent<ZeusTeleportZone>().playerInZone)
            {
                transform.position = leftArena.position;
            }
            else
            {
                randomPosition = Random.Range(0, 3);
            }
        }
        else if (randomPosition == 2)
        {
            if (!rightArena.GetComponent<ZeusTeleportZone>().playerInZone)
            {
                transform.position = rightArena.position;
            }
            else
            {
                randomPosition = Random.Range(0, 3);
            }
        }
        else if (randomPosition == 3)
        {
            if (!downArena.GetComponent<ZeusTeleportZone>().playerInZone)
            {
                transform.position = downArena.position;
            }
            else
            {
                randomPosition = Random.Range(0, 3);
            }
        }
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

        yield return new WaitForSeconds(timeBtwStrike - 0.1f);
        shoot3bullets = false;

        yield return new WaitForSeconds(0.1f);
        bossIsShooting = false;
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
            RandomPositionTP();
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

        yield return new WaitForSeconds(timeBtwStrike -0.1f);
        shoot1bullets = false;

        yield return new WaitForSeconds(0.1f);
        bossIsShooting = false;
    }

    IEnumerator ProtectionWall()
    {
        SetWallDirection();
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
            GameObject lightningInstance = Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
            newLightning = lightningInstance;
        }
    }
    #endregion
}
