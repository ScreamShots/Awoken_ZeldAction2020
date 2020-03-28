using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather attack of the archer and feedback
/// </summary>

public class ArcherAttack : MonoBehaviour
{
    #region Variables
    private GameObject player;

    private Rigidbody2D rb;

    [Header("Attack Settings")]
    public float timeBeforeShoot;

    [SerializeField] float timeBtwShots = 0;

    [Header("Bullet initiate")]
    public GameObject archerBullet;
    [SerializeField] private float bulletSpeed = 0;

    public Transform shootPoint;

    private bool archerIsShooting;

    private Vector2 direction;

    [HideInInspector]
    public bool archerIsAttacking;

    [HideInInspector]
    public bool archerCanAttack;

    [HideInInspector]
    public bool animationAttack;

    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;
    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (archerCanAttack && !archerIsShooting)                                                   //if player is in attack zone and archer isn't shooting
        {
            StartCoroutine(PrepareToShoot());
            StartCoroutine(StartAnimation());
            archerIsShooting = true;
            archerIsAttacking = true;
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
            else
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
            else
            {
                watchDirection = Direction.down;
            }
        }
    }

    void ArcherShoot()
    {
        direction = (player.transform.position - transform.position).normalized;            //Calculate direction between archer && player

        GameObject bulletInstance = Instantiate(archerBullet, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = direction;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = bulletSpeed;
    }

    IEnumerator CooldownShoot()                                                                     //Time between shoots
    {
        yield return new WaitForSeconds(timeBtwShots);
        archerIsShooting = false;
    }

    IEnumerator PrepareToShoot()                                                                    //Time before attack : time of the animation
    {
        SetDirectionAttack();
        yield return new WaitForSeconds(timeBeforeShoot);
        ArcherShoot();
        archerIsAttacking = false;
        animationAttack = false;
        StartCoroutine(CooldownShoot());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(timeBeforeShoot - 0.5f);
        animationAttack = true;
    }
}
