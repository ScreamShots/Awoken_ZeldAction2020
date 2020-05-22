using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {Running, Pause, ProjectilePary, LvlFrameTransition, Dialogue, Death}
    public GameState gameState = GameState.Running;

    [Header("DevTools")]

    [SerializeField] [Range(0,1)]
    float timeScaleRatio = 1;
    float l_timeScaleRatio = 1;
    [SerializeField]
    float currentTimeScale = 0;

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
    GameObject deathUI = null;
    [SerializeField]
    GameObject continueButton = null;

    [Header("GamePause")]

    [SerializeField]
    GameObject pauseUI = null;
    bool gameIsPause = false;
    GameState lastGameState;

    [Header("Global UI")]

    [SerializeField]
    EventSystem eventSystemUI = null;



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
            Debug.Log("1 GameManager Deleted (can't be more than one GameManager in the scene)");
        }
        #endregion        
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        deathUI.SetActive(false);
        eventSystemUI.gameObject.SetActive(false);
        if (onEndSlowTime == null) onEndSlowTime = new UnityEvent();
    }

    private void Update()
    {
        currentTimeScale = Time.timeScale;

        if(l_timeScaleRatio != timeScaleRatio)
        {
            Time.timeScale = 1 * timeScaleRatio;
            l_timeScaleRatio = timeScaleRatio;
        }


        if (Input.GetButtonDown("Pause"))
        {
            if (gameState != GameState.Pause)
            {
                StartGamePause();
            }
            else if(gameState == GameState.Pause)
            {
                EndGamePause();
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
        gameIsPause = true;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void EndGamePause()
    {
        gameState = lastGameState;
        gameIsPause = false;

        if(lastGameState == GameState.ProjectilePary)
        {
            Time.timeScale = currentSlowValue;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseUI.SetActive(false);
    }

    public void PlayerDeath()
    {
        gameState = GameState.Death;
        onEndSlowTime.AddListener(PoPDeathScreen);
        StartCoroutine(SlowTime(slowTimeLengthDeath, slowTimeCurveDeath, true));
    }

    void PoPDeathScreen()
    {
        eventSystemUI.firstSelectedGameObject = continueButton;
        deathUI.SetActive(true);
        eventSystemUI.gameObject.SetActive(true);
    }

    public void PlayerRespawn()
    {
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().Respawn();
        deathUI.SetActive(false);
        eventSystemUI.gameObject.SetActive(false);
        StartCoroutine(ChangeGameState(GameState.Running));
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
        }
    }

    public IEnumerator ChangeGameState(GameState nextGS)
    {
        yield return new WaitForEndOfFrame();

        gameState = nextGS;
    }
}
