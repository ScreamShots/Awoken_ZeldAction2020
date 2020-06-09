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

    public float maxNumOfHearts;
    public float currentNumOfHearts;

    private Sprite currentSprite = null;

    private Vector2 normalSizeDelta;
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
        normalSizeDelta = hearts[0].rectTransform.sizeDelta;

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
                currentSprite = hearts[i].sprite;

                if (i < maxNumOfHearts)
                {
                    hearts[i].enabled = true;                               //check the max PV of Player and convert to heart max 
                }
                else
                {
                    hearts[i].enabled = false;
                }

                if (i <= (currentNumOfHearts - 1))
                {
                    if (currentSprite == halfHeart)                          //regen half to full
                    {
                        hearts[i].GetComponent<Animator>().enabled = true;
                        hearts[i].GetComponent<Animator>().SetTrigger("Heal_full_half");
                        hearts[i].sprite = fullHeart;

                        StartCoroutine(DisableAnimator(i, fullHeart));
                    }
                    else if (currentSprite == emptyHeart)                    //regen empty to full
                    {
                        hearts[i].GetComponent<Animator>().enabled = true;
                        hearts[i].GetComponent<Animator>().SetTrigger("Heal_full");
                        hearts[i].sprite = fullHeart;

                        StartCoroutine(DisableAnimator(i, fullHeart));
                    }                
                }
                else if (i <= (currentNumOfHearts - 1) + 0.5f)
                {
                    if (currentSprite == fullHeart)                          //damage full to half
                    {
                        hearts[i].GetComponent<Animator>().enabled = true;
                        hearts[i].GetComponent<Animator>().SetTrigger("Damage_full_half");
                        hearts[i].sprite = halfHeart;

                        StartCoroutine(DisableAnimator(i, halfHeart));
                    }
                    else if (currentSprite == emptyHeart)                    //regen empty to half
                    {
                        hearts[i].GetComponent<Animator>().enabled = true;
                        hearts[i].GetComponent<Animator>().SetTrigger("Heal_half");
                        hearts[i].sprite = halfHeart;

                        StartCoroutine(DisableAnimator(i, halfHeart));
                    }
                }
                else
                {
                    if (currentSprite == fullHeart)                          //damage full to empty
                    {
                        hearts[i].GetComponent<Animator>().enabled = true;
                        hearts[i].GetComponent<Animator>().SetTrigger("Damage_full");
                        hearts[i].sprite = emptyHeart;

                        StartCoroutine(DisableAnimator(i, emptyHeart));
                    }
                    else if (currentSprite == halfHeart)                    //damage half to empty
                    {
                        hearts[i].GetComponent<Animator>().enabled = true;
                        hearts[i].GetComponent<Animator>().SetTrigger("Damage_half");
                        hearts[i].sprite = emptyHeart;

                        StartCoroutine(DisableAnimator(i, emptyHeart));
                    }
                }              
            }
        }
    }

    IEnumerator DisableAnimator(int i, Sprite spriteToActivate)
    {
        yield return new WaitForSeconds(0.4f);

        hearts[i].GetComponent<Animator>().enabled = false;
        hearts[i].rectTransform.sizeDelta = normalSizeDelta;
        hearts[i].sprite = spriteToActivate;
    }
}
