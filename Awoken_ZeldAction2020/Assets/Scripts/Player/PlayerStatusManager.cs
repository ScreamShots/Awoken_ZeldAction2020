using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This class is used to test different state of the player and capacity Availability
/// It is acessable from anywhere by using the PlayerStatusManager.Instacnce command
/// </summary>


[RequireComponent(typeof(PlayerManager))]
public class PlayerStatusManager : MonoBehaviour
{
    public static PlayerStatusManager Instance;
    public enum State { neutral, block, attack, interract, spin, charge, knockBack};

    #region Current State Algorithm
    [SerializeField] public State currentState = State.neutral;
    [HideInInspector]
    private State lastState = State.neutral;

    #endregion

    #region In Time Status

    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isInteracting;
    [HideInInspector] public bool isSpinning;
    [HideInInspector] public bool isCharging;
    [HideInInspector] public bool isKnockBacked;

    [HideInInspector] public bool isLoading;

    #endregion

    #region Status Ending

    [HideInInspector] public bool needToEndBlock;
    [HideInInspector] public bool needToEndAttack;
    [HideInInspector] public bool needToEndInteract;
    [HideInInspector] public bool needToEndSpin;
    [HideInInspector] public bool needToEndCharge;
    [HideInInspector] public bool needToEndKnockBack;

    [HideInInspector] public bool needToEndLoad;

    #endregion

    #region CD Status
    [Space][Header("CD Status")]
    public bool cdOnBlock;
    public bool cdOnAttack;
    public bool cdOnInteract;
    public bool cdOnSpin;
    public bool cdOnCharge;

    #endregion

    #region Availability
    [Space] [Header("Availability")]
    public bool canMove = true;
    public bool canBlock = true;
    public bool canAttack = true;
    public bool canInteract = true;
    public bool canSpin = true;
    public bool canCharge = true;
    public bool canChangeDirection = true;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        NeedToEnd();
        StateManagement();
    }

    void NeedToEnd()
    {
        if (needToEndBlock == true)
        {
            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnSpin == false) { canSpin = true; }

            if (cdOnCharge == false) { canCharge = true; }

            
            needToEndBlock = false;
            isBlocking = false;

        }
        else if (needToEndAttack == true)
        {
            if (cdOnBlock == false) { canBlock = true; }

            if (cdOnSpin == false) { canSpin = true; }

            if (cdOnCharge == false) { canCharge = true; }

            needToEndAttack = false;
            isAttacking = false;
        }
        else if (needToEndInteract == true)
        {
            if (cdOnBlock == false) { canBlock = true; }

            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnSpin == false) { canSpin = true; }

            if (cdOnCharge == false) { canCharge = true; }

            needToEndInteract = false;
            isInteracting = false;
        }
        else if (needToEndSpin == true)
        {
            if (cdOnBlock == false) { canBlock = true; }

            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnCharge == false) { canCharge = true; }

            needToEndSpin = false;
            isSpinning = false;
        }
        else if (needToEndCharge == true)
        {
            if (cdOnBlock == false) { canBlock = true; }

            if (cdOnAttack == false) { canAttack = true; }

            if (cdOnSpin == false) { canSpin = true; }

            needToEndCharge = false;
            isCharging = false;
        }
        else if (needToEndKnockBack == true)
        {
            if (isBlocking)
            {
                canMove = true;
                needToEndKnockBack = false;
                isKnockBacked = false;
            }
            else
            {
                if (cdOnAttack == false) { canAttack = true; }

                if (cdOnBlock == false) { canBlock = true; }

                if (cdOnSpin == false) { canSpin = true; }

                if (cdOnCharge == false) { canCharge = true; }

                needToEndKnockBack = false;
                isKnockBacked = false;
            }           
        }
        else if (needToEndLoad == true)
        {
            canMove = true;
            canBlock = true;
            canAttack = true;
            canInteract = true;
            canSpin = true;
            canCharge = true;
        }
    }
    void StateManagement()
    {
        if (isLoading == true)
        {
            canMove = false;
            canBlock = false;
            canAttack = false;
            canInteract = false;
            canSpin = false;
            canCharge = false;
            canChangeDirection = false;

            PlayerMovement.playerRgb.velocity = new Vector2(0, 0);

        }


        if (isBlocking == true) { currentState = State.block; }

        else if (isAttacking == true) { currentState = State.attack; }

        else if (isInteracting == true) { currentState = State.interract; }

        else if (isSpinning == true) { currentState = State.spin; }

        else if (isCharging == true) { currentState = State.charge; }

        else if (isKnockBacked == true) { currentState = State.knockBack; }

        else { currentState = State.neutral; }

        if (isKnockBacked && isBlocking)
        {
            canMove = false;
        }

        if (currentState != lastState && isLoading == false)
        {
            lastState = currentState;

            switch (currentState)
            {
                case State.block:
                    canAttack = false;
                    canInteract = false;
                    canSpin = false;
                    canCharge = false;
                    canChangeDirection = false;
                    break;

                case State.attack:
                    canMove = false;
                    canBlock = false;
                    canAttack = false;
                    canInteract = false;
                    canSpin = false;
                    canCharge = false;
                    canChangeDirection = false;
                    break;

                case State.interract:
                    canMove = false;
                    canBlock = false;
                    canAttack = false;
                    canInteract = false;
                    canSpin = false;
                    canCharge = false;
                    canChangeDirection = false;
                    break;

                case State.spin:
                    canMove = false;
                    canBlock = false;
                    canAttack = false;
                    canInteract = false;
                    canSpin = false;
                    canCharge = false;
                    canChangeDirection = false;
                    break;

                case State.charge:
                    canMove = false;
                    canBlock = false;
                    canAttack = false;
                    canInteract = false;
                    canSpin = false;
                    canCharge = false;
                    canChangeDirection = false;
                    break;

                case State.knockBack:
                    canMove = false;
                    canBlock = false;
                    canAttack = false;
                    canInteract = false;
                    canSpin = false;
                    canCharge = false;
                    canChangeDirection = false;
                    canAttack = false;
                    break;

                default:
                    break;
            }

        }

        if (currentState == State.neutral && isLoading == false)
        {
            if (cdOnBlock == false) canBlock = true;
            else canBlock = false;

            if (cdOnAttack == false) canAttack = true;
            else canAttack = false;

            if (cdOnSpin == false) canSpin = true;
            else canSpin = false;

            if (cdOnCharge == false) canCharge = true;
            else canCharge = false;

            canMove = true;
            canInteract = true;
            canChangeDirection = true;            
        }
    }
}
