using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnigma1Done : EnigmaTool
{

    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FoodPickUp.nbrOfFood == 3)
        {
            isEnigmaDone = true;
        }
    }
}
