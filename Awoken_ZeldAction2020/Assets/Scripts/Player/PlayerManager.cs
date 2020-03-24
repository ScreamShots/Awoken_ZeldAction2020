using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This script is a HighLevel Reference for all functionality of the player
/// It's a static instance you can call using PlayerManager.Instance
/// </summary>

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("1 player Delete (can't be more than one player in the scene)");
        }
        #endregion
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0.001f;
            }
            else
            {
                Time.timeScale = 1;
                
            }
        }
    }
}
