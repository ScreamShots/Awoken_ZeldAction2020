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
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(EndGamePause);
        GameManager.Instance.blackMelt.MeltIn();        
    }

    public void QuitGame()
    {
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(Application.Quit);
        GameManager.Instance.blackMelt.MeltIn();        
    }

    public void EndGamePause()
    {
        StartCoroutine(GameManager.Instance.EndGamePause());
    }
}
