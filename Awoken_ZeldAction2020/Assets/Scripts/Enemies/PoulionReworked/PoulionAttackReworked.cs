using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulionAttackReworked : MonoBehaviour
{
    PoulionMovementReworked poulionMoveScript;
    EnemyHealthSystem poulionHealthScript;
    Rigidbody2D poulionRgb;
    GameObject player;
    GameObject overlappedShield;

    Vector2 chargeDirection;

    [SerializeField] [Min(0)]
    float prepareChargeTime = 0;
    [SerializeField] [Min(0)]
    float chargeSpeed = 0;
    [SerializeField] [Min(0)]
    float chargeDistance = 0;
    float chargeTimer;
    [SerializeField] [Min(0)]
    float stunTime = 0;
    [SerializeField] [Min(0)]
    float dmg = 0;
    [SerializeField] [Min(0)]
    float staminaLost = 0;

    bool isAttacking;
    [HideInInspector]
    public bool isCharging;
    [HideInInspector]
    public bool isStun;
    bool isOverlappingPlayer;
    bool isOverlappingShield;
    [HideInInspector]
    public bool isPreparingCharge;


    private void Start()
    {
        poulionMoveScript = GetComponent<PoulionMovementReworked>();
        poulionRgb = GetComponent<Rigidbody2D>();
        poulionHealthScript = GetComponent<EnemyHealthSystem>();
        player = PlayerManager.Instance.gameObject;
    }

    private void Update()
    {
        if (poulionMoveScript.playerInAttackRange && !isAttacking)
        {
            poulionMoveScript.canMove = false;
            isAttacking = true;
            StartCoroutine(ChargePreparation());
        }

        if (isCharging)
        {
            if(chargeTimer > 0)
            {
                chargeTimer -= Time.deltaTime;
            }
            else
            {
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;
                isStun = true;
                StartCoroutine(Stun());
            }
        }
    }
    private void FixedUpdate()
    {
        if (isCharging)
        {
            if(isOverlappingShield == true)
            {
                if (overlappedShield.GetComponent<ShieldHitZone>().isActivated)
                {
                    isCharging = false;
                    poulionHealthScript.canTakeDmg = true;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun());
                }                   
            }
            else if (isOverlappingPlayer == true && !isOverlappingShield)
            {
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;
                isStun = true;
                player.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
                StartCoroutine(Stun());
            }
            else
            {
                poulionRgb.velocity = chargeDirection * chargeSpeed * Time.fixedDeltaTime;
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;
        
        if (isCharging)
        {
            if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
            {
                isOverlappingPlayer = true;
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;
                isStun = true;
                StartCoroutine(Stun());
                detectedElement.transform.root.gameObject.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
            }
            if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")
            {
                overlappedShield = detectedElement;
                isOverlappingShield = true;
                if (detectedElement.GetComponent<ShieldHitZone>().isActivated)
                {                    
                    isCharging = false;
                    poulionHealthScript.canTakeDmg = true;
                    isStun = true;
                    StartCoroutine(Stun());
                    detectedElement.transform.root.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                }               
            }
            else if (detectedElement.tag == "Wall")
            {
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;
                isStun = true;
                StartCoroutine(Stun());
            }
        }
        else
        {
            if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
            {
                isOverlappingPlayer = true;                
            }
            if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")
            {
                overlappedShield = detectedElement;
                isOverlappingShield = true;                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            isOverlappingPlayer = false;            
        }
        if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            isOverlappingShield = false;
        }
    }

    IEnumerator Stun()
    {
        poulionRgb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        isAttacking = false;
        poulionMoveScript.canMove = true;
        isStun = false;
    }

    IEnumerator ChargePreparation()
    {
        isPreparingCharge = true;
        yield return new WaitForSeconds(prepareChargeTime);
        isPreparingCharge = false;
        chargeDirection = player.transform.position - transform.position;
        chargeDirection.Normalize();
        chargeTimer = chargeDistance / (chargeSpeed * Time.fixedDeltaTime);
        isCharging = true;
        poulionHealthScript.canTakeDmg = false;
    }
}
