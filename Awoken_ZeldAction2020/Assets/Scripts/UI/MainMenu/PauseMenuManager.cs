using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public void Resume()
    {
        StartCoroutine(GameManager.Instance.EndGamePause());
    }

    public void MainMenu()
    {
        GameManager.Instance.sceneToLoad = 0;
        GameManager.Instance.areaToLoad = 0;        
        GameManager.Instance.GoToScene();
        StartCoroutine(GameManager.Instance.EndGamePause());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
