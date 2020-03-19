using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This script is made for testing.
/// It's a Object that fire preset projectile (selectionned in the inspector) in a chosen direction.
/// You can choose the fire direction, fire rate and the projectile speed from this script.
/// </summary>

public class ProjectileThrower : MonoBehaviour
{
    #region HideInInspector var Statement

    private enum Direction {up, down, right, left };

    private bool isShooting;

    #endregion

    #region SerializeField var Statement    

    [Header("Requiered Elements")]

    [SerializeField] private GameObject projectile = null;

    [Header("Stats")]

    [SerializeField] private Direction shootDirection = Direction.up;

    [SerializeField] private float fireRate = 0;
    [SerializeField] private bool fire = true;
    [SerializeField] private float projectileSpeed = 0;

    #endregion
    
    private void Update()
    {
        if(fire && !isShooting)             //if fire is activate and the thrower is not shotting activate shooting
        {
            StartCoroutine(Shoot());
            isShooting = true;
        }
    }

    IEnumerator Shoot()                 //Throw determined projectile is selectionned direction (loop every fire rate value if fire is activated)
    {
        GameObject thisProjectile;                  //reference for the instancitate projectile

        thisProjectile = Instantiate(projectile, transform.position, transform.rotation);       //create an instance of the projectile preset

        switch (shootDirection)             //setting projectile movement direction dpending on the selectionned direction
        {
            case Direction.up:
                thisProjectile.GetComponent<ClassicProjectile>().projectileOrientation = ClassicProjectile.Direction.up;
                break;
            case Direction.down:
                thisProjectile.GetComponent<ClassicProjectile>().projectileOrientation = ClassicProjectile.Direction.down;
                break;
            case Direction.right:
                thisProjectile.GetComponent<ClassicProjectile>().projectileOrientation = ClassicProjectile.Direction.right;
                break;
            case Direction.left:
                thisProjectile.GetComponent<ClassicProjectile>().projectileOrientation = ClassicProjectile.Direction.left;
                break;
            default:
                break;
        }

        thisProjectile.GetComponent<ClassicProjectile>().speed = projectileSpeed;               //set projectile speed depending on the one referenced here

        yield return new WaitForSeconds(fireRate);                                              //Waiting the fire Rate Time

        if (fire)                                                                               
        {
            StartCoroutine(Shoot());                                                            //if fire is activated loop on the shoot
        }
        else
        {
            isShooting = false;
        }
        
    }
}
