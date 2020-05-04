using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use to start Boss fight and end it
/// </summary>

public class ArenaManager : MonoBehaviour
{
    #region Variables
    TransitionArena transitionArenaScript;
    BossState3 bossState3Script;
    EnemySpawner enemySpawnerAScript;
    EnemySpawner enemySpawnerBScript;

    private int ennemisToKillToOpenGate;
    private int currentEnnemisKilled;

    private bool exitGateOpen;

    #endregion

    #region Inspector Settings
    [Header("Requiered Elements")]
    public GameObject arenaZone;
    public GameObject bossUI;

    [Header("gates")]
    public GameObject gateEnter;
    public GameObject gaterExit;

    #endregion

    void Start()
    {
        transitionArenaScript = arenaZone.GetComponent<TransitionArena>();
        bossState3Script = BossManager.Instance.GetComponent<BossState3>();

        ennemisToKillToOpenGate = bossState3Script.ennemisToKillToOpenGate;
    }

    void Update()
    {
        if (transitionArenaScript.playerInZone && !exitGateOpen)                    //To close gate after player when he enter in arena
        {
            BossManager.Instance.canStartBossFight = true;
            gateEnter.SetActive(true);
            gaterExit.SetActive(true);

            bossUI.SetActive(true);
        }

        if (bossState3Script.spawnerExist)                                                   //when pattern 1 of state 3 actived, get the two spawners and count how many ennemis dead
        {
            enemySpawnerAScript = bossState3Script.spawnerA.GetComponent<EnemySpawner>();
            enemySpawnerBScript = bossState3Script.spawnerB.GetComponent<EnemySpawner>();
            currentEnnemisKilled = enemySpawnerAScript.enemiesDead + enemySpawnerBScript.enemiesDead;
        }

        if (currentEnnemisKilled >= ennemisToKillToOpenGate && !exitGateOpen)                                    //if ennemis dead are enough, open the exit gate for kill Boss
        {
            exitGateOpen = true;

            gateEnter.SetActive(false);
            gaterExit.SetActive(false);

            enemySpawnerAScript.spawnEnable = false;
            enemySpawnerBScript.spawnEnable = false;
            
            enemySpawnerAScript.KillAllEnnemies();
            enemySpawnerBScript.KillAllEnnemies();
        }

        if (BossManager.Instance == null)
        {
            bossUI.SetActive(false);
        }
    }
}
