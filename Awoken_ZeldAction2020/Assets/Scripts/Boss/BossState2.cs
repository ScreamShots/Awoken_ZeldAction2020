using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// All the pattern in the boss state 2 fight
/// </summary>
public class BossState2 : MonoBehaviour
{
    private GameObject player;

    #region State2 pattenr1 and 2

    [SerializeField]
    private bool bossIsShooting;
    [SerializeField]
    private bool bossCanAttack;
    private bool willShoot;
    [SerializeField]
    private float timeBeforeShoot;
    [SerializeField]
    private float timeBtwStrike;
    [SerializeField]
    private float strikeSpeed;
    [SerializeField]
    private float quickShoot;
    private Vector2 direction;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject bossStrike;
    [SerializeField]
    private GameObject protectionWall;
    [SerializeField]
    private Transform wallTransform;
    [SerializeField]
    private float wallspawnTime;
    [SerializeField]
    private float destroyWallTIme;

    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    #endregion

    #region pattern3
    public float timeBtwLightning;
    private float timeLeft;
    public GameObject Lightning;
    #endregion

    void Start()
    {
        player = PlayerManager.Instance.gameObject;
        timeLeft = timeBtwLightning;
    }

    void Update()
    {
        //Pattern1();
        //Pattern2();
        //Pattern3();
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

    IEnumerator ProtectionWall()
    {
        direction = (player.transform.position - transform.position).normalized;

        GameObject wallInstance = Instantiate(protectionWall, wallTransform.position, wallTransform.rotation);

        yield return new WaitForSeconds(destroyWallTIme);

        Destroy(wallInstance);
    }

    #region Pattern1
    void Pattern1()
    {
        if (bossCanAttack && !bossIsShooting)
        {
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot1());
        }
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

    IEnumerator PrepareForShoot1()
    {
        willShoot = true;
        SetDirectionAttack();

        yield return new WaitForSeconds(timeBeforeShoot);
        willShoot = false;
        StartCoroutine(Pattern1Shoot());
    }
    #endregion

    #region Pattern2
    void Pattern2()
    {
        if (bossCanAttack && !bossIsShooting)
        {
            bossIsShooting = true;
            StartCoroutine(PrepareForShoot2());
        }
    }

    IEnumerator Pattern2Shoot()
    {
        BossAttackStrike();
        yield return new WaitForSeconds(wallspawnTime);
        StartCoroutine(ProtectionWall());

        yield return new WaitForSeconds(timeBtwStrike);
        bossIsShooting = false;
    }

    IEnumerator PrepareForShoot2()
    {
        willShoot = true;
        SetDirectionAttack();

        yield return new WaitForSeconds(timeBeforeShoot);
        willShoot = false;
        StartCoroutine(Pattern2Shoot());
    }
    #endregion


    void Pattern3()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft += timeBtwLightning;
            Instantiate(Lightning, player.transform.position, Lightning.transform.rotation);
        }
    }
}
