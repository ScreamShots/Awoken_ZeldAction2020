using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    Button continueButton = null;

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

    public void QuitGame()
    {
        Application.Quit();
    }



    public void LaunchNewGame()
    {

        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        GameObject progressionManagerHolder = ProgressionManager.Instance.gameObject;
        Destroy(ProgressionManager.Instance);
        yield return new WaitForEndOfFrame();
        progressionManagerHolder.AddComponent<ProgressionManager>();
        GameManager.Instance.sceneToLoad = 1;
        GameManager.Instance.areaToLoad = 0;
        GameManager.Instance.GoToScene();
    }

    public void Continue()
    {
        ProgressionManager.Instance.LoadTheProgression();        
    }
}
