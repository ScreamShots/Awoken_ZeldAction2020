using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LoadChapterUI : MonoBehaviour
{
    [System.Serializable]
   public struct ChapterDisplay
    {
        public Sprite chapterScreen;
        public string chapterName;
    }

    [SerializeField]
    ChapterDisplay[] allChapters = null;

    [HideInInspector]
    public int displayIndex = 0;

    [SerializeField]
    Image displayChapterScreen = null;
    [SerializeField]
    TextMeshProUGUI displayChapterName = null;

    public GameObject backButton = null;

    private void OnEnable()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetButtonDown("LoadChapterRight"))
        {
            if(displayIndex < allChapters.Length - 1)
            {
                displayIndex += 1;
                UpdateUI();
            }
        }
        else if (Input.GetButtonDown("LoadChapterLeft"))
        {
            if (displayIndex > 0)
            {
                displayIndex -= 1;
                UpdateUI();
            }
        }
    }

    void UpdateUI()
    {
        displayChapterScreen.sprite = allChapters[displayIndex].chapterScreen;
        displayChapterName.text = (displayIndex + 1) + " - " + allChapters[displayIndex].chapterName;
    }

    public void Back()
    {
        PauseMenuManager pauseManager = GameManager.Instance.pauseUI.GetComponent<PauseMenuManager>();
        
        foreach(GameObject button in pauseManager.allPauseButton)
        {
            button.SetActive(true);
        }

        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(pauseManager.allPauseButton[0]);
        }

        displayIndex = 0;

        gameObject.SetActive(false);
    }

    public void Load()
    {
        EventSystem.current.gameObject.SetActive(false);
        ProgressionManager.Instance.LoadChapterSetup(displayIndex);
        ProgressionManager.Instance.SaveTheProgression();
        GameManager.Instance.blackMelt.gameObject.SetActive(true);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.GoToScene);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(GameManager.Instance.pauseUI.GetComponent<PauseMenuManager>().EndGamePause);
        GameManager.Instance.blackMelt.onMeltInEnd.AddListener(Back);
        GameManager.Instance.blackMelt.MeltIn();
    }
}
