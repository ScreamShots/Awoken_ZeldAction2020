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

    #region Pattern1 Statment
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
    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    #endregion

    void Start()
    {
        player = PlayerManager.Instance.gameObject;
    }

    void Update()
    {
        if (bossCanAttack && !bossIsShooting)
        {
            bossIsShooting = true;
            StartCoroutine(PreperaForShoot());
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
    void BossAttackPattern1()
    {
        direction = (player.transform.position - transform.position).normalized;


        GameObject bulletInstance = Instantiate(bossStrike, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = direction;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = strikeSpeed;
    }

    IEnumerator Pattern1()
    {
        BossAttackPattern1();
        
        yield return new WaitForSeconds(quickShoot);
        BossAttackPattern1();
        
        yield return new WaitForSeconds(quickShoot);
        BossAttackPattern1();

        yield return new WaitForSeconds(timeBtwStrike);
        bossIsShooting = false;
    }

    IEnumerator PreperaForShoot()
    {
        willShoot = true;
        SetDirectionAttack();

        yield return new WaitForSeconds(timeBeforeShoot);
        willShoot = false;
        StartCoroutine(Pattern1());
    }
}
