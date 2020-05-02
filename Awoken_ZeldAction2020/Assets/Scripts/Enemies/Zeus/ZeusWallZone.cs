using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusWallZone : MonoBehaviour
{
    #region SerializeField var Statement
    [Header("Target Tag Selection")]

    [SerializeField] private string targetedElement = null;

    [Header("Bullet Detection")]
    [Space]

    public bool bulletDetection;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform.tag == targetedElement && element != null)
        {
            bulletDetection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform.tag == targetedElement && element != null)
        {
            bulletDetection = false;
        }
    }
}
