using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use for detect element in minototaure's attack zone
/// </summary>

public class MinototaureDetectZone : MonoBehaviour
{
    #region Variables
    [HideInInspector] public bool isOverlappingPlayer;               //if the enemy is already on player hitbox
    [HideInInspector] public bool isOverlappingShield;               //if the enemy is already on player shield
    [HideInInspector] public GameObject overlappedShield;

    public GameObject minototaure;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;            

        if (minototaure.GetComponent<MinototaureAttack>().lauchAttack)                                                  
        {
            if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")       //if the collided element is a player hitbox
            {
                isOverlappingPlayer = true;     
            }
            if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")   //if the collided element is a player shield zone
            {
                overlappedShield = detectedElement;         //storing the actual shield zone for security test of charge method                                
                isOverlappingShield = true;                 //saying that we are overlapping a player shield zone(so if the enemy dont go out before the next attack if we trigger security check of charge method)
            }
        }
        else
        {
            if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
            {
                isOverlappingPlayer = true;
            }                                                                                                               
            if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")           //Purpose and functionnement still the same as upward
            {
                overlappedShield = detectedElement;
                isOverlappingShield = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;                  //storing the gameobject we are colliding with for easier references

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")           //if we are leaving the player hitbox disable security check
        {
            isOverlappingPlayer = false;
        }
        if (detectedElement.tag == "ShieldZone" && detectedElement.transform.root.gameObject.tag == "Player")       //if we are leaving the player shield zone disable security check
        {
            isOverlappingShield = false;
        }
    }
}
