using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
}
