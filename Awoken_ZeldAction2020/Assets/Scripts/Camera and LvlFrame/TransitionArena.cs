using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use to know if Player is in transition zone for switch between cameras
/// </summary>

public class TransitionArena : MonoBehaviour
{
    #region Inspector Settings
    [Header("Target Tag Selection")]
    [SerializeField] private string targetedElement = null;

    public bool playerInZone;
    [HideInInspector] public bool cutsceneRunning = false;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox" && element != null)
            {
                if (!cutsceneRunning)
                {
                    playerInZone = true;
                }
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
                if (!cutsceneRunning)
                {
                    playerInZone = false;
                }
            }
        }
    }
}
