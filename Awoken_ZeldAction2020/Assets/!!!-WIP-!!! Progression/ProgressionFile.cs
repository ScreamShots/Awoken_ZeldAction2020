using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressionFile
{
    public int progressionTimeLineValue;

    public bool[] r1VegetablesValue;

    public string[] playerCapacityKey;
    public bool[] playerCapacityValue;

    public float maxHp;
    public float playerHp;
    public float maxStamina;
    public float playerStamina;
    public float playerFury;

    public bool[] r1Fragments;
    public bool[] r2Fragments;
    public bool[] r3Fragments;

    public bool[] f1Fragments;
    public bool[] f2Fragments;
    public bool[] f3Fragments;

    public int currentSceneIndex;
    public int currentAreaIndex;

    public int availableFragments;
    public int totalFragemnts;

    public int lvlOfHealthUpgrade;
    public int lvlOfShieldUpgrade;

    public ProgressionFile (ProgressionManager progressionManager)
    {
        progressionTimeLineValue = (int)progressionManager.thisSessionTimeLine;

        r1VegetablesValue = new bool[3];
        
        for(int i = 0; i < r1VegetablesValue.Length; i++)
        {
            r1VegetablesValue[i] = progressionManager.R1Vegetables[i+1];
        }

        playerCapacityKey = new string[4];
        playerCapacityValue = new bool[4];

        playerCapacityKey[0] = "Shield";
        playerCapacityKey[1] = "Block";
        playerCapacityKey[2] = "Pary";
        playerCapacityKey[3] = "Charge";

        for(int i = 0; i < playerCapacityValue.Length; i++)
        {
            playerCapacityValue[i] = progressionManager.PlayerCapacity[playerCapacityKey[i]];
        }

        maxHp = progressionManager.maxHp;
        playerHp = progressionManager.playerHp;
        maxStamina = progressionManager.maxStamina;
        playerStamina = progressionManager.playerStamina;
        playerFury = progressionManager.playerFury;

        r1Fragments = new bool[progressionManager.R1Fragments.Count];
        r2Fragments = new bool[progressionManager.R2Fragments.Count]; 
        r3Fragments = new bool[progressionManager.R3Fragments.Count];

        f1Fragments = new bool[progressionManager.F1Fragments.Count];
        f2Fragments = new bool[progressionManager.F2Fragments.Count];
        f3Fragments = new bool[progressionManager.F3Fragments.Count];

        for(int i =0; i < r1Fragments.Length; i++)
        {
            r1Fragments[i] = progressionManager.R1Fragments[i];
        }
        for (int i = 0; i < r2Fragments.Length; i++)
        {
            r2Fragments[i] = progressionManager.R2Fragments[i];
        }
        for (int i = 0; i < r3Fragments.Length; i++)
        {
            r3Fragments[i] = progressionManager.R3Fragments[i];
        }
        for (int i = 0; i < f1Fragments.Length; i++)
        {
            f1Fragments[i] = progressionManager.F1Fragments[i];
        }
        for (int i = 0; i < f2Fragments.Length; i++)
        {
            f2Fragments[i] = progressionManager.F2Fragments[i];
        }
        for (int i = 0; i < f3Fragments.Length; i++)
        {
            f3Fragments[i] = progressionManager.F3Fragments[i];
        }

        currentAreaIndex = progressionManager.currentAreaIndex;
        currentSceneIndex = progressionManager.currentSceneIndex;

        availableFragments = progressionManager.availableFragments;
        totalFragemnts = progressionManager.totalFragments;

        lvlOfHealthUpgrade = progressionManager.lvlOfHealUpgrade;
        lvlOfShieldUpgrade = progressionManager.lvlOfShieldUpgrade;
    }

}
