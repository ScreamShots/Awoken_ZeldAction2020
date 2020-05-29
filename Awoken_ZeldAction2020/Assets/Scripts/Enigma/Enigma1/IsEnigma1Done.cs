using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IsEnigma1Done : EnigmaTool
{
    /*[Space]
    [Header("Engima Sound")]
    public AudioClip partialResolve;
    [Range(0f, 1f)] public float partialResolveVolume = 0.5f;

    public AudioClip globalResolve;
    [Range(0f, 1f)] public float globalResolveVolume = 0.5f;

    private bool firstVegetable = false;
    private bool secondVegetable = false;
    private bool thirdVegetable = false;*/

    public static int nbrOfFoodCollected;
    static IsEnigma1Done enigmaInstance;

    protected override void Start()
    {
        enigmaInstance = this;
    }

    public static void CollectFood()
    {
        //if (FoodPickUp.nbrOfFood == 1 && !firstVegetable)
        nbrOfFoodCollected += 1;

        if (nbrOfFoodCollected == 3)
        {
            //firstVegetable = true;
            enigmaInstance.isEnigmaDone = true;
            //SoundManager.Instance.PlaySfx(partialResolve, partialResolveVolume);
        }
        /*else if (FoodPickUp.nbrOfFood == 2 && !secondVegetable)
        {
            secondVegetable = true;
            SoundManager.Instance.PlaySfx(partialResolve, partialResolveVolume);
        }
        else if (FoodPickUp.nbrOfFood == 3 && !thirdVegetable)
        {
            isEnigmaDone = true;

            thirdVegetable = true;
            SoundManager.Instance.PlaySfx(globalResolve, globalResolveVolume);
            enigmaInstance.isEnigmaDone = true;
            
        }*/
    }
}
