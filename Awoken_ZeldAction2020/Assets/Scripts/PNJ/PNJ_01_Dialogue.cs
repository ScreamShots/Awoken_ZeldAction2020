using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJ_01_Dialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject pressX = null;
    [SerializeField]
    private GameObject dialogue01 = null;
    private bool playerIsHere;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            pressX.SetActive(true);
            playerIsHere = true;
        }
    }

    void Update()
    {
        if (playerIsHere == true && Input.GetButtonDown("Interraction") && dialogue01.activeInHierarchy == false)
        {
            dialogue01.SetActive(true);
        }
       else if (playerIsHere == true && Input.GetButtonDown("Interraction") && dialogue01.activeInHierarchy == true)
       {
            dialogue01.SetActive(false);
       }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            pressX.SetActive(false);
            dialogue01.SetActive(false);
            playerIsHere = false;
        }
    }
}
