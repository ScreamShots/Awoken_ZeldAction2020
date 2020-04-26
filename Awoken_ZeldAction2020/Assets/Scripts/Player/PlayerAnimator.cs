using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Made by Rémi Sécher
/// This script is used to manage the player animator
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    #region HideInInspector var Statement 

    private Animator plyAnimator;
    private PlayerMovement playerMoveScript;
    private PlayerAttack playerAttackScript;
    private PlayerHealthSystem playerHealthScript;
    private bool alreadyAttacked = false;

    #endregion

    private void Start()
    {
        plyAnimator = GetComponent<Animator>();                                     //finding requiered component
        playerMoveScript = GetComponentInParent<PlayerMovement>();
        playerAttackScript = GetComponentInParent<PlayerAttack>();
        playerHealthScript = GetComponentInParent<PlayerHealthSystem>();
    }

    private void Update()
    {
            SetWatchDirection();
            Running();
            TempAttack();
            SetBlock();
            HitKnockBack();
    }


    void SetBlock()                                                                  //Launch block animation
    {
        plyAnimator.SetBool("isBlocking", PlayerStatusManager.Instance.isBlocking);

        if(PlayerStatusManager.Instance.isBlocking == true)
        {
            switch (playerMoveScript.watchDirection)
            {
                case PlayerMovement.Direction.down:
                    if(PlayerMovement.playerRgb.velocity.y > 0)
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", -1);
                    }
                    else
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", 1);
                    }
                    break;
                case PlayerMovement.Direction.up:
                    if (PlayerMovement.playerRgb.velocity.y < 0)
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", -1);
                    }
                    else
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", 1);
                    }
                    break;
                case PlayerMovement.Direction.left:
                    if (PlayerMovement.playerRgb.velocity.x > 0)
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", -1);
                    }
                    else
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", 1);
                    }
                    break;
                case PlayerMovement.Direction.right:
                    if (PlayerMovement.playerRgb.velocity.x < 0)
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", -1);
                    }
                    else
                    {
                        plyAnimator.SetFloat("ShieldRunSpeed", 1);
                    }
                    break;
                default:                                    
                    break;
            }
        }               //change animation speed to -1 when he goes opposite of the looked direction to prevent moonwalk
    }                                                             
    void SetWatchDirection()                                                        //giving information relative to the watch direction to the animator
    {
        switch (playerMoveScript.watchDirection)
        {
            case PlayerMovement.Direction.down:
                plyAnimator.SetFloat("XMovement", 0);
                plyAnimator.SetFloat("YMovement", -1);
                break;
            case PlayerMovement.Direction.up:
                plyAnimator.SetFloat("XMovement", 0);
                plyAnimator.SetFloat("YMovement", 1);
                break;
            case PlayerMovement.Direction.left:
                plyAnimator.SetFloat("XMovement", -1);
                plyAnimator.SetFloat("YMovement", 0);
                break;
            case PlayerMovement.Direction.right:
                plyAnimator.SetFloat("XMovement", 1);
                plyAnimator.SetFloat("YMovement", 0);
                break;
            default:
                plyAnimator.SetFloat("XMovement", 0);
                plyAnimator.SetFloat("YMovement", -1);
                break;
        }
    }
    void Running()                                                                  //Launch run animation   
    {
        plyAnimator.SetBool("isRunning", playerMoveScript.isRunning);               
    }    
    void TempAttack()                                                               //Launch attack animation    
    {
        plyAnimator.SetInteger("AttackState", (int)playerAttackScript.attackState);
        if (PlayerStatusManager.Instance.isAttacking && !alreadyAttacked)          
        {
            alreadyAttacked = true;
            plyAnimator.SetTrigger("Temp_LaunchAttack");
        }
        else if (!PlayerStatusManager.Instance.isAttacking && alreadyAttacked)
        {
            alreadyAttacked = false;                                                //security to avoid animation starting twice for a single attack
        }
    }
    void HitKnockBack()
    {
        if(PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.knockBack)
        {
            plyAnimator.SetBool("isKnockBack", PlayerStatusManager.Instance.isKnockBacked);
        }
        else
        {
            plyAnimator.SetBool("isKnockBack", false);
        }

        if (PlayerStatusManager.Instance.isKnockBacked)
        {
            switch (playerHealthScript.knockBackDir)
            {
                case 0:
                    plyAnimator.SetFloat("XHit", 0);
                    plyAnimator.SetFloat("YHit", 1);
                    break;
                case 1:
                    plyAnimator.SetFloat("XHit", 0);
                    plyAnimator.SetFloat("YHit", -1);
                    break;
                case 2:
                    plyAnimator.SetFloat("XHit", 1);
                    plyAnimator.SetFloat("YHit", 0);
                    break;
                case 3:
                    plyAnimator.SetFloat("XHit", -1);
                    plyAnimator.SetFloat("YHit", 0);
                    break;
                default:
                    break;
            }
        }

    }
    public void Pary()
    {
        plyAnimator.SetBool("isPary", true);
        StartCoroutine(StopPary());
    }
    IEnumerator StopPary()
    {
        AnimationClip[] allAnimatorClips = plyAnimator.runtimeAnimatorController.animationClips;
        float paryAnimationLength = 0;
        for (int i =0; i < allAnimatorClips.Length; i++)
        {
            if(allAnimatorClips[i].name == "PJ_parry_down")
            {
                paryAnimationLength = allAnimatorClips[i].length;
                break;
            }
        }
        yield return new WaitForSeconds(paryAnimationLength);
        plyAnimator.SetBool("isPary", false);
    }

}
