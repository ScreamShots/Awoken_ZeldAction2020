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

    public float playerHp;
    public float playerStamina;
    public float playerFury;

    public int currentSceneIndex;
    public int currentAreaIndex;

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

        playerHp = progressionManager.playerHp;
        playerStamina = progressionManager.playerStamina;
        playerFury = progressionManager.playerFury;

        currentAreaIndex = progressionManager.currentAreaIndex;
        currentSceneIndex = progressionManager.currentSceneIndex;
    }

}
