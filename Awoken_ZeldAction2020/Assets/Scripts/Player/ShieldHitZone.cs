using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class ShieldHitZone : MonoBehaviour
{    
    public List<GameObject> detectedElements;
    public bool isActivated;
    private bool l_isActivated;

    private void Update()
    {

        if(detectedElements.Count > 0)
        {
            CleanList();
        }

        if(isActivated && !l_isActivated)
        {
            Debug.Log("Active");
            l_isActivated = true;

            foreach (GameObject element in detectedElements)
            {
                if (element.transform != element.transform.root)
                {
                    if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")
                    {
                        element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = true;
                    }
                }
                else if (element.tag == "EnemyProjectile")
                {
                    element.GetComponent<BlockHandler>().isBlocked = true;
                }
            }
        }
        else if (!isActivated && l_isActivated)
        {
            l_isActivated = false;
            Debug.Log("Desactive");

            foreach (GameObject element in detectedElements)
            {
                if (element.transform != element.transform.root)
                {
                    if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")
                    {
                        element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = false;
                    }
                }
                else if (element.tag == "EnemyProjectile")
                {
                    element.GetComponent<BlockHandler>().isBlocked = false;
                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if(element.transform != element.transform.root)
        {
            if(element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")
            {
                detectedElements.Add(element.transform.parent.gameObject);
                if (isActivated)
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = true;
                }
            }
        }
        else if (element.tag == "EnemyProjectile")
        {
            detectedElements.Add(element);
            if (isActivated)
            {
                element.GetComponent<BlockHandler>().isBlocked = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == "Enemy" && element.tag == "AttackZone")
            {
                detectedElements.Remove(element.transform.parent.gameObject);

                if(element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked == true)
                {
                    element.transform.parent.gameObject.GetComponent<BlockHandler>().isBlocked = false;
                }
            }
        }
        else if (element.tag == "EnemyProjectile")
        {
            detectedElements.Remove(element);

            if (element.GetComponent<BlockHandler>().isBlocked == true)
            {
                element.GetComponent<BlockHandler>().isBlocked = false;
            }
        }
    }

    void CleanList()
    {
        for(int i = 0; i < detectedElements.Count; i++)
        {
            if(detectedElements[i] == null)
            {
                detectedElements.Remove(detectedElements[i]);
            }
        }
    }

}
