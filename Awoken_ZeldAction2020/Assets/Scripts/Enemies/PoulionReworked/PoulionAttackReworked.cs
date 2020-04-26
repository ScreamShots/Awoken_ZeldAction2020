using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher based on Antoine Leroux work
/// Class handling poulion attack through 3 part (preparation , charge and stun)
/// also handle player hit and block
/// </summary>

[RequireComponent(typeof(PoulionMovementReworked))]
public class PoulionAttackReworked : MonoBehaviour
{
    #region HideInInspector Var Statement

    //Requiered object and component for references

    PoulionMovementReworked poulionMoveScript;
    EnemyHealthSystem poulionHealthScript;
    Rigidbody2D poulionRgb;                                 
    GameObject player;
    GameObject overlappedShield;                    //use for security test on special case (player block)

    //charge informations

    Vector2 chargeDirection;                //direction of the charge
    float chargeTimer;                      //duation time of the charge

    //Algorythm boolean for intern logic and animator

    bool isAttacking;                       //is the enemy on and attack (prepa or charge or stun) 
    [HideInInspector]
    public bool isPreparingCharge;          //is the enemy preparing for charge
    [HideInInspector]
    public bool isCharging;                 //is the enemy currently charging
    [HideInInspector]
    public bool isStun;                     //is the enemy stun

    //Some security test to handle case where the enemy is already into the player collider or shield collider when he start charging

    bool isOverlappingPlayer;               //if the enemy is already on player hitbox
    bool isOverlappingShield;               //if the enemy is already on player shield


    #endregion

    #region Serialize Var Statement

    //Part1 - Preparation
    [Header("Part1 - Preparation")]

    [SerializeField] [Min(0)]
    float prepareChargeTime = 0;

    [Header("Part2 - Charge")]

    [SerializeField] [Min(0)]
    float chargeSpeed = 0;
    [SerializeField] [Min(0)]
    float chargeDistance = 0;

    [Header("Part3 - Stun")]

    [SerializeField] [Min(0)]
    float stunTime = 0;

    [Header("Stats")]

    [SerializeField] [Min(0)]
    float dmg = 0;
    [SerializeField] [Min(0)]
    float staminaLost = 0;

    #endregion
       
    private void Start()
    {
        poulionMoveScript = GetComponent<PoulionMovementReworked>();
        poulionRgb = GetComponent<Rigidbody2D>();
        poulionHealthScript = GetComponent<EnemyHealthSystem>();
        player = PlayerManager.Instance.gameObject;
    }

    private void Update()
    {
        if (poulionMoveScript.playerInAttackRange && !isAttacking)          //if the movement class says the player is in range to attack and we are not already attacking
        {
            poulionMoveScript.canMove = false;                              //override movement management (disable movement from the move class)
            isAttacking = true;
            StartCoroutine(ChargePreparation());                            //Launch the attack with part 1 -> preparation
        }

        if (isCharging)                                                     //if we are currently charging
        {
            if(chargeTimer > 0)                                             //while the charge duration doesn't come to end just decrease charge timer
            {
                chargeTimer -= Time.deltaTime;
            }
            else                                                            //when the timer come to and end 
            {
                isCharging = false;                                         //change some bool according to the enemy conception
                poulionHealthScript.canTakeDmg = true;
                isStun = true;
                StartCoroutine(Stun());                                     //launch part3 -> stun
            }
                                                                            //The timer can be cut if the is charge bool is stoped before it falls to  0
        }

    }
    private void FixedUpdate()
    {
        if (isCharging)                                                                         //What happen if the enemy is in part2 - charge
        {
            Charge();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;                  //store the gameObject we collid with for easier reference
        
        if (isCharging)                                                     //if we are currently charging
        {
            if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")       //if the collided element is a player hitbox
            {
                isOverlappingPlayer = true;     //saying that we are overlapping a player hitbox(so if the enemy dont go out before the next attack if we trigger security check of charge method)
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;                                                              //deal dmg and launch part3- stun
                isStun = true;
                StartCoroutine(Stun());
                detectedElement.transform.root.gameObject.GetComponent<PlayerHealthSystem>().TakeDmg(dmg, transform.position);
            }
            if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")   //if the collided element is a player shield zone
            {
                overlappedShield = detectedElement;         //storing the actual shield zone for security test of charge method                                
                isOverlappingShield = true;                 //saying that we are overlapping a player shield zone(so if the enemy dont go out before the next attack if we trigger security check of charge method)
                if (detectedElement.GetComponent<ShieldHitZone>().isActivated)                          //if the shield zone is activated
                {                    
                    isCharging = false;
                    poulionHealthScript.canTakeDmg = true;
                    isStun = true;                                                                      //Apply Block behaviour and launch part3- stun
                    StartCoroutine(Stun());
                    detectedElement.transform.root.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                }               
            }
            else if (detectedElement.tag == "Wall")                                 //if the collided element is a obstacle
            {
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;                              //launch part 3- stun
                isStun = true;
                StartCoroutine(Stun());
            }
        }
        else
        {
            if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
            {
                isOverlappingPlayer = true;                
            }                                                                                                               //instruction for check security if the enemy overlap a shield or a hitbox and is not charging yet
            if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")           //Purpose and functionnement still the same as upward
            {
                overlappedShield = detectedElement;
                isOverlappingShield = true;                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;                  //storing the gameobject we are colliding with for easier references

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")           //if we are leaving the player hitbox disable security check
        {
            isOverlappingPlayer = false;            
        }
        if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")       //if we are leaving the player shield zone disable security check
        {
            isOverlappingShield = false;
        }
    }

    void Charge()                               //method that handle movement and security check from collision during the charge
    {
        if (isOverlappingShield == true)                                                     //security: if the enemy is overlapping a playershield do security check before the first move frame
        {
            if (overlappedShield.GetComponent<ShieldHitZone>().isActivated)                 //check if this shield is activated or not 
            {
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;
                isStun = true;                                                              //if it is activated we pass to the part3- stun and we apply every consequences to the player (he did block)
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                StartCoroutine(Stun());
            }
            else if (isOverlappingPlayer)                                                   //if it's disactivated we check if the enemy is overlapping the player hitbox
            {
                isCharging = false;
                poulionHealthScript.canTakeDmg = true;
                isStun = true;                                                              //if it's so we infligt damages to the player and launch the part3- stun
                player.GetComponent<PlayerHealthSystem>().TakeDmg(dmg, transform.position);
                StartCoroutine(Stun());
            }
        }
        else if (isOverlappingPlayer == true && !isOverlappingShield)                       //security: if the enemy is overlapping hitbox but no shield
        {
            isCharging = false;
            poulionHealthScript.canTakeDmg = true;
            isStun = true;                                                                  //we infligt damages to the player and launch the part3- stun
            player.GetComponent<PlayerHealthSystem>().TakeDmg(dmg, transform.position);
            StartCoroutine(Stun());
        }
        else
        {
            poulionRgb.velocity = chargeDirection * chargeSpeed * Time.fixedDeltaTime;      //if the poulion if overlapping nothing we juste charge
        }
    }

    IEnumerator Stun()                  //Coroutine that handle stun
    {
        poulionRgb.velocity = Vector2.zero;                     //stop the enemy movement
        yield return new WaitForSeconds(stunTime);              //wait the stun duration
        isAttacking = false;                                    //saying that the attack is finished
        poulionMoveScript.canMove = true;                       //giving movement management back to move class
        isStun = false;                                         //bool for animator
    }

    IEnumerator ChargePreparation()     //Coroutine that handle Charge Preparation
    {
        isPreparingCharge = true;                                   //animator bool
        yield return new WaitForSeconds(prepareChargeTime);         //Wait the preparation duration
        isPreparingCharge = false;                                  //animator bool
        chargeDirection = player.transform.position - transform.position;       //deffine the charge direction from the player position
        chargeDirection.Normalize();
        chargeTimer = chargeDistance / (chargeSpeed * Time.fixedDeltaTime);     //deffine the time duration the charge must be to fit with the distance assigned in the inspector (speed = distance / time <so> time = distance/ speed)
        isCharging = true;                                                      
        poulionHealthScript.canTakeDmg = false;
    }
}
