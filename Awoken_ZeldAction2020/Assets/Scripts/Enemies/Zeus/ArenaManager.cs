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
    BossState2Bis bossState2BisScript;
    BossState3 bossState3Script;
    EnemySpawner enemySpawnerAScript;
    EnemySpawner enemySpawnerBScript;

    private int ennemisToKillToOpenGate;
    private int currentEnnemisKilled;

    private bool exitGateOpen;

    private bool dialogueStartBattleRunning = false;
    private bool dialogueStartBattleFinish = false;
    private bool dialogue1Running = false;
    private bool dialogue1Finish = false;
    private bool dialogue2Running = false;
    private bool dialogue2Finish = false;
    private bool dialogue3Running = false;
    private bool dialogue3Finish = false;

    #endregion

    #region Inspector Settings
    [Header("Requiered Elements")]
    public GameObject arenaZone;
    public GameObject bossUI;

    [Header("Gates")]
    public GameObject gateEnter;
    public GameObject gaterExit;

    [Header("Gates")]
    public CSManagerBossEndState1 bossEndState1Script;
    public CSManagerBossEndState2 bossEndState2Script;
    public CSManagerBossEndState3 bossEndState3Script;
    public CSManagerBossStartBattle bossStartBattleScript;

    #endregion

    PlayerCharge playerChargeScript;
    ProjectileParyBehaviour playerParyScript;

    void Start()
    {
        transitionArenaScript = arenaZone.GetComponent<TransitionArena>();
        bossState2BisScript = BossManager.Instance.GetComponent<BossState2Bis>();
        bossState3Script = BossManager.Instance.GetComponent<BossState3>();
        playerChargeScript = PlayerManager.Instance.GetComponent<PlayerCharge>();
        playerParyScript = PlayerManager.Instance.GetComponent<ProjectileParyBehaviour>();

        ennemisToKillToOpenGate = bossState3Script.ennemisToKillToOpenGate;
    }

    void Update()
    {
        LauchDialogue();

        if (bossState2BisScript.spawnerExist && bossState3Script.CounterIsActivate)                              //when pattern 1 of state 3 actived, get the two spawners and count how many ennemis dead
        {
            enemySpawnerAScript = bossState2BisScript.spawnerA.GetComponent<EnemySpawner>();
            enemySpawnerBScript = bossState2BisScript.spawnerB.GetComponent<EnemySpawner>();
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

    void LauchDialogue()
    {
        if (transitionArenaScript.playerInZone)
        {
            if (!dialogueStartBattleRunning)
            {
                if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.charge)
                {
                    playerChargeScript.FastEndCharge();
                }
                else if(PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.block)
                {
                    playerParyScript.StopOrientation();
                }

                dialogueStartBattleRunning = true;
                bossStartBattleScript.StartCutScene();
            }
        }
        if (dialogueStartBattleRunning)
        {
            if (!bossStartBattleScript.inDialogue && !dialogueStartBattleFinish)
            {
                dialogueStartBattleFinish = true;
                BossManager.Instance.canStartBossFight = true;
            }
        }

        if (BossManager.Instance.currentHp <= 470)
        {
            if (!dialogue1Running)
            {
                if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.charge)
                {
                    playerChargeScript.FastEndCharge();
                }
                else if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.block)
                {
                    playerParyScript.StopOrientation();
                }

                dialogue1Running = true;
                bossEndState1Script.StartCutScene();
            }
            else
            {
                if (!bossEndState1Script.inDialogue && !dialogue1Finish)
                {
                    dialogue1Finish = true;
                    BossManager.Instance.dialogueState1Finish = true;
                }
            }
        }

        if (BossManager.Instance.currentHp <= 200)
        {
            if (!dialogue2Running)
            {
                if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.charge)
                {
                    playerChargeScript.FastEndCharge();
                }
                else if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.block)
                {
                    playerParyScript.StopOrientation();
                }

                dialogue2Running = true;
                bossEndState2Script.StartCutScene();
            }
            else
            {
                if (!bossEndState2Script.inDialogue && !dialogue2Finish)
                {
                    dialogue2Finish = true;
                    BossManager.Instance.dialogueState2Finish = true;
                }
            }
        }

        if (BossManager.Instance.currentHp <= 50)
        {
            if (!dialogue3Running)
            {
                if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.charge)
                {
                    playerChargeScript.FastEndCharge();
                }
                else if (PlayerStatusManager.Instance.currentState == PlayerStatusManager.State.block)
                {
                    playerParyScript.StopOrientation();
                }

                dialogue3Running = true;
                bossEndState3Script.StartCutScene();
            }
            else
            {
                if (!bossEndState3Script.inDialogue && !dialogue3Finish)
                {
                    dialogue3Finish = true;
                    BossManager.Instance.dialogueState3Finish = true;
                }
            }
        }
    }
}
