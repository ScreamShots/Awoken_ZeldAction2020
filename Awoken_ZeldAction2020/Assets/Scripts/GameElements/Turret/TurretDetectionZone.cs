using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use to detect if player is in turret zone
/// </summary>
/// 
public class TurretDetectionZone : MonoBehaviour
{
    #region Inspector Settings
    [Header("Target Tag Selection")]
    [SerializeField] private string targetedElement = null;

    public bool playerInZone;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox" && element != null)
            {
                playerInZone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox" && element != null)
            {
                playerInZone = false;
            }
        }
    }
}
