using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Antoine
/// This script is use to display boss life 
/// </summary>

public class HPBossBar : MonoBehaviour
{
    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private Image fillHealthBar = null;

    #endregion

    private void Update()
    {
        if (BossManager.Instance != null)
        {
            fillHealthBar.fillAmount = BossManager.Instance.currentHp / BossManager.Instance.maxHp;                    
        }
    }
}
