using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState {Running, Pause, ProjectilePary, MeleePary, LvlFrameTransition}
    public GameState gameState = GameState.Running;
    public static GameManager Instance;
    [Range(0,1)]
    public float timeScaleRatio = 1;
    float l_timeScaleRatio = 1;
    public GameObject PauseMenu;
    public GameObject player;
    public AreaManager[] allArea;
    public HPBar hpBar;
    public StaminaBar staminaBar;



    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("1 GameManager Deleted (can't be more than one GameManager in the scene)");
        }
        #endregion        
    }

    private void Update()
    {
        if(l_timeScaleRatio != timeScaleRatio)
        {
            Time.timeScale = 1 * timeScaleRatio;
            l_timeScaleRatio = timeScaleRatio;
        }

        if(gameState == GameState.Running)
        {
            if (Input.GetButtonDown("Pause"))
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                gameState = GameState.Pause;
            }
        }
        else if(gameState == GameState.Pause)
        {
            if (Input.GetButtonDown("Pause"))
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                gameState = GameState.Running;
            }
        }
    }

    public void ProjectileParyStart(GameObject target)
    {
        gameState = GameState.ProjectilePary;
        Time.timeScale = 0;
        PlayerManager.Instance.GetComponent<ProjectileParyBehaviour>().LaunchOrientation(target);
    }
    public void ProjectileParyStop()
    {
        gameState = GameState.Running;
        Time.timeScale = 1 * timeScaleRatio;
    }

    public void StartCreate(Vector3 startPos)
    {
        StartCoroutine(CreatePlayer( startPos));
    }

   public IEnumerator CreatePlayer(Vector3 startPos)
    {

        yield return new WaitForSeconds(0.1f);
        Instantiate(player, startPos, Quaternion.identity);
        foreach(AreaManager area in allArea)
        {
            area.thisAreaCam.Follow = PlayerManager.Instance.gameObject.transform;
        }
        hpBar.playerHpSystem = PlayerManager.Instance.gameObject.GetComponent<PlayerHealthSystem>();
        staminaBar.playerShieldSystem = PlayerManager.Instance.gameObject.GetComponent<PlayerShield>();
        LvlManager.Instance.UnloadCurrentArea();
        LvlManager.Instance.ResetArea();
    }
}
