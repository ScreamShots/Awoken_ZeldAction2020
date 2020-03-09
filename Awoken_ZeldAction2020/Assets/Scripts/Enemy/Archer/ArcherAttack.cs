﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather attack of the archer and feedback
/// </summary>

public class ArcherAttack : MonoBehaviour
{
    #region Variables
    private GameObject player;

    [Header("Attack Settings")]
    public float timeBeforeShoot;

    [Header("Bullet initiate")]
    public GameObject archerBullet;

    public Transform shootPoint;

    private bool archerIsShooting;

    [HideInInspector]
    public bool archerIsAttacking;
    #endregion

    [SerializeField] float timeBtwShots;
    public bool archerCanAttack;

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;
    }

    private void Update()
    {
        if (archerCanAttack && !archerIsShooting)         //if player is in attack zone and archer isn't shooting
        {
            StartCoroutine(PrepareToShoot());
            archerIsShooting = true;
            archerIsAttacking = true;
        }
    }

    void ArcherShoot()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;            //Calculate direction between archer && player

        GameObject bulletInstance = Instantiate(archerBullet, shootPoint.position, shootPoint.rotation);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = direction;
    }

    IEnumerator CooldownShoot()                                                                     //Time between shoots
    {
        yield return new WaitForSeconds(timeBtwShots);
        archerIsShooting = false;
    }

    IEnumerator PrepareToShoot()                                                                    //Time before attack : time of the animation
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        ArcherShoot();
        archerIsAttacking = false;
        StartCoroutine(CooldownShoot());
    }
}
