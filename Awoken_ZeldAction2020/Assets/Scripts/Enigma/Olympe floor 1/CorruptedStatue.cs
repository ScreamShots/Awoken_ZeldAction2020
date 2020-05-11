using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedStatue : EnigmaTool
{
    [SerializeField]
    private GameObject statue = null;
    [SerializeField]
    private AreaManager area7 = null;

    void Awake()
    {
        statue.SetActive(false);
    }
    protected override void Start()
    {

    }

    void Update()
    {
        FinishEnigma();
        StatueCorruption();
    }

    void FinishEnigma()
    {
        if(area7.allEnemyAreDead == true)
        {
            isEnigmaDone = true;
        }
    }

    void StatueCorruption()
    {
        if(isEnigmaDone == true)
        {
            statue.SetActive(true);
        }
    }

}
