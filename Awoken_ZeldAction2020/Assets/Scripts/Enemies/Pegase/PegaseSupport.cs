using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve support of Pegase for enemies
/// </summary>

public class PegaseSupport : MonoBehaviour
{
    #region Variables
    EnemyHealthSystem pegaseHealthScript;
    
    private GameObject trailInstance;
    private bool trailExist;
    #endregion

    #region Inspector Settings
    [Header("Target Tag Selection")]
    [SerializeField] private string targetedElement = null;

    public GameObject ShieldTrail;

    [Header("Element in Zone")]
    [Space]

    public List<GameObject> detectedElement;
    #endregion

    private void Start()
    {
        pegaseHealthScript = gameObject.transform.root.GetComponent<EnemyHealthSystem>();
    }

    private void Update()
    {
        if(detectedElement.Count > 0)
        {
            //TrailShield();

            for (int i = 0; i < detectedElement.Count; i++)
            {
                if (detectedElement[i].gameObject != null && pegaseHealthScript.currentHp > 0)
                {
                    detectedElement[i].gameObject.GetComponent<EnemyHealthSystem>().ProtectByPegase = true;                             //if a element is in the liste, he's can't take damage
                }
                else if (detectedElement[i].gameObject == null)                                                                     //remove a element of the list if he's = null 
                {
                    detectedElement.Remove(detectedElement[i].gameObject);
                }
                
                if(pegaseHealthScript.currentHp <= 0)                                                                               //when Pegase die, the ennemies can take damage again
                {
                    detectedElement[i].gameObject.GetComponent<EnemyHealthSystem>().ProtectByPegase = false;
                }
            }
        }
        /*else
        {
            trailExist = false;
            Destroy(trailInstance);
        }*/
    }

    void TrailShield()
    {
        if (!trailExist)
        {
            trailExist = true;
            trailInstance = Instantiate(ShieldTrail, transform.position, ShieldTrail.transform.rotation);
            trailInstance.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox" && element != null)         
            {
                for(int i = 0; i < detectedElement.Count; i++)                                                                        //for not add twice same element in the list
                {
                    if (detectedElement[i].gameObject.transform.root.name == element.transform.root.name)
                    {
                        detectedElement.Remove(element.transform.parent.gameObject);
                    }
                }

                detectedElement.Add(element.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
