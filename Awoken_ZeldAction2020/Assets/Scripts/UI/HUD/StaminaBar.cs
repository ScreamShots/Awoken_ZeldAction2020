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
    [SerializeField] private Image fillStaminaBar = null;

    #endregion


    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            playerShieldSystem = PlayerManager.Instance.gameObject.GetComponent<PlayerShield>();       // getting the player's shield managment script
        }
    }

    private void Update()
    {
        if (ProgressionManager.Instance.undergroudCutSceneDone)
        {
            if (playerShieldSystem.currentStamina >= playerShieldSystem.maxStamina - playerShieldSystem.maxStamina * 0.01) // the soustraction on the second part of the test creat a margin that smooth the stamina bar display
            {
                fillStaminaBar.transform.parent.gameObject.SetActive(false);                        //if the stamina is at his max value hide the stamina bar
            }
            else
            {
                fillStaminaBar.transform.parent.gameObject.SetActive(true);                        //if the stamina is not at his max vanue display the stamina bar     
            }

            if (playerShieldSystem != null)
            {
                fillStaminaBar.fillAmount = playerShieldSystem.currentStamina / playerShieldSystem.maxStamina;                     //Filling of the UI immage depends of the hp value of the player compare to his max health
            }
        }
        else
        {
            fillStaminaBar.transform.parent.gameObject.SetActive(false);
        }

    }
}
