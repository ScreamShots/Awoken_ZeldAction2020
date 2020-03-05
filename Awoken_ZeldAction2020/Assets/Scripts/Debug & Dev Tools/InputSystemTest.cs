using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystemTest : MonoBehaviour
{
    /// <summary>
    /// Made by Rémi Sécher
    /// This script is used to test that all the maped input work
    /// You can new one if u map new input
    /// </summary>
    /// 

    [SerializeField] private bool showAxis;

    void Update()
    {  
        
        if(showAxis == true)
        {
            Debug.Log(Input.GetAxis("HorizontalAxis"));
            Debug.Log(Input.GetAxis("VerticalAxis"));
        }        

        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack");
        }
        if (Input.GetButtonDown("Block"))
        {
            Debug.Log("Block");
        }
        if (Input.GetButtonDown("Interraction"))
        {
            Debug.Log("Interraction");
        }
        if (Input.GetButtonDown("SwapMod"))
        {
            Debug.Log("SwapMod");
        }
        if (Input.GetButtonDown("Spin"))
        {
            Debug.Log("Spin");
        }
        if (Input.GetButtonDown("Charge"))
        {
            Debug.Log("Charge");
        }
        if (Input.GetButtonDown("MenuValidate"))
        {
            Debug.Log("MenuValidate");
        }
        if (Input.GetButtonDown("MenuReturn"))
        {
            Debug.Log("MenuReturn");
        }
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("Pause");
        }
    }
}
