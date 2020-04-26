using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnter : MonoBehaviour
{
    public TuyauExit exit;
    public GameObject bullet;


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
            bullet.SetActive(false);
            bullet.transform.position = exit.bulletTp.position;
            bullet.SetActive(true);
            bullet.GetComponent<BulletComportement>().aimDirection = exit.direction;
        }
    }
}
