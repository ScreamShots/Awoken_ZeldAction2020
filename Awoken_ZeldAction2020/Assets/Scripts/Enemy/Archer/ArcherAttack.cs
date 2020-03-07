using System.Collections;
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

    public GameObject archerBullet;

    private bool archerIsShooting;
    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;
    }

    private void Update()
    {
        if (gameObject.GetComponent<ArcherMovement>().archerCanAttack && !archerIsShooting)         //if player is in attack zone and archer isn't shooting
        {
            ArcherShoot();
            StartCoroutine(CooldownShoot());
        }
    }

    void ArcherShoot()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;            //Calculate direction between archer && player

        GameObject bulletInstance = Instantiate(archerBullet, transform.position, Quaternion.identity);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = direction * archerBullet.GetComponent<BulletComportement>().bulletSpeed;
    }

    IEnumerator CooldownShoot()                                                                     //Time between shoots
    {
        archerIsShooting = true;
        yield return new WaitForSeconds(archerBullet.GetComponent<BulletComportement>().timeBtwShots);
        archerIsShooting = false;
    }
}
