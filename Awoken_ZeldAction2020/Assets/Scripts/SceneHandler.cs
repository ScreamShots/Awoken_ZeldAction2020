using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    public int zoneToLoad;

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
        SceneManager.LoadScene(sceneName);
        zoneToLoad = spawnZone;
    }
}
