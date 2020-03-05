using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance;

    //Current State Algorithm

    public int currentState;
    public int lastState;

    //In Time Status

    public static bool isDashing;       // state 1
    public static bool isFalling;       // state 2
    public static bool isAttacking;     // state 3
    public static bool isUsing;         // state 4
    public static bool isInteracting;   // state 5

    public static bool isLoading;


    //Status Ending

    public static bool needToEndDash;
    public static bool needToEndFall;
    public static bool needToEndAttack;
    public static bool needToEndUse;
    public static bool needToEndInteract;

    public static bool needToEndLoad;

    //CD Status

    public static bool cdOnDash;
    public static bool cdOnAttack;
    public static bool cdOnInteract;

    //Availability

    public static bool canMove;
    public static bool canDash;
    public static bool canFall;
    public static bool canAttack;
    public static bool canUse;
    public static bool canInteract;
    public static bool canPick;
    public static bool canScroll;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentState = 0;
        lastState = 0;

        isDashing = false;
        isFalling = false;
        isAttacking = false;
        isUsing = false;
        isInteracting = false;
        isLoading = false;

        needToEndAttack = false;
        needToEndDash = false;
        needToEndFall = false;
        needToEndInteract = false;
        needToEndUse = false;
        needToEndLoad = false;

        cdOnAttack = false;
        cdOnDash = false;
        cdOnInteract = false;

        canAttack = true;
        canDash = true;
        canFall = true;
        canInteract = true;
        canMove = true;
        canPick = true;
        canScroll = true;
        canUse = true;

    }
    private void FixedUpdate()
    {


        if (needToEndDash == true)
        {
            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnInteract == false) { canInteract = true; }

            canMove = true;
            canPick = true;
            canFall = true;
            canUse = true;

            needToEndDash = false;
            isDashing = false;

        }
        else if (needToEndFall == true)
        {
            if (cdOnDash == false) { canDash = true; }

            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnInteract == false) { canInteract = true; }

            canMove = true;
            canPick = true;
            canUse = true;

            needToEndFall = false;
            isFalling = false;
        }
        else if (needToEndAttack == true)
        {
            if (cdOnDash == false) { canDash = true; }

            if (cdOnInteract == false) { canInteract = true; }

            canMove = true;
            canPick = true;
            canFall = true;
            canUse = true;
            canScroll = true;

            needToEndAttack = false;
            isAttacking = false;
        }
        else if (needToEndUse == true)
        {
            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnInteract == false) { canInteract = true; }

            if (cdOnDash == false) { canDash = true; }

            canMove = true;
            canPick = true;
            canFall = true;
            canScroll = true;

            needToEndUse = false;
            isUsing = false;
        }
        else if (needToEndInteract == true)
        {
            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnDash == false) { canDash = true; }

            canMove = true;
            canPick = true;
            canScroll = true;
            canFall = true;
            canUse = true;

            needToEndInteract = false;
            isInteracting = false;
        }
        else if (needToEndLoad == true)
        {
            canAttack = true;
            canDash = true;
            canFall = true;
            canInteract = true;
            canMove = true;
            canPick = true;
            canScroll = true;
            canUse = true;
        }

        if (isLoading == true)
        {
            canAttack = false;
            canDash = false;
            canFall = false;
            canInteract = false;
            canMove = false;
            canPick = false;
            canScroll = false;
            canUse = false;
            PlayerMovement.playerRgb.velocity = new Vector3(0, 0, 0);
        }


        if (isDashing == true) { currentState = 1; }

        else if (isFalling == true) { currentState = 2; }

        else if (isAttacking == true) { currentState = 3; }

        else if (isUsing == true) { currentState = 4; }

        else if (isInteracting == true) { currentState = 5; }

        else
        { currentState = 0; }

        if (currentState != lastState && isLoading == false)
        {
            lastState = currentState;

            switch (currentState)
            {
                case 1:
                    canMove = false;
                    canDash = false;
                    canFall = false;
                    canAttack = false;
                    canInteract = false;
                    canUse = false;
                    canPick = false;
                    break;

                case 2:
                    canMove = false;
                    canDash = false;
                    canFall = false;
                    canAttack = false;
                    canUse = false;
                    canInteract = false;
                    canPick = false;
                    PlayerMovement.playerRgb.velocity = new Vector3(0, 0, 0);
                    break;

                case 3:
                    canMove = false;
                    canDash = false;
                    canFall = false;
                    canAttack = false;
                    canUse = false;
                    canInteract = false;
                    canPick = false;
                    canScroll = false;
                    PlayerMovement.playerRgb.velocity = new Vector3(0, 0, 0);
                    break;

                case 4:
                    canMove = true;
                    canDash = false;
                    canFall = false;
                    canAttack = false;
                    canUse = false;
                    canInteract = false;
                    canPick = false;
                    canScroll = false;
                    break;

                case 5:
                    canMove = false;
                    canDash = false;
                    canFall = false;
                    canAttack = false;
                    canUse = false;
                    canInteract = false;
                    canPick = false;
                    canScroll = false;
                    PlayerMovement.playerRgb.velocity = new Vector3(0, 0, 0);
                    break;

                default:
                    break;
            }

        }

        if (currentState == 0 && isLoading == false)
        {
            if (cdOnDash == false) { canDash = true; }

            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnInteract == false) { canInteract = true; }

            canFall = true;
            canUse = true;
        }
    }
}
