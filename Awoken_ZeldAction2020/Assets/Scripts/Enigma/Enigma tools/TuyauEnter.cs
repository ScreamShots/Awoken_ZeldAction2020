using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnter : MonoBehaviour
{
    public Transform exit;
    public GameObject bullet;
    [SerializeField]
    private float bulletTimeTravel;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer ("PlayerProjectile"))
        {
            bullet = other.gameObject;
            StartCoroutine(BulletTrajectory());
        }
    }

    private IEnumerator BulletTrajectory()
    {
        bullet.SetActive(false);
        bullet.transform.position = exit.position;
        yield return new WaitForSeconds(bulletTimeTravel);
        bullet.SetActive(true);
        bullet.GetComponent<BulletComportement>().aimDirection = exit.up;
    }

}
