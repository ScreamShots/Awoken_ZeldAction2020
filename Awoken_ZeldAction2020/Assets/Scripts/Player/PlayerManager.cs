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

    [SerializeField]
    GameObject classicRender = null;
    [SerializeField]
    GameObject attackZone = null;
    [SerializeField]
    GameObject collisionDetection = null;
    [SerializeField]
    GameObject hitBox = null;
    [SerializeField]
    GameObject shieldZone = null;
    [SerializeField]
    GameObject paryZone = null;
    [SerializeField]
    GameObject cutsceneRenderer = null;
    //[SerializeField]
    //GameObject playerSound = null;


    public static int fragmentNumber;
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

    public void PlayerInitializeCutScene()
    {
        classicRender.SetActive(false);
        attackZone.SetActive(false);
        collisionDetection.SetActive(false);
        hitBox.SetActive(false);
        shieldZone.SetActive(false);
        paryZone.SetActive(false);
        //playerSound.SetActive(false);
        cutsceneRenderer.SetActive(true);
    }

    public void PlayerEndCutScene()
    {
        classicRender.SetActive(true);
        attackZone.SetActive(true);
        collisionDetection.SetActive(true);
        hitBox.SetActive(true);
        shieldZone.SetActive(true);
        paryZone.SetActive(true);
        //playerSound.SetActive(true);
        cutsceneRenderer.SetActive(false);
    }

}
