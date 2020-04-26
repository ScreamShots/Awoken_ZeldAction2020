using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnter : MonoBehaviour
{
    public TuyauExit exit;
    public GameObject bullet;
    [SerializeField]
    private float bulletTimeTravel;


    void Start()
    {
        exit = GetComponentInChildren<TuyauExit>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer ("PlayerProjectile"))
        {
            bullet = other.gameObject;
            Debug.Log("Enter");
            StartCoroutine(BulletTrajectory());
        }
    }

    private IEnumerator BulletTrajectory()
    {
        bullet.SetActive(false);
        yield return new WaitForSeconds(bulletTimeTravel);
        bullet.transform.position = exit.bulletTp.position;
        bullet.SetActive(true);
        bullet.GetComponent<BulletComportement>().aimDirection = exit.direction;
    }

}
