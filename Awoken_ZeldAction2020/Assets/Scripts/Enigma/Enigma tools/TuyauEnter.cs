using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnter : MonoBehaviour
{
    public Transform exit;
    public GameObject bullet;
    [Header("Pipe Duration")]
    [SerializeField]
    private float bulletTimeTravel = 0;
    [SerializeField]
    private float nbrOfPipes;
    private float animDuration;
    [HideInInspector]
    public bool isEnter;
    [HideInInspector]
    public bool isExit;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer ("PlayerProjectile"))
        {
            StartCoroutine(animEnter());
            bullet = other.gameObject;
            StartCoroutine(BulletTrajectory());

        }
    }

    private IEnumerator BulletTrajectory()
    {
        bullet.SetActive(false);
        bullet.transform.position = exit.position;
        yield return new WaitForSeconds(bulletTimeTravel * nbrOfPipes);
        StartCoroutine(AnimExit());
        bullet.SetActive(true);
        bullet.GetComponent<BulletComportement>().aimDirection = exit.up;
    }

    private IEnumerator animEnter()
    {
        isEnter = true;
        yield return new WaitForSeconds(1f);
        isEnter = false;
    }

    private IEnumerator AnimExit()
    {
        isExit = true;
        yield return new WaitForSeconds(1f);
        isExit = false;
    }

}
