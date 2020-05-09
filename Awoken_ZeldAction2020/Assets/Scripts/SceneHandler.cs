using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    public int zoneToLoad;
    [HideInInspector]
    public float playerHp = 100;
    public bool alreadyLoadAScene;

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

        if(PlayerManager.Instance != null)
        {
            alreadyLoadAScene = true;
            playerHp = PlayerManager.Instance.gameObject.GetComponent<PlayerHealthSystem>().currentHp;
        }

        SceneManager.LoadScene(sceneName);
        zoneToLoad = spawnZone;
    }
}
