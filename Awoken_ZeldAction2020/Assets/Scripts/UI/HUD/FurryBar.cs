using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurryBar : MonoBehaviour
{
    #region HideInInspector var Statement

    private PlayerAttack playerAttackScript;

    #endregion

    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private Image furryFillBar = null;
    [SerializeField] private GameObject furryUI = null;

    #endregion


    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            playerAttackScript = PlayerManager.Instance.gameObject.GetComponent<PlayerAttack>();       // getting the player's attack managment script
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
    }
}
