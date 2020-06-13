using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurryBar : MonoBehaviour
{
    #region HideInInspector var Statement

    private PlayerAttack playerAttackScript;

    private bool animationFurryFull = false;

    private Vector2 furryBorderSizeDelta;
    private Vector3 furryBorderPosition;
    private Sprite furryBorderInitialSprite = null;
    Animator furryBorderAnimator = null;

    #endregion

    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private Image furryFillBar = null;
    [SerializeField] private Image furryBorderBar = null;
    [SerializeField] private GameObject furryUI = null;

    #endregion


    private void Start()
    {
        furryBorderSizeDelta = furryBorderBar.rectTransform.sizeDelta;
        furryBorderPosition = furryBorderBar.rectTransform.position;
        furryBorderInitialSprite = furryBorderBar.sprite;
        furryBorderAnimator = furryBorderBar.GetComponent<Animator>();

        if (PlayerManager.Instance != null)
        {
            playerAttackScript = PlayerManager.Instance.gameObject.GetComponent<PlayerAttack>();       // getting the player's attack managment script
        }
    }

    void OnEnable()
    {
        if (animationFurryFull)
        {
            furryBorderAnimator.SetTrigger("BarFurry_Full");
        }
    }

    private void Update()
    {
        if (playerAttackScript != null && playerAttackScript.enabled == true)
        {
            if (!furryUI.activeInHierarchy)
            {
                furryUI.SetActive(true);
            }
            furryFillBar.fillAmount = playerAttackScript.currentFury / playerAttackScript.maxFury;                     //Filling of the UI immage depends of the hp value of the player compare to his max health
        }
        else
        {
            furryUI.SetActive(false);
        }

        if (furryFillBar.fillAmount == 1)
        {
            BarFurryFullAnimation();
        }
        else
        {
            furryBorderAnimator.enabled = false;
            furryBorderBar.rectTransform.sizeDelta = furryBorderSizeDelta;
            furryBorderBar.rectTransform.position = furryBorderPosition;
            furryBorderBar.sprite = furryBorderInitialSprite;

            animationFurryFull = false;
        }
    }

    private void BarFurryFullAnimation()
    {
        if (!animationFurryFull)
        {
            PlayerManager.Instance.GetComponentInChildren<PlayerAnimator>().FurryFull();
            furryBorderAnimator.enabled = true;
            furryBorderAnimator.SetTrigger("BarFurry_Full");

            animationFurryFull = true;
        }
    }
}
