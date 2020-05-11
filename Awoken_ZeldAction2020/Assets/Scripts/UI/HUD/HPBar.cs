using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Antoine 
/// This script is use to get the life of Player and transform to heart
/// </summary>
/// 
public class HPBar : MonoBehaviour
{
    #region HideInInspector var Statement

    private PlayerHealthSystem playerHpSystem;

    private float maxNumOfHearts;
    private float currentNumOfHearts;
    #endregion

    #region SerialiazeFiled var Statement

    [Header("Requiered Sprites")]

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [Header("Requiered Hearts")]
    
    public Image[] hearts;
    #endregion


    private void Start()
    {
        if(PlayerManager.Instance != null)
        {
            playerHpSystem = PlayerManager.Instance.gameObject.GetComponent<PlayerHealthSystem>();       // getting the player's health managment script
        }
    }

    private void Update()
    {
        UpdateHeartBar();
    }

    private void UpdateHeartBar()                                           //function to refresh the display of heart bar
    {
        if (playerHpSystem != null)
        {
            maxNumOfHearts = playerHpSystem.maxHp / 20;                     //one heart = 20PV, a half heart = 10PV
            currentNumOfHearts = playerHpSystem.currentHp / 20;

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < maxNumOfHearts)
                {
                    hearts[i].enabled = true;                               //check the max PV of Player and convert to heart max 
                }
                else
                {
                    hearts[i].enabled = false;
                }

                hearts[i].sprite = emptyHeart;

                for (float c = 0; c < currentNumOfHearts; c += 0.5f)
                {
                    int b = Mathf.FloorToInt(c);
                    int e = (int)c;

                    if (c != (float)b)                                      //if the current heart value is not a decimal
                    {
                        hearts[e].sprite = fullHeart;
                    }
                    else
                    {
                        hearts[e].sprite = halfHeart;                       //if the current heart value is a decimal
                    }
                }
            }
        }
    }
}
