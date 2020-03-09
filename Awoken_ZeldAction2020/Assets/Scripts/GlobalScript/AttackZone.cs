using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This Script is a base script that can be use to make different attack patterns.
/// You can modify it a bit if u want to use inheritance.
/// This script can be use on enemy and player, it's very flexible since you can change the target from the inspector using tag.
/// </summary>

public class AttackZone : MonoBehaviour
{
    #region SerializeField var Statement

    [Header("Target Tag Selection")]

    [SerializeField] private string targetedElement = null;
    [SerializeField] private string[] allTags;
    
    [Header("Element Detection")]
    [Space]

    public List<GameObject> detectedElement;

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox" && element != null)         //Removing gameobject that are not in the range anymore.
            {
                detectedElement.Add(element.transform.parent.gameObject);
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox" && element != null)         //Removing gameobject that are not in the range anymore.
            {
                detectedElement.Remove(element.transform.parent.gameObject);
            }
        }        
    }

    [ContextMenu("Refresh Tag List")]
    void RefreshTagList()
    {
        allTags = UnityEditorInternal.InternalEditorUtility.tags;                               //Function to access the tag's List from unity in the inspector even if you are not in PlayMode
    }                                                                                           //To do so just right click on the script name and click on the function name in the menu
}
