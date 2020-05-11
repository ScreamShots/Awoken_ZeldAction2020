using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons des projectiles
/// </summary>

public class BulletSound : MonoBehaviour
{

    #region HideInInspector var Statement
    private BulletComportement bulletBehavior;
    private PrefabSoundManager bulletManager;
    private BlockHandler projectileHandler;

    private bool l_hasBeenLaunchBack;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        bulletBehavior = GetComponentInParent<BulletComportement>();
        bulletManager = GetComponent<PrefabSoundManager>();
        projectileHandler = GetComponentInParent<BlockHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(l_hasBeenLaunchBack != projectileHandler.hasBeenLaunchBack)
        {
            if(projectileHandler.hasBeenLaunchBack == true)
            {
                Launch();
            }
            l_hasBeenLaunchBack = projectileHandler.hasBeenLaunchBack;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)             //When collide with anything, play sound
    {
        if (other.CompareTag("HitBox") && other.gameObject.transform.root.CompareTag("Player"))
        {
            BulletDeath();
        }
    }

    void BulletDeath()
    {
        bulletManager.Play("BulletDeath");
    }

    void Launch()
    {
        bulletManager.PlayOnlyOnce("LaunchBack");
    }
}
