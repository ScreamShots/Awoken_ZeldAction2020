using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons du projectile de l'Archer
/// </summary>

public class BulletSound : MonoBehaviour
{

    #region HideInInspector var Statement
    private BulletComportement bulletBehavior;
    private PrefabSoundManager bulletManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        bulletBehavior = GetComponentInParent<BulletComportement>();
        bulletManager = GetComponent<PrefabSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
