using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState {Running, Pause, ProjectilePary, MeleePary, LvlFrameTransition, Dialogue}
    public GameState gameState = GameState.Running;
    public static GameManager Instance;
    [Range(0,1)]
    public float timeScaleRatio = 1;
    float l_timeScaleRatio = 1;



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

    private void Update()
    {
        if(l_timeScaleRatio != timeScaleRatio)
        {
            Time.timeScale = 1 * timeScaleRatio;
            l_timeScaleRatio = timeScaleRatio;
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
}
