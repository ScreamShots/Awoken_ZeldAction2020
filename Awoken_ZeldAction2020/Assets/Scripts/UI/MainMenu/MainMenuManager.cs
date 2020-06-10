using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    Button continueButton = null;
    [SerializeField]
    VideoPlayer introVideoPlayer = null;
    [SerializeField]
    GameObject eventSystem = null;
    [SerializeField]
    GameObject blackScreen = null;

    [Header("Menu Pannel")]
    public GameObject mainMenuPannel = null;
    public GameObject optionMenuPannel = null;
    public GameObject graphicMenuPannel = null;
    public GameObject soundMenuPannel = null;
    public GameObject controlMenuPannel = null;

    [Header("All Buttons")]
    public GameObject[] allMenuButton = null;
    public GameObject[] allOptionButton = null;
    public GameObject[] allGraphicButton = null;
    public GameObject[] allSoundButton = null;
    public GameObject[] allControlButton = null;

    private void Awake()
    {
        introVideoPlayer.loopPointReached += OnEndIntro;
        eventSystem.gameObject.SetActive(false);
        blackScreen.SetActive(true);
    }

    private void Start()
    {
        if (!SaveSystem.FilePresencePing())
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    private void Update()
    {
        if (Input.anyKey && introVideoPlayer.isPlaying)
        {
            OnEndIntro(introVideoPlayer);
        }
    }

    public void QuitGame()
    {
        eventSystem.SetActive(false);
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(Application.Quit); ;
        GameManager.Instance.blackMelt.MeltIn();
    }



    public void LaunchNewGame()
    {

        eventSystem.SetActive(false);
        ProgressionManager.Instance.ResetProgressionManager();
        GameManager.Instance.sceneToLoad = 1;
        GameManager.Instance.areaToLoad = 0;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();
    }

    /*IEnumerator NewGame()
    {
        eventSystem.SetActive(false);
        GameObject progressionManagerHolder = ProgressionManager.Instance.gameObject;
        Destroy(ProgressionManager.Instance);
        yield return new WaitForEndOfFrame();
        progressionManagerHolder.AddComponent<ProgressionManager>();
        GameManager.Instance.sceneToLoad = 1;
        GameManager.Instance.areaToLoad = 0;
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.MeltIn();
    }*/

    public void Continue()
    {
        eventSystem.SetActive(false);
        ProgressionManager.Instance.LoadTheProgression();        
    }

    public void OnEndIntro(VideoPlayer player)
    {
        SoundManager.Instance.PauseGame(false);
        blackScreen.SetActive(false);
        player.Stop();
        eventSystem.gameObject.SetActive(true);

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
                foreach (GameObject button in allMenuButton)
                {
                    button.SetActive(true);
                }

                mainMenuPannel.SetActive(true);
                optionMenuPannel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(allMenuButton[0]);
                break;

            case "Option":
                foreach (GameObject button in allMenuButton)
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

                mainMenuPannel.SetActive(false);
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
