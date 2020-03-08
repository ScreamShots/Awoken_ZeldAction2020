using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    private enum Direction {up, down, right, left };
    [SerializeField] private Direction shootDirection = Direction.up;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    [SerializeField] private bool fire = true;
    private bool isShooting;

    private void Update()
    {
        if(fire && !isShooting)
        {
            StartCoroutine(Shoot());
            isShooting = true;
        }
    }

    IEnumerator Shoot()
    {
        GameObject thisProjectile;
        thisProjectile = Instantiate(projectile, transform.position, transform.rotation);

        switch (shootDirection)
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
        
        yield return new WaitForSeconds(fireRate);

        if (fire)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            isShooting = false;
        }
        
    }
}
