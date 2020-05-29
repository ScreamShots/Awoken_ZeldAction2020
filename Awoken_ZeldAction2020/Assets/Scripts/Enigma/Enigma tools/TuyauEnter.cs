using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnter : MonoBehaviour
{
    public Transform exit;
    public GameObject bullet;
    [SerializeField]
    public float bulletTimeTravel = 0;
    [HideInInspector] public bool bulletIsEnter = false;
    [HideInInspector] public bool bulletInsideZone = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer ("PlayerProjectile"))
        {
            bullet = other.gameObject;
            StartCoroutine(BulletTrajectory());
            bulletInsideZone = true;
        }
        else
        {
            bulletInsideZone = false;
        }
    }

    private IEnumerator BulletTrajectory()
    {
        bulletIsEnter = true;
        bullet.SetActive(false);
        bullet.transform.position = exit.position;
        yield return new WaitForSeconds(bulletTimeTravel);
        bullet.SetActive(true);
        bullet.GetComponent<BulletComportement>().aimDirection = exit.up;
        bulletIsEnter = false;
    }

}
