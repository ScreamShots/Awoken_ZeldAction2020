using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [Header("Target Tag Selection")]

    [SerializeField] private string targetedElement;
    [SerializeField] private string[] allTags;
    
    [Header("Element Detection")]
    [Space]

    public List<GameObject> detectedElement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if(element.transform.parent.tag == targetedElement && element.tag == "HitBox")
        {
            detectedElement.Add(element.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform.parent.tag == targetedElement && element.tag == "HitBox")
        {
            detectedElement.Remove(element.transform.parent.gameObject);
        }
    }

    [ContextMenu("Refresh Tag List")]
    void RefreshTagList()
    {
        allTags = UnityEditorInternal.InternalEditorUtility.tags;
    }
}
