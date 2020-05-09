﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    public int zoneToLoad;
    public float playerHp;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SceneTransition(string sceneName, int spawnZone)
    {
        playerHp = PlayerManager.Instance.gameObject.GetComponent<PlayerHealthSystem>().currentHp;
        SceneManager.LoadScene(sceneName);
        zoneToLoad = spawnZone;
        PlayerManager.Instance.gameObject.GetComponent<PlayerHealthSystem>().currentHp = playerHp;
    }
}
