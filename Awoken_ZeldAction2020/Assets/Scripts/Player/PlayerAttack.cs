using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Made by Rémi Sécher
///Scrpit managin attack of the player and feedback.
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    #region HideInInspector var Statement

    private AttackZone attackZoneBehaviour;
    private PlayerMovement playerMoveScript;
    [HideInInspector]
    public List<GameObject> inRangeElement;
    PlayerIndex playerIndex = PlayerIndex.One;                        //requiered for gamepad vibrations
    [HideInInspector]
    public float attackState = 0;
    [HideInInspector]
    public float currentFury = 0;

    #endregion

    #region SerializeField var Statement
    [Header("Requiered Elements")]

    [SerializeField] private GameObject attackZone = null;

    [Header("Stats")]

    [Min(0)]
    [SerializeField] private float firstAttackdmg = 0;
    [Min(0)]
    [SerializeField] private float secondAttackdmg = 0;
    [Min(0)]
    [SerializeField] private float thirdAttackdmg = 0;
    [Range(0f, 50f)]
    [SerializeField] private float attackProjection = 0;
    [Min(0)]
    [SerializeField] private float timeBtwAttack = 0;
    [Min(0)]
    [SerializeField] private float timeBtwCombo = 0;
    [Min(0)]
    [SerializeField] private float timeComboFade = 0;


    [Header("FeedBack")]

    [Min(0)]
    [SerializeField] private float vibrateIntensity = 0;

    [Header("Fury")]

    [Min(0)]
    public float maxFury = 0;
    [SerializeField] private float furyGainAttackOne = 0;
    [SerializeField] private float furyGainAttackTwo = 0;
    [SerializeField] private float furyGainAttackThree = 0;


    [Header("Dev Tools")]

    [SerializeField] private bool autoAttack = false;

    #endregion 

    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
        attackZoneBehaviour = attackZone.GetComponent<AttackZone>();
    }

    private void Update()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.Running)
        {
            AttackRotation();

            if (Input.GetButtonDown("Attack") && PlayerStatusManager.Instance.canAttack)
            {
                StartCoroutine(LaunchAttack());
            }

            if (autoAttack && PlayerStatusManager.Instance.canAttack)                                   //Dev Tools enabling the auto attack (to test max attack rate)
            {
                StartCoroutine(LaunchAttack());
            }
        }
        
    }

    void AttackRotation()                                                       //rotate the attack collider linked to the watch direction
    {
        switch (playerMoveScript.watchDirection)
        {
            case PlayerMovement.Direction.down:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case PlayerMovement.Direction.up:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case PlayerMovement.Direction.right:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case PlayerMovement.Direction.left:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }

    IEnumerator FadeComboTimer()
    {
        float timer = timeComboFade;

        while (timer > 0)
        {
            yield return new WaitForEndOfFrame();
            if (PlayerStatusManager.Instance.isAttacking)
            {
                break;
            }
            if (PlayerStatusManager.Instance.canMove && PlayerMovement.playerRgb.velocity.x != 0 || PlayerMovement.playerRgb.velocity.y != 0)
            {
                attackState = 0;
                break;
            }
            if (PlayerStatusManager.Instance.isBlocking)
            {
                attackState = 0;
                break;
            }

            timer -= Time.deltaTime;

        }
        if (timer <= 0)
        {
            attackState = 0;
        }
    }


    public IEnumerator LaunchAttack()
    {
        attackState += 1;
        PlayerStatusManager.Instance.isAttacking = true;
        yield return new WaitForFixedUpdate();
        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);

        switch (playerMoveScript.watchDirection)                                //Add force to the player on the targeted direction when it hit (intensity depends on attackprojection value
        {
            case PlayerMovement.Direction.down:

                PlayerMovement.playerRgb.AddForce(new Vector2(0, -1) * attackProjection);

                break;
            case PlayerMovement.Direction.up:

                PlayerMovement.playerRgb.AddForce(new Vector2(0, 1) * attackProjection); ;

                break;
            case PlayerMovement.Direction.right:

                PlayerMovement.playerRgb.AddForce(new Vector2(1, 0) * attackProjection); ;

                break;
            case PlayerMovement.Direction.left:

                PlayerMovement.playerRgb.AddForce(new Vector2(-1, 0) * attackProjection); ;

                break;
        }

        inRangeElement = attackZoneBehaviour.detectedElement;

        if (inRangeElement.Count > 0)                      //if there is enemies in range infligt damages to them and do vibration on the gamepad
        {
            bool enemyIsImune = true;
            for (int i = 0; i < inRangeElement.Count; i++)
            {
                if (inRangeElement[i].GetComponent<EnemyHealthSystem>() != null) {
                    switch (attackState)
                    {
                        case 1:
                            inRangeElement[i].GetComponent<EnemyHealthSystem>().TakeDmg(firstAttackdmg);
                            break;
                        case 2:
                            inRangeElement[i].GetComponent<EnemyHealthSystem>().TakeDmg(secondAttackdmg);
                            break;
                        case 3:
                            inRangeElement[i].GetComponent<EnemyHealthSystem>().TakeDmg(thirdAttackdmg);
                            break;
                        default:
                            inRangeElement[i].GetComponent<EnemyHealthSystem>().TakeDmg(firstAttackdmg);
                            break;
                    }
                    if (inRangeElement[i].GetComponent<EnemyHealthSystem>().canTakeDmg)
                    {
                        enemyIsImune = false;
                    }
                }
                else
                {
                    switch (attackState)
                    {
                        case 1:
                            inRangeElement[i].GetComponent<GameElementsHealthSystem>().TakeDmg(firstAttackdmg);
                            break;
                        case 2:
                            inRangeElement[i].GetComponent<GameElementsHealthSystem>().TakeDmg(secondAttackdmg);
                            break;
                        case 3:
                            inRangeElement[i].GetComponent<GameElementsHealthSystem>().TakeDmg(thirdAttackdmg);
                            break;
                        default:
                            inRangeElement[i].GetComponent<GameElementsHealthSystem>().TakeDmg(firstAttackdmg);
                            break;
                    }
                    if (inRangeElement[i].GetComponent<GameElementsHealthSystem>().canTakeDmg)
                    {
                        enemyIsImune = false;
                    }
                }
               
            }
            if(enemyIsImune == false)
            {
                switch (attackState)
                {
                    case 1:
                        currentFury += furyGainAttackOne;
                        if (currentFury > maxFury)
                        {
                            currentFury = maxFury;
                        }
                        break;

                    case 2:
                        currentFury += furyGainAttackTwo;
                        if (currentFury > maxFury)
                        {
                            currentFury = maxFury;
                        }
                        break;
                    case 3:
                        currentFury += furyGainAttackThree;
                        if (currentFury > maxFury)
                        {
                            currentFury = maxFury;
                        }
                        break;
                    default:
                        currentFury += furyGainAttackOne;
                        if (currentFury > maxFury)
                        {
                            currentFury = maxFury;
                        }
                        break;
                }

                GamePad.SetVibration(playerIndex, vibrateIntensity * Mathf.Pow(attackState, 3), vibrateIntensity * Mathf.Pow(attackState, 3));
            }            
        }

        yield return new WaitForSeconds(0.15f);                                 //Wait for the animation end

        GamePad.SetVibration(playerIndex, 0f, 0f);                              //Stop gamepad vibration

        yield return new WaitForSeconds(0.1f);                                 //Wait for the animation end

        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);                  //Reset velocity to be sure Force added upward is stopped       

        PlayerStatusManager.Instance.needToEndAttack = true;                    //security state change (see PlayerStatusManager)
        PlayerStatusManager.Instance.cdOnAttack = true;                         //security state change (see PlayerStatusManager)

        if (attackState == 3)
        {
            attackState = 0;
            yield return new WaitForSeconds(timeBtwCombo);
            PlayerStatusManager.Instance.cdOnAttack = false;
        }
        else
        {
            yield return new WaitForSeconds(timeBtwAttack);                                  //attackspeed

            PlayerStatusManager.Instance.cdOnAttack = false;                        //security state change (see PlayerStatusManager)
            StartCoroutine(FadeComboTimer());
        }
    }
}
