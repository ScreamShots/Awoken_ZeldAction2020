using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IsEnigma1Done : EnigmaTool
{
    [Space]
    [Header("Engima Sound")]
    public AudioClip partialResolve;
    [Range(0f, 1f)] public float partialResolveVolume = 0.5f;

    public AudioClip globalResolve;
    [Range(0f, 1f)] public float globalResolveVolume = 0.5f;

    private bool firstVegetable = false;
    private bool secondVegetable = false;
    private bool thirdVegetable = false;

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
        }

        if (nbrOfFoodCollected == 1 && !enigmaInstance.firstVegetable)
        {
            enigmaInstance.firstVegetable = true;
            SoundManager.Instance.PlaySfx(enigmaInstance.partialResolve, enigmaInstance.partialResolveVolume);
        }
        else if (nbrOfFoodCollected == 2 && !enigmaInstance.secondVegetable)
        {
            enigmaInstance.secondVegetable = true;
            SoundManager.Instance.PlaySfx(enigmaInstance.partialResolve, enigmaInstance.partialResolveVolume);
        }
        else if (nbrOfFoodCollected == 3 && !enigmaInstance.thirdVegetable)
        {
            enigmaInstance.thirdVegetable = true;
            SoundManager.Instance.PlaySfx(enigmaInstance.globalResolve, enigmaInstance.globalResolveVolume);         
        }
    }
}
