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

    private Vector2 barBorderSizeDelta;
    private Vector2 barFillSizeDelta;

    private Sprite barBorderInitialSprite = null;
    private Sprite barFillInitialSprite = null;

    Animator barBorderAnimator = null;
    Animator barFillAnimator = null;

    #endregion

    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private Image borderStaminaBar = null;
    [SerializeField] private Image fillStaminaBar = null;
    [SerializeField] private Image graduationStaminaBar = null;

    [Space] public Sprite[] graduationBar;
    #endregion


    private void Start()
    {
        barBorderSizeDelta = borderStaminaBar.rectTransform.sizeDelta;
        barFillSizeDelta = fillStaminaBar.rectTransform.sizeDelta;

        barBorderInitialSprite = borderStaminaBar.sprite;
        barFillInitialSprite = fillStaminaBar.sprite;

        barBorderAnimator = borderStaminaBar.GetComponent<Animator>();
        barFillAnimator = fillStaminaBar.GetComponent<Animator>();

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

            BarShakingOnBlockedElement();
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

    private void BarShakingOnBlockedElement()
    {
        if (playerShieldSystem.elementBlocked)
        {
            barBorderAnimator.enabled = true;
            barBorderAnimator.SetTrigger("BarBorder_Damage");

            barFillAnimator.enabled = true;
            barFillAnimator.SetTrigger("BarFill_Damage");

            StartCoroutine(DisableAnimator());
            playerShieldSystem.elementBlocked = false;
        }
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(0.4f);

        barBorderAnimator.enabled = false;
        borderStaminaBar.rectTransform.sizeDelta = barBorderSizeDelta;
        borderStaminaBar.sprite = barBorderInitialSprite;

        barFillAnimator.enabled = false;
        fillStaminaBar.rectTransform.sizeDelta = barFillSizeDelta;
        fillStaminaBar.sprite = barFillInitialSprite;
    }
}
