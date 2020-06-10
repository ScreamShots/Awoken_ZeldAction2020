using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject[] allPauseButton = null;
    public GameObject eventSystem = null;

    private void OnEnable()
    {
        StartCoroutine(FirstSelectedButton());
    }

    IEnumerator FirstSelectedButton()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(allPauseButton[0]);
    }

    public void Resume()
    {
        StartCoroutine(GameManager.Instance.EndGamePause());
    }

    public void MainMenu()
    {
        EventSystem.current.gameObject.SetActive(false);
        GameManager.Instance.sceneToLoad = 0;
        GameManager.Instance.areaToLoad = 0;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(EndGamePause);
        GameManager.Instance.blackMelt.MeltIn();        
    }

    public void QuitGame()
    {
        EventSystem.current.gameObject.SetActive(false);
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(Application.Quit);
        GameManager.Instance.blackMelt.MeltIn();        
    }

    public void EndGamePause()
    {
        StartCoroutine(GameManager.Instance.EndGamePause());
    }

    public void LoadChapter()
    {
        foreach(GameObject button in allPauseButton)
        {
            button.SetActive(false);
        }

        GameManager.Instance.chapterUI.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(GameManager.Instance.chapterUI.backButton);

    }
}
