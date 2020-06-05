using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This script is a HighLevel Reference for all functionality of the player
/// It's a static instance you can call using PlayerManager.Instance
/// </summary>

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [Header("Player Elements")]
    [Header("Rendering Elements")]
    
    [SerializeField]
    GameObject classicRender = null;
    [SerializeField]
    GameObject cutsceneRenderer = null;
    [SerializeField]
    RuntimeAnimatorController classicController = null;
    [SerializeField]
    RuntimeAnimatorController noShieldController = null;

    [Header("Gameplay Elements")]

    [Header ("Global Components")]

    [SerializeField]
    GameObject collisionDetection = null;
    [SerializeField]
    GameObject hitBox = null;
    [SerializeField]
    PlayerHealthSystem healthSystem = null;

    [Header("Attack Components")]

    [SerializeField]
    GameObject attackZone = null;
    [SerializeField]
    PlayerAttack attackScript = null;

    [Header("Block Components")]

    [SerializeField]
    GameObject shieldZone = null;
    [SerializeField]
    PlayerShield playerShieldScript = null;

    [Header("Pary Components")]

    [SerializeField]
    GameObject paryZone = null;

    [Header("ChargeComponents")]

    [SerializeField]
    GameObject chargeZones = null;
    [SerializeField]
    PlayerCharge chargeScript = null;

    //[SerializeField]
    //GameObject playerSound = null;

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("1 player Delete (can't be more than one player in the scene)");
        }
        
        #endregion
    }

    private void Start()
    {
        InitializePlayer();
    }

    public void PlayerInitializeCutScene()
    {
        classicRender.SetActive(false);
        attackZone.SetActive(false);
        collisionDetection.SetActive(false);
        hitBox.SetActive(false);
        shieldZone.SetActive(false);
        paryZone.SetActive(false);
        //playerSound.SetActive(false);
        cutsceneRenderer.SetActive(true);
    }

    public void PlayerEndCutScene()
    {
        classicRender.SetActive(true);
        attackZone.SetActive(true);
        collisionDetection.SetActive(true);
        hitBox.SetActive(true);
        if (ProgressionManager.Instance.PlayerCapacity["Block"])
        {
            shieldZone.SetActive(true);
        }
            
        if (ProgressionManager.Instance.PlayerCapacity["Pary"])
        {
            paryZone.SetActive(true);
        }
        //playerSound.SetActive(true);
        cutsceneRenderer.SetActive(false);
    }

    public void InitializePlayer()
    {
        if (!ProgressionManager.Instance.PlayerCapacity["Shield"])
        {
            // change animator Controller to no shield controller
            classicRender.GetComponent<Animator>().runtimeAnimatorController = noShieldController;
        }

        if (!ProgressionManager.Instance.PlayerCapacity["Block"])
        {
            shieldZone.SetActive(false);
            playerShieldScript.enabled = false;
        }
        else
        {
            playerShieldScript.maxStamina = ProgressionManager.Instance.maxStamina;
            playerShieldScript.currentStamina = ProgressionManager.Instance.playerStamina;
        }

        if (!ProgressionManager.Instance.PlayerCapacity["Pary"])
        {
            paryZone.SetActive(false);            
        }

        if (!ProgressionManager.Instance.PlayerCapacity["Charge"])
        {
            chargeZones.SetActive(false);
            chargeScript.enabled = false;
        }
        else
        {
            attackScript.currentFury = ProgressionManager.Instance.playerFury;
        }

        healthSystem.maxHp = ProgressionManager.Instance.maxHp;
        healthSystem.currentHp = ProgressionManager.Instance.playerHp;

    }

    public void ActivateShield()
    {
        classicRender.GetComponent<Animator>().runtimeAnimatorController = classicController;
    }

    public void ActivateBlock()
    {
        if(HUDManager.Instance != null)
        {
            HUDManager.Instance.ActivateBlockHUD();
        }

        shieldZone.SetActive(true);
        playerShieldScript.enabled = true;

        playerShieldScript.currentStamina = playerShieldScript.maxStamina;

    }

    public void ActivatePary()
    {
        paryZone.SetActive(true);
    }

    public void ActivateCharge()
    {
        if(HUDManager.Instance != null)
        {
            HUDManager.Instance.ActivateChargeHUD();
        }
        chargeZones.SetActive(true);
        chargeScript.enabled = true;

        attackScript.currentFury = attackScript.maxFury;
    }

    [ContextMenu("Activate all capacity")]
    public void ActivateAll()
    {
        ActivateShield();

        ActivateBlock();

        ActivatePary();

        ActivateCharge();
    }
}
