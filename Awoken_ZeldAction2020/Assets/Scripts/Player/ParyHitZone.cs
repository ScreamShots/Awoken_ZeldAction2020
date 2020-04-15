using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParyHitZone : MonoBehaviour
{
    [Header("Detection")]

    public List<GameObject> detectedElements;


    private void Update()
    {
        if (detectedElements.Count > 0)
        {
            CleanList();                    //Delete empty element in the list (can be due to object destruction) to prevent low evel error
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;          //store temporary the gameobject involve in the collision to easier reference in the function

        if (element.transform != element.transform.root)     //security test to prevent error of the objet as not parent's transform
        {
            if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")      //Test if we th collision is with an ennemy that attack with an attack zone
            {
                detectedElements.Add(element.transform.parent.gameObject);                  //Add this enemy to the detected element list              
            }
        }
        else if (element.tag == "EnemyProjectile")      //if the detected element has not parent's transform it follow ennemy projectile pattern
        {
            detectedElements.Add(element);      //We add the projectile to te detected element list           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;          //store temporary the gameobject involve in the collision to easier reference in the function

        if (element.transform != element.transform.root)        //security test to prevent error of the objet as not parent's transform
        {
            if (element.transform.parent.CompareTag("Enemy") && element.tag == "AttackZone")         //Test if we th collision is with an ennemy that attack with an attack zone
            {

                detectedElements.Remove(element.transform.parent.gameObject);                   //Remove this enemy to the detected element list

                if (element.transform.parent.gameObject.GetComponent<BlockHandler>().isParied == true)
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isParied = false;          //if the element were on a blocked state we unblock him since he is leaving the blocking zone
                }
            }
        }
        else if (element.tag == "EnemyProjectile")              //if the detected element has not parent's transform it follow ennemy projectile pattern
        {
            detectedElements.Remove(element);           //We add the projectile to te detected element list

            if (element.GetComponent<BlockHandler>().isParied == true)
            {
                element.GetComponent<BlockHandler>().isParied = false;             //if the element were on a blocked state we unblock him since he is leaving the blocking zone
            }
        }
    }

    void CleanList()                                //Delete empty element in the list (can be due to object destruction) to prevent low evel error
    {
        for (int i = 0; i < detectedElements.Count; i++)
        {
            if (detectedElements[i] == null)
            {
                detectedElements.Remove(detectedElements[i]);           //scan entire detected element list and destroy index that are null
            }
        }
    }
}
