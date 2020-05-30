﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {Running, Pause, ProjectilePary, LvlFrameTransition, Dialogue, Death}
    public GameState gameState = GameState.Running;

    [Header("TimeManagement")]

    UnityEvent onEndSlowTime; 
    float slowTimeTimer = 0;
    bool breakSlowTime = false;
    float currentSlowValue = 0;

    [Header("ParySlow")]

    [SerializeField]
    AnimationCurve slowTimeCurvePary = null;
    [SerializeField]
    float slowTimeLengthPary = 0;

    [Header ("DeathSlow")]

    [SerializeField]
    AnimationCurve slowTimeCurveDeath = null;
    [SerializeField]
    float slowTimeLengthDeath = 0;
    [SerializeField]
    GameOverUI deathUI = null;
    [SerializeField]
    Button[] allGameOverButtons = null;


    [Header("GamePause")]

    [SerializeField]
    GameObject pauseUI = null;
    bool gameIsPause = false;
    GameState lastGameState;
    GameObject lastES = null;

    [Header("BlackMelt")]

    public BlackMelt blackMelt = null;

    //LoadScene

    [HideInInspector]
    public int areaToLoad = 0;
    [HideInInspector]
    public int sceneToLoad = 0;
    [HideInInspector]
    public bool bossRoom = false;

    bool mainMenu = true;

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion        
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        blackMelt.gameObject.SetActive(false);
        if (onEndSlowTime == null) onEndSlowTime = new UnityEvent();

        //Test Transition

        LoadLvlAfterTransition(SceneManager.GetActiveScene(), LoadSceneMode.Single);

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu = true;
        }
        else
        {
            mainMenu = false;
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (gameState == GameState.Running /*&& !mainMenu*/)
            {
                StartGamePause();
            }
            else if(gameState == GameState.Pause)
            {
                StartCoroutine(EndGamePause());
            }
        }
    }

    public void ProjectileParyStart(GameObject target)
    {
        gameState = GameState.ProjectilePary;
        PlayerManager.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().canTakeDmg = false;
        StartCoroutine(SlowTime(slowTimeLengthPary, slowTimeCurvePary, false));
        PlayerManager.Instance.GetComponent<ProjectileParyBehaviour>().LaunchOrientation(target);
    }
    public void ProjectileParyStop()
    {
        gameState = GameState.Running;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().canTakeDmg = true;
        breakSlowTime = true;
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1;
    }

    public void StartGamePause()
    {
        lastGameState = gameState;
        gameState = GameState.Pause;

        if(EventSystem.current != null)
        {
            lastES = EventSystem.current.gameObject;
            EventSystem.current.gameObject.SetActive(false);
        }

        SoundManager.Instance.PauseGame(true);
        gameIsPause = true;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public IEnumerator EndGamePause()
    {
        yield return new WaitForEndOfFrame();

        if (lastGameState == GameState.ProjectilePary)
        {
            Time.timeScale = currentSlowValue;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseUI.SetActive(false);
        if (lastES != null)
        {
            lastES.SetActive(true);
            lastES = null;
        }
        SoundManager.Instance.PauseGame(false);
        gameState = lastGameState;
        gameIsPause = false;
    }

    public void PlayerDeath()
    {
        gameState = GameState.Death;
        onEndSlowTime.AddListener(deathUI.ActiveDeathUI);
        SoundManager.Instance.PlayerDead(true);
        StartCoroutine(SlowTime(slowTimeLengthDeath, slowTimeCurveDeath, true));
    }

    public void OutDeathUI()
    {
    
        foreach(Button button in allGameOverButtons)
        {
            button.gameObject.GetComponent<Image>().enabled = false;
            button.enabled = false;
        }
        blackMelt.gameObject.SetActive(true);
        if (!bossRoom)
        {
            blackMelt.onMeltInEnd.AddListener(PlayerRespawn);
            SoundManager.Instance.PlayerDead(false);
            blackMelt.MeltIn();
        }
        else
        {
            ReloadScene();
            blackMelt.onMeltInEnd.AddListener(PlayerRespawn);
            SoundManager.Instance.PlayerDead(false);
            blackMelt.MeltIn();
        }

    }

    public void PlayerRespawnBoss()
    {
        blackMelt.onMeltInEnd.AddListener(PlayerRespawnBoss);
        blackMelt.MeltOut();
    }

    public void PlayerRespawn()
    {

        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1;
        if (!bossRoom)
        {
            PlayerManager.Instance.GetComponent<PlayerHealthSystem>().Respawn();
        }
        deathUI.DesactiveDeathUI();
        StartCoroutine(TransitionTimeBeforeLaunchBack());
    }

    public void LaunchGameBack()
    {
        blackMelt.gameObject.SetActive(false);
        if(gameState != GameState.Dialogue)
        {
            StartCoroutine(ChangeGameState(GameState.Running));
        }
    }

    [ContextMenu("ReloadScene")]
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToScene()
    {
        SoundManager.Instance.SwitchScene(true);
        if(sceneToLoad == 10)
        {
            bossRoom = true;
        }
        SceneManager.LoadSceneAsync(sceneToLoad);
        blackMelt.onMeltOutEnd.AddListener(LaunchGameBack);
        SceneManager.sceneLoaded += LoadLvlAfterTransition;
    }

    void LoadLvlAfterTransition(Scene scene, LoadSceneMode mode)
    {
        if (LvlManager.Instance != null)
        {
            LvlManager.Instance.InitializeLvl(areaToLoad);
        }
        StartCoroutine(SceneTransitionSecurityTime());
    }

    IEnumerator SceneTransitionSecurityTime()
    {
        yield return new WaitForSeconds(1f);
        blackMelt.MeltOut();
        blackMelt.onMeltOutEnd.AddListener(ActiveBackFXSound);
    }

    void ActiveBackFXSound()
    {
        SoundManager.Instance.SwitchScene(false);
    }

    IEnumerator TransitionTimeBeforeLaunchBack()
    {
        yield return new WaitForSecondsRealtime(2f);
        blackMelt.onMeltOutEnd.AddListener(LaunchGameBack);
        blackMelt.MeltOut();
    }

    public IEnumerator SlowTime(float length, AnimationCurve slowCurve, bool sendMessage)
    {
        breakSlowTime = false;
        slowTimeTimer = 0;
        currentSlowValue = Time.timeScale;

        while(slowTimeTimer < length)
        {
            if (breakSlowTime)
            {
                break;
            }
            if (!gameIsPause)
            {
                Time.timeScale = slowCurve.Evaluate(slowTimeTimer / length);
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                slowTimeTimer += Time.unscaledDeltaTime;
                currentSlowValue = Time.timeScale;
                yield return new WaitForEndOfFrame();

            }
            else
            {
                yield return new WaitUntil(() => gameIsPause);
            }

        }

        if (sendMessage)
        {
            onEndSlowTime.Invoke();
            onEndSlowTime.RemoveAllListeners();
        }
    }

    public IEnumerator ChangeGameState(GameState nextGS)
    {
        yield return new WaitForEndOfFrame();

        gameState = nextGS;
    }
}
