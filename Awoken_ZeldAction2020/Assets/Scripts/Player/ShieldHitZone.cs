using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This Script is dedicated to manage the behaviour of each Shield zones appart.
/// </summary>

[RequireComponent(typeof(Collider2D))]
public class ShieldHitZone : MonoBehaviour
{
    #region HideInInspector var Satement

    private bool l_isActivated;                 //This bool is used to precisely deffind ht emoment the player goes from desactivated to activated and inverse

    #endregion

    #region SerializeField var Satement

    [Header("State")]

    public bool isActivated;

    [Header("Detection")]

    public List<GameObject> detectedElements;
    

    #endregion
    

    private void Update()
    {
        if(detectedElements.Count > 0)
        {
            CleanList();                    //Delete empty element in the list (can be due to object destruction) to prevent low evel error
        }

        if(isActivated && !l_isActivated)
        {
            BlockDetectedElement();         //Make Detected element state become Blocked=true (see BlockHandler Script on everything that can be blocked)
        }
        else if (!isActivated && l_isActivated)
        {
            UnBlockDetectedElement();       //Make Detected element state become Blocked=false (see BlockHandler Script on everything that can be blocked)
        }
    }   

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;          //store temporary the gameobject involve in the collision to easier reference in the function

        if(element.transform != element.transform.root)     //security test to prevent error of the objet as not parent's transform
        {
            if(element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")      //Test if we th collision is with an ennemy that attack with an attack zone
            {
                detectedElements.Add(element.transform.parent.gameObject);                  //Add this enemy to the detected element list

                if (isActivated)                                                        
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = true;          //If the shield is activated Make Detected element state become Blocked=true (see BlockHandler Script on everything that can be blocked)
                }
            }
        }
        else if (element.tag == "EnemyProjectile")      //if the detected element has not parent's transform it follow ennemy projectile pattern
        {
            detectedElements.Add(element);      //We add the projectile to te detected element list

            if (isActivated)
            {
                element.GetComponent<BlockHandler>().isBlocked = true;  //If the shield is activated Make Detected element state become Blocked=true (see BlockHandler Script on everything that can be blocked)
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;          //store temporary the gameobject involve in the collision to easier reference in the function

        if (element.transform != element.transform.root)        //security test to prevent error of the objet as not parent's transform
        {
            if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")         //Test if we th collision is with an ennemy that attack with an attack zone
            {
                detectedElements.Remove(element.transform.parent.gameObject);                   //Remove this enemy to the detected element list

                if (element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked == true)        
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = false;          //if the element were on a blocked state we unblock him since he is leaving the blocking zone
                }
            }
        }
        else if (element.tag == "EnemyProjectile")              //if the detected element has not parent's transform it follow ennemy projectile pattern
        {
            detectedElements.Remove(element);           //We add the projectile to te detected element list

            if (element.GetComponent<BlockHandler>().isBlocked == true)
            {
                element.GetComponent<BlockHandler>().isBlocked = false;             //if the element were on a blocked state we unblock him since he is leaving the blocking zone
            }
        }
    }

    void CleanList()                                //Delete empty element in the list (can be due to object destruction) to prevent low evel error
    {
        for(int i = 0; i < detectedElements.Count; i++)
        {
            if(detectedElements[i] == null)
            {
                detectedElements.Remove(detectedElements[i]);           //scan entire detected element list and destroy index that are null
            }
        }
    }

    void BlockDetectedElement()                     //On Shield activation, pass every detected element on blocked state
    {
        l_isActivated = true;                       //bool to trigger the function only once when the zone pass from desactivated to activated

        foreach (GameObject element in detectedElements)        //loop go go on every element in detected element list
        {
            if (element.transform != element.transform.root)            //security test to prevent gameobject with no parent to cause low level error
            {
                if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")         
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = true;          //if this element is an enemy with an attaque zone we set his blocked state on true
                }
            }
            else if (element.tag == "EnemyProjectile")                  //if the element has no parent's transfom it's a projectile
            {
                element.GetComponent<BlockHandler>().isBlocked = true;  //we set the projectile's block state on true;
            }
        }
    }

    void UnBlockDetectedElement()                   //On Shield desactivation, pass every detected element state on unblocked state
    {
        l_isActivated = false;                      //security to run the function only once when the shield zone goes from activate state to desactivate state

        foreach (GameObject element in detectedElements)            //Going true all deteceted element with this loop
        {
            if (element.transform != element.transform.root)        //security test to prevent gameobject with no parent to cause low level error
            {
                if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = false;     //if this element is an enemy with an attaque zone we set his blocked state on flase
                }
            }
            else if (element.tag == "EnemyProjectile")              //if the element has no parent's transfom it's a projectile
            {
                element.GetComponent<BlockHandler>().isBlocked = false;             //we set the projectile's block state on false;
            }
        }
    }

}
