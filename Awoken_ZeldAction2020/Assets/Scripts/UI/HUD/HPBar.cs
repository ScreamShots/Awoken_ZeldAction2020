using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Rémi Sécher
/// This script is temporary. Used to display dynamic hp bar relative to the player's life
/// </summary>
public class HPBar : MonoBehaviour
{
    #region HideInInspector var Statement

    private BasicHealthSystem playerHpSystem;

    #endregion

    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private Image fillHpBar = null;

    #endregion


    private void Start()
    {
        if(PlayerManager.Instance != null)
        {
            playerHpSystem = PlayerManager.Instance.gameObject.GetComponent<BasicHealthSystem>();       // getting the player's health managment script
        }        
    }

    private void Update()
    {
        if(playerHpSystem != null)
        {
            fillHpBar.fillAmount = playerHpSystem.currentHp / playerHpSystem.maxHp;                     //Filling of the UI immage depends of the hp value of the player compare to his max health
        }        
    }
}
