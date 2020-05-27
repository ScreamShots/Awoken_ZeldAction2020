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

    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FoodPickUp.nbrOfFood == 1 && !firstVegetable)
        {
            firstVegetable = true;
            SoundManager.Instance.PlaySfx(partialResolve, partialResolveVolume);
        }
        else if (FoodPickUp.nbrOfFood == 2 && !secondVegetable)
        {
            secondVegetable = true;
            SoundManager.Instance.PlaySfx(partialResolve, partialResolveVolume);
        }
        else if (FoodPickUp.nbrOfFood == 3 && !thirdVegetable)
        {
            isEnigmaDone = true;

            thirdVegetable = true;
            SoundManager.Instance.PlaySfx(globalResolve, globalResolveVolume);
        }
    }
}
