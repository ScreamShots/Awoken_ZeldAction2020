using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// This script is here only to define the pressure plate behavior
/// </summary>
public abstract class PressurePlateBehavior : MonoBehaviour
{
    [SerializeField] protected  bool isPressed;
    [SerializeField] protected List<GameObject> elementsOnPlate;
    protected virtual void OnTriggerEnter2D(Collider2D other) //Looks if the Player enters the pressure plate
    {
        if (other.gameObject.transform.root.CompareTag("Player"))
        {
            isPressed = true;
            elementsOnPlate.Add(other.transform.root.gameObject);
        }
        else if (other.gameObject.tag == "ObjectToMove")
        {
            isPressed = true;
            elementsOnPlate.Add(other.gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) //Looks if the player leaves the pressure plate
    {
        if (other.gameObject.transform.root.CompareTag("Player") || other.gameObject.tag == "ObjectToMove")
        {
            isPressed = false;
            elementsOnPlate.Remove(other.transform.root.gameObject);
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D other) //Looks if the player is on the pressure plate and press a specific input
    {
        if (other.gameObject.transform.root.CompareTag("Player") && Input.GetButtonDown("Interraction"))
        {
            isPressed = true;
        }
    }
}
