using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use to see if PJ is in the teleportation zone of Zeus
/// </summary>

public class ZeusTeleportZone : MonoBehaviour
{
    #region SerializeField var Statement
    [Header("Target Tag Selection")]

    [SerializeField] private string targetedElement = null;

    [Header("Player Detection")]
    [Space]

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
