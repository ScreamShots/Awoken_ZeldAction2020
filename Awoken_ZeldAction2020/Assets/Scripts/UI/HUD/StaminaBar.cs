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
    [SerializeField] private Image graduationStaminaBar = null;

    [Space] public Sprite[] graduationBar;
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

            ShowBarGraduation();
        }
    }

    private void ShowBarGraduation()
    {
        switch (playerShieldSystem.maxStamina)
        {
            case 40:
                graduationStaminaBar.sprite = graduationBar[0];
                break;
            case 50:
                graduationStaminaBar.sprite = graduationBar[1];
                break;
            case 60:
                graduationStaminaBar.sprite = graduationBar[2];
                break;
            case 70:
                graduationStaminaBar.sprite = graduationBar[3];
                break;
            case 80:
                graduationStaminaBar.sprite = graduationBar[4];
                break;
            case 90:
                graduationStaminaBar.sprite = graduationBar[5];
                break;
        }
    }
}
