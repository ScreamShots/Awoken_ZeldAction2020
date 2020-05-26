using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnigma1Done : EnigmaTool
{

    public static int nbrOfFoodCollected;
    static IsEnigma1Done enigmaInstance;

    protected override void Start()
    {
        enigmaInstance = this;
    }

    public static void CollectFood()
    {
        nbrOfFoodCollected += 1;

        if (nbrOfFoodCollected == 3)
        {
            enigmaInstance.isEnigmaDone = true;
            ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.VegetablesEnd;
        }
    }
}
