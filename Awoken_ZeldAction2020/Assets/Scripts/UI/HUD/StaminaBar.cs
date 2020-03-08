using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Rémi Sécher
/// This script is temporary. Used to display dynamic stamina relative to player shield script
/// </summary>

public class StaminaBar : MonoBehaviour
{
    #region HideInInspector var Statement

    private PlayerShield playerShieldSystem;

    #endregion

    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private Image fillStaminaBar;

    #endregion


    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            playerShieldSystem = PlayerManager.Instance.gameObject.GetComponent<PlayerShield>();       // getting the player's health managment script
        }
    }

    private void Update()
    {
        if(playerShieldSystem.currentStamina >= playerShieldSystem.maxStamina)
        {
            fillStaminaBar.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            fillStaminaBar.transform.parent.gameObject.SetActive(true);
        }

        if (playerShieldSystem != null)
        {
            fillStaminaBar.fillAmount = playerShieldSystem.currentStamina / playerShieldSystem.maxStamina;                     //Filling of the UI immage depends of the hp value of the player compare to his max health
        }
    }
}
