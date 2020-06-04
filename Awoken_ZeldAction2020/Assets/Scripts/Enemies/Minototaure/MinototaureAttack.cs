using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather attack of the Minototaure
/// </summary>

public class MinototaureAttack : MonoBehaviour
{
    #region Variables
    //global

    MinototaureMovement minototaureMoveScript;
    [HideInInspector] public MinototaureDetectZone minototaureDetectScript;
    EnemyHealthSystem minototaureHealthScript;
    GameObject player;
    private PlayerMovement playerMoveScript;

    //attack information

    [HideInInspector]
    public bool isAttacking;                //is the enemy on and attack state
    [HideInInspector]
    public bool isPreparingAttack;          //is the enemy preparing for charge
    [HideInInspector]
    public bool lauchAttack;                //is the enemy attack
    [HideInInspector]
    public bool isStun;                     //is the enemy stun
    [HideInInspector]
    public bool cooldownAttack;             //is the enemy is on cooldown;
    #endregion

    #region Inspector Settings
    [SerializeField] private GameObject attackZone = null;

    //Part1 - Preparation
    [Header("Part1 - Preparation")]

    [SerializeField]
    [Min(0)]
    float prepareChargeTime = 0;

    [Header("Part2 - Stun")]

    [SerializeField]
    [Min(0)]
    float stunTime = 0;
    [SerializeField]
    [Min(0)]
    float noBlockedStunTime = 0;

    [Header("Stats")]

    [SerializeField]
    [Min(0)]
    float dmg = 0;
    [SerializeField]
    [Min(0)]
    float staminaLost = 0;

    [Header("Part3 - Cooldown")]

    [SerializeField]
    [Min(0)]
    float timeBtwAttack = 0;
    #endregion

    private void Start()
    {
        minototaureMoveScript = GetComponent<MinototaureMovement>();
        minototaureDetectScript = attackZone.GetComponent<MinototaureDetectZone>();
        minototaureHealthScript = GetComponent<EnemyHealthSystem>();
        player = PlayerManager.Instance.gameObject;
        playerMoveScript = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!isStun)
        {
            AttackRotation();
        }

        if (minototaureMoveScript.playerInAttackRange && !isAttacking && !cooldownAttack)          
        {
            minototaureMoveScript.canMove = false;                              
            isAttacking = true;
            StartCoroutine(ChargePreparation());                           
        }
    }

    private void FixedUpdate()
    {
        if (lauchAttack)                                                                        
        {
            Attack();
        }
    }

    void Attack()
    {
        if (minototaureDetectScript.isOverlappingShield == true)                                        //if collide with shield
        {
            if (minototaureDetectScript.overlappedShield.GetComponent<ShieldHitZone>().isActivated)     //if shield is enable      
            {
                // 4 normal directions case
                if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.left && playerMoveScript.watchDirection == PlayerMovement.Direction.right)
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.right && playerMoveScript.watchDirection == PlayerMovement.Direction.left)
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.up && playerMoveScript.watchDirection == PlayerMovement.Direction.down)
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.down && playerMoveScript.watchDirection == PlayerMovement.Direction.up)
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                
                //Diagonal case
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.diagonalUpLeft && (playerMoveScript.watchDirection == PlayerMovement.Direction.right || playerMoveScript.watchDirection == PlayerMovement.Direction.down))
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.diagonalDownLeft && (playerMoveScript.watchDirection == PlayerMovement.Direction.right || playerMoveScript.watchDirection == PlayerMovement.Direction.up))
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.diagonalUpRight && (playerMoveScript.watchDirection == PlayerMovement.Direction.left || playerMoveScript.watchDirection == PlayerMovement.Direction.down))
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }
                else if (minototaureMoveScript.watchDirection == MinototaureMovement.Direction.diagonalDownRight && (playerMoveScript.watchDirection == PlayerMovement.Direction.left || playerMoveScript.watchDirection == PlayerMovement.Direction.up))
                {
                    lauchAttack = false;
                    isStun = true;
                    player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                    StartCoroutine(Stun(stunTime));
                }

                else
                {
                    lauchAttack = false;
                    player.GetComponent<BasicHealthSystem>().TakeDmg(dmg, transform.position);
                    //StartCoroutine(NotStunt());
                    isStun = true;
                    StartCoroutine(Stun(noBlockedStunTime));
                }
            }
            else if (minototaureDetectScript.isOverlappingPlayer)                                       //if shield is disabled
            {
                lauchAttack = false;
                player.GetComponent<PlayerHealthSystem>().TakeDmg(dmg, transform.position);
                //StartCoroutine(NotStunt());
                isStun = true;
                StartCoroutine(Stun(noBlockedStunTime));
            }
        }
        else if (minototaureDetectScript.isOverlappingPlayer == true && !minototaureDetectScript.isOverlappingShield)           //if collide with player without shield
        {
            lauchAttack = false;
            player.GetComponent<PlayerHealthSystem>().TakeDmg(dmg, transform.position);
            //StartCoroutine(NotStunt());
            isStun = true;
            StartCoroutine(Stun(noBlockedStunTime));
        }
        else                                                                                                                    //if no collide
        {
            lauchAttack = false;
            //StartCoroutine(NotStunt());
            isStun = true;
            StartCoroutine(Stun(noBlockedStunTime));
        }
    }

    void AttackRotation()                                                       //rotate the attack collider linked to the watch direction
    {
        switch (minototaureMoveScript.watchDirection)
        {
            case MinototaureMovement.Direction.down:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MinototaureMovement.Direction.up:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case MinototaureMovement.Direction.right:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MinototaureMovement.Direction.left:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case MinototaureMovement.Direction.diagonalUpLeft:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case MinototaureMovement.Direction.diagonalDownLeft:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case MinototaureMovement.Direction.diagonalUpRight:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MinototaureMovement.Direction.diagonalDownRight:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    IEnumerator ChargePreparation()     //Coroutine that handle Charge Preparation
    {
        minototaureHealthScript.canTakeDmg = false;
        isPreparingAttack = true;                                   //animator bool
        yield return new WaitForSeconds(prepareChargeTime);         //Wait the preparation duration
        isPreparingAttack = false;                                  //animator bool
        lauchAttack = true;
    }

    IEnumerator Stun(float stun)                  //Coroutine that handle stun
    {
        if (!minototaureHealthScript.protectedByPegase)
        {
            minototaureHealthScript.canTakeDmg = true;
        }        
        yield return new WaitForSeconds(stun + 0.5f);       //wait the stun duration
        minototaureHealthScript.canTakeDmg = false;
        isStun = false;
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;                                    //saying that the attack is finished
        minototaureMoveScript.canMove = true;

        StartCoroutine(WaitBeforeAttack());
    }

    IEnumerator NotStunt()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;                                    //saying that the attack is finished
        minototaureMoveScript.canMove = true;

        StartCoroutine(WaitBeforeAttack());
    }

    IEnumerator WaitBeforeAttack()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(timeBtwAttack);
        cooldownAttack = false;
    }
}
