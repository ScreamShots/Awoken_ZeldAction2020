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
    private Vector2 perpendicularAimDirection;  //Needed to deffine postion of the ray for shield security check

    private Rigidbody2D bulletRgb;              
    private CircleCollider2D bulletCollider;    //Needed to deffine postion of the ray for shield security check

    private int layerMaskPlyShield = 0;         //Layer mask to isolate the playerShield layer
    private bool blockDetected = false;         //true if the ray detected an activated shield
    #endregion

    private void Start()
    {
        bulletRgb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<CircleCollider2D>();       

        Destroy(gameObject, TimeBeforeDestroy);                 //Destroy bullet after x time : security

        player = PlayerManager.Instance.gameObject;
        blockHandle = GetComponent<BlockHandler>();

        perpendicularAimDirection = Vector2.Perpendicular(aimDirection.normalized);
        perpendicularAimDirection.Normalize();
        layerMaskPlyShield = LayerMask.GetMask("PlayerShield");
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
        RayCastSecurity();
        bulletRgb.velocity = aimDirection * bulletSpeed * Time.fixedDeltaTime;      
    }

    private void OnTriggerEnter2D(Collider2D other)             //When collide with player destroy bullet
    {
        if (other.CompareTag("HitBox") && other.gameObject.transform.root.CompareTag("Player"))
        {
            if (blockDetected)                                                                                      //if we detected a shield before and that the bullet is collisionning with the hitbox of the player
            {                                                                                                       // we run the block behaviour instead
                OnBlocked();
            }
            else
            {
                player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
                Destroy(gameObject);
            }            
        }
    }

    void OnBlocked()                        //What happen when the projectile is blocked
    {
        PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLoseOnBlock);        //cause player lost stamina
        Destroy(gameObject);
    }

    void RayCastSecurity()
    {
        /*1-*/
        RaycastHit2D centerHit = Physics2D.Raycast(transform.localPosition + (Vector3)aimDirection.normalized * bulletCollider.radius + (Vector3)(bulletRgb.velocity * Time.fixedDeltaTime), bulletRgb.velocity, (bulletRgb.velocity * Time.fixedDeltaTime).magnitude, layerMaskPlyShield);
        /*2-*/
        RaycastHit2D upHit = Physics2D.Raycast(transform.localPosition + (Vector3)perpendicularAimDirection * bulletCollider.radius + (Vector3)(bulletRgb.velocity * Time.fixedDeltaTime), bulletRgb.velocity, (bulletRgb.velocity * Time.fixedDeltaTime).magnitude, layerMaskPlyShield);
        /*3-*/
        RaycastHit2D downHit = Physics2D.Raycast(transform.localPosition - (Vector3)perpendicularAimDirection * bulletCollider.radius + (Vector3)(bulletRgb.velocity * Time.fixedDeltaTime), bulletRgb.velocity, (bulletRgb.velocity * Time.fixedDeltaTime).magnitude, layerMaskPlyShield);

        /// <summary>
        /// 1 -Shoat a ray in the aim directioon with a lenght equal to the distance that the bullet travel each frame
        /// 2- same as up but the ray is not shot from the center but from the top of the bullet to cover more space
        /// 3- same as up but the ray is not shot from the center but from the bottom of the bullet to cover more space
        /// 
        /// All these ray check for collision only with elements that are on the PlayerShield layer wich is reserved to shieldHitZone of the player
        /// </summary>

        if (centerHit.collider != null)
        {
            if (centerHit.collider.gameObject.GetComponent<ShieldHitZone>().isActivated)                            //start
            {
                blockDetected = true;
            }
        }
        else if (upHit.collider != null)
        {
            if (upHit.collider.gameObject.GetComponent<ShieldHitZone>().isActivated)                                //Depending on wich ray detected a shield, we test if that shield is activated or not, if he is we active a boolean
            {
                blockDetected = true;
            }
        }
        else if (downHit.collider != null)
        {
            if (downHit.collider.gameObject.GetComponent<ShieldHitZone>().isActivated)
            {
                blockDetected = true;
            }
        }                                                                                                           //end
    }
}
