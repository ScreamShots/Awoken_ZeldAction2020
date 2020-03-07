using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather the comportement of the bullet
/// </summary>

public class BulletComportement : MonoBehaviour
{
    #region Variables

    [Header("Bullet Stats")]
    public float bulletSpeed;

    public float timeBtwShots;
    
    [Space]
    public float TimeBeforeDestroy;
    #endregion

    private void Start()
    {
        Destroy(gameObject, TimeBeforeDestroy);                 //Destroy bullet after x time : security
    }

    private void OnTriggerEnter2D(Collider2D other)             //When collide with player destroy bullet
    {
        if (other.CompareTag("HitBox"))
        {
            Destroy(gameObject);
        }
    }
}
