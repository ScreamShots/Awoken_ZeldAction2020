using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du cube à pousser
/// </summary>

public class SoundCubePushed : MonoBehaviour
{
    private CubeToPush scriptCube;

    private bool l_playerPushing;

    // Start is called before the first frame update
    void Start()
    {
        scriptCube = GetComponentInParent<CubeToPush>();
    }

    // Update is called once per frame
    void Update()
    {
        if(l_playerPushing != scriptCube.playerPushing)
        {
            if(scriptCube.playerPushing == true)
            {
                CubePushed();
            }
            l_playerPushing = scriptCube.playerPushing;
        }
    }

    void CubePushed()
    {
        Debug.Log("Push me");
        SoundManager.Instance.Play("PushedCube");
    }
}
