using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather the comportement of the bullet
/// </summary>

public class BulletComportement : MonoBehaviour
{
    #region Variables

    [Header("Requiered Components")]
    [SerializeField] GameObject bulletRender = null;

    [Header("Bullet Stats")]
    [Min(0)]
    [SerializeField] private float dmg = 0;

    public float bulletSpeed;

    [SerializeField] private float staminaLoseOnBlock = 0;
    private BlockHandler blockHandle;
    [SerializeField]
    [Min(0)]
    private float launchBackAccelerationRatio = 1;

    [Space]
    public float TimeBeforeDestroy;

    private GameObject player;

    [HideInInspector]
    public Vector2 aimDirection;
    private Vector2 l_aimDirection;
    private Vector2 perpendicularAimDirection;  //Needed to deffine postion of the ray for shield security check

    [HideInInspector]
    public Rigidbody2D bulletRgb;
    private bool l_hasBeenLaunchBack = false;
    [HideInInspector]
    public bool isParied;
    #endregion

    private void Awake()
    {
        EnemyManager.Instance.allProjectile.Add(gameObject);
    }

    private void Start()
    {
        bulletRgb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, TimeBeforeDestroy);                 //Destroy bullet after x time : security

        player = PlayerManager.Instance.gameObject;
        blockHandle = GetComponent<BlockHandler>();

        perpendicularAimDirection = Vector2.Perpendicular(aimDirection.normalized);
        perpendicularAimDirection.Normalize();
    }

    private void Update()
    {
        if (l_aimDirection != aimDirection)
        {
            bulletRender.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
            l_aimDirection = aimDirection;
        }

        if (blockHandle.hasBeenLaunchBack && blockHandle.hasBeenLaunchBack != l_hasBeenLaunchBack)       //when launch back through player pary (execute only once)
        {
            bulletSpeed *= launchBackAccelerationRatio;                         //augment the speed oif the bullet      
            gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");       //change the layer of the bullet from enemyProjectile (for player interraction) to playerProjectile (for enemy intrraction).
            l_hasBeenLaunchBack = blockHandle.hasBeenLaunchBack;                //variable l_hasBeenLaunchBack is used to run through that part of the script only once
        }

        if (!blockHandle.isParied && !blockHandle.hasBeenLaunchBack)
        {
            blockHandle.projectileDirection = aimDirection;
        }
        else if (blockHandle.isParied && !blockHandle.hasBeenLaunchBack)
        {
            aimDirection = blockHandle.projectileDirection;
        }

        if (blockHandle.isBlocked && !blockHandle.isParied)          //Testin if the projectile has been blocked
        {
            OnBlocked();                    //Apply behaviour design for the projectile when it's blocked
        }
    }

    private void FixedUpdate()
    {
        if (!isParied)
        {
            bulletRgb.velocity = aimDirection * bulletSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)             //When collide with player destroy bullet
    {
        if (!blockHandle.hasBeenLaunchBack)             //if the projectile has not been laucnh back through player parry
        {
            if (other.CompareTag("HitBox") && other.gameObject.transform.root.CompareTag("Player"))
            {
                player.GetComponent<PlayerHealthSystem>().TakeDmg(dmg, transform.position);
                Destroy(gameObject);
            }
        }
        else                                //if the projectile has been launch back through player pary
        {
            if (other.CompareTag("HitBox") && other.gameObject.transform.root.CompareTag("Enemy"))
            {
                if (other.gameObject.transform.root.GetComponent<EnemyHealthSystem>() != null)
                {
                    other.gameObject.transform.root.GetComponent<EnemyHealthSystem>().TakeDmg(dmg);
                }
                else
                {
                    other.gameObject.transform.root.GetComponent<GameElementsHealthSystem>().TakeDmg(dmg);
                }

                Destroy(gameObject);
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Default") || other.gameObject.layer == LayerMask.NameToLayer("ObjectToMove"))
        {
            Destroy(gameObject);
        }

    }

    void OnBlocked()                        //What happen when the projectile is blocked
    {
        PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLoseOnBlock);        //cause player lost stamina
        Destroy(gameObject);
    }
}