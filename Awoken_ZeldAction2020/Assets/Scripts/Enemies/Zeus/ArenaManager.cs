﻿using System.Collections;
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
    DialogueTrigger dialogueTriggerScript1;
    DialogueTrigger dialogueTriggerScript2;

    private int ennemisToKillToOpenGate;
    private int currentEnnemisKilled;

    private bool exitGateOpen;

    private bool dialogue1Running = false;
    private bool dialogue1Finish = false;
    private bool dialogue2Running = false;
    private bool dialogue2Finish = false;

    #endregion

    #region Inspector Settings
    [Header("Requiered Elements")]
    public GameObject arenaZone;
    public GameObject bossUI;

    [Header("Gates")]
    public GameObject gateEnter;
    public GameObject gaterExit;

    [Header("Dialogues")]
    public GameObject dialogueEndState1;
    public GameObject dialogueEndState2;

    #endregion

    void Start()
    {
        transitionArenaScript = arenaZone.GetComponent<TransitionArena>();
        bossState3Script = BossManager.Instance.GetComponent<BossState3>();
        dialogueTriggerScript1 = dialogueEndState1.GetComponent<DialogueTrigger>();
        dialogueTriggerScript2 = dialogueEndState2.GetComponent<DialogueTrigger>();

        ennemisToKillToOpenGate = bossState3Script.ennemisToKillToOpenGate;
    }

    void Update()
    {
        LauchDialogue();

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

    void LauchDialogue()
    {
        if (BossManager.Instance.currentHp <= 230)
        {
            if (!dialogue1Running)
            {
                dialogue1Running = true;
                dialogueTriggerScript1.StartDialogue();
            }
            else
            {
                if (dialogueTriggerScript1.dialogueEnded && !dialogue1Finish)
                {
                    dialogue1Finish = true;
                    BossManager.Instance.dialogueState1Finish = true;
                }
            }
        }
        
        if (BossManager.Instance.currentHp <= 50)
        {
            if (!dialogue2Running)
            {
                dialogue2Running = true;
                dialogueTriggerScript2.StartDialogue();
            }
            else
            {
                if (dialogueTriggerScript2.dialogueEnded && !dialogue2Finish)
                {
                    dialogue2Finish = true;
                    BossManager.Instance.dialogueState2Finish = true;
                }
            }
        }
    }
}
