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
    private List<GameObject> inRangeElement;
    PlayerIndex playerIndex;                        //requiered for gamepad vibrations

    #endregion

    #region SerializeField var Statement
    [Header("Requiered Elements")]

    [SerializeField] private GameObject attackZone;

    [Header("Stats")]

    [Min(0)]
    [SerializeField] private float dmg;
    [Range(0f,0.5f)]
    [SerializeField] private float attackProjection;
    [Min(0)]
    [SerializeField] private float timeBtwAttack;

    [Header("FeedBack")]

    [Min(0)]
    [SerializeField] private float vibrateIntensity;

    [Header("Dev Tools")]

    [SerializeField] private bool autoAttack;

    #endregion 

    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
        attackZoneBehaviour = attackZone.GetComponent<AttackZone>();
    }

    private void Update()
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

    IEnumerator LaunchAttack()
    {
        PlayerStatusManager.Instance.isAttacking = true;                        //security state change (see PlayerStatusManager)

        switch (playerMoveScript.watchDirection)                                //Add force to the player on the targeted direction when it hit (intensity depends on attackprojection value
        {
            case PlayerMovement.Direction.down:

                yield return new WaitForFixedUpdate();
                PlayerMovement.playerRgb.velocity = new Vector2(0,0);
                PlayerMovement.playerRgb.AddForce(new Vector2(0,-1) * attackProjection);

                break;
            case PlayerMovement.Direction.up:

                yield return new WaitForFixedUpdate();
                PlayerMovement.playerRgb.velocity = new Vector2(0, 0);
                PlayerMovement.playerRgb.AddForce(new Vector2(0, 1) * attackProjection); ;

                break;
            case PlayerMovement.Direction.right:

                yield return new WaitForFixedUpdate();
                PlayerMovement.playerRgb.velocity = new Vector2(0, 0);
                PlayerMovement.playerRgb.AddForce(new Vector2(1, 0) * attackProjection); ;

                break;
            case PlayerMovement.Direction.left:

                yield return new WaitForFixedUpdate();
                PlayerMovement.playerRgb.velocity = new Vector2(0, 0);
                PlayerMovement.playerRgb.AddForce(new Vector2(-1, 0) * attackProjection); ;

                break;
        }

        inRangeElement = attackZoneBehaviour.detectedElement;

        if (inRangeElement.Count > 0)                      //if there is enemies in range infligt damages to them and do vibration on the gamepad
        {
            for (int i = 0; i < inRangeElement.Count; i++)
            {
                inRangeElement[i].GetComponent<BasicHealthSystem>().TakeDmg(dmg);
            }

            GamePad.SetVibration(playerIndex, vibrateIntensity, vibrateIntensity);
        }
        
        yield return new WaitForSeconds(0.25f);                                 //Wait for the animation end

        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);                  //Reset velocity to be sure Force added upward is stopped
        GamePad.SetVibration(playerIndex, 0f, 0f);                              //Stop gamepad vibration

        PlayerStatusManager.Instance.needToEndAttack = true;                    //security state change (see PlayerStatusManager)
        PlayerStatusManager.Instance.cdOnAttack = true;                         //security state change (see PlayerStatusManager)

        yield return new WaitForSeconds(timeBtwAttack);                                  //attackspeed

        PlayerStatusManager.Instance.cdOnAttack = false;                        //security state change (see PlayerStatusManager)

    }
}
