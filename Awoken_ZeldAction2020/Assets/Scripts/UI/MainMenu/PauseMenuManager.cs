using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    #region Variables

    [Header("Menu Pannel")]
    public GameObject pausePannel = null;
    public GameObject optionMenuPannel = null;
    public GameObject graphicMenuPannel = null;
    public GameObject soundMenuPannel = null;
    public GameObject controlMenuPannel = null;

    [Header("All Buttons")]

    public GameObject[] allPauseButton = null;
    public GameObject[] allOptionButton = null;
    public GameObject[] allGraphicButton = null;
    public GameObject[] allSoundButton = null;
    public GameObject[] allControlButton = null;

    public GameObject eventSystem = null;

    #endregion

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

    public void PannelToActivate(string nameOfPannel)
    {
        switch (nameOfPannel)
        {
            case "Menu":
                foreach (GameObject button in allOptionButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allPauseButton)
                {
                    button.SetActive(true);
                }

                pausePannel.SetActive(true);
                optionMenuPannel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(allPauseButton[0]);
                break;

            case "Option":
                foreach (GameObject button in allPauseButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allGraphicButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allSoundButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allControlButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allOptionButton)
                {
                    button.SetActive(true);
                }

                pausePannel.SetActive(false);
                soundMenuPannel.SetActive(false);
                graphicMenuPannel.SetActive(false);
                controlMenuPannel.SetActive(false);
                optionMenuPannel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(allOptionButton[0]);
                break;

            case "Graphic":
                foreach (GameObject button in allOptionButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allGraphicButton)
                {
                    button.SetActive(true);
                }

                graphicMenuPannel.SetActive(true);
                optionMenuPannel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(allGraphicButton[0]);
                break;

            case "Sound":
                foreach (GameObject button in allOptionButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allSoundButton)
                {
                    button.SetActive(true);
                }

                soundMenuPannel.SetActive(true);
                optionMenuPannel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(allSoundButton[0]);
                break;

            case "Control":
                foreach (GameObject button in allOptionButton)
                {
                    button.SetActive(false);
                }
                foreach (GameObject button in allControlButton)
                {
                    button.SetActive(true);
                }

                controlMenuPannel.SetActive(true);
                optionMenuPannel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(allControlButton[0]);
                break;
        }
    }
}
