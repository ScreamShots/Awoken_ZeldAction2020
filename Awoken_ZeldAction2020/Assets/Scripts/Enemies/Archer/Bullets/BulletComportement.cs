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

    [Header("Bullet Stats")]
    [Min(0)]
    [SerializeField] private float dmg = 0;

    public float bulletSpeed;

    [SerializeField] private float staminaLoseOnBlock = 0;
    private BlockHandler blockHandle;

    [Space]
    public float TimeBeforeDestroy;
    
    private GameObject player;

    [HideInInspector]
    public Vector2 aimDirection;

    private Rigidbody2D bulletRgb;
    #endregion

    private void Start()
    {
        bulletRgb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, TimeBeforeDestroy);                 //Destroy bullet after x time : security

        player = PlayerManager.Instance.gameObject;
        blockHandle = GetComponent<BlockHandler>();
    }

    private void Update()
    {
        if (blockHandle.isBlocked)          //Testin if the projectile has been blocked
        {
            OnBlocked();                    //Apply behaviour design for the projectile when it's blocked
        }
    }

    private void FixedUpdate()
    {
        bulletRgb.velocity = aimDirection * bulletSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)             //When collide with player destroy bullet
    {
        if (other.CompareTag("HitBox") && other.gameObject.transform.root.CompareTag("Player"))
        {
            player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
            Destroy(gameObject);
        }
    }

    void OnBlocked()                        //What happen when the projectile is blocked
    {
        PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLoseOnBlock);        //cause player lost stamina
        Destroy(gameObject);
    }
}
