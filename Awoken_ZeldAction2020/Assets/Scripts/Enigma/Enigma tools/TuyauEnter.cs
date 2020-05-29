using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauEnter : MonoBehaviour
{
    public Transform exit = null;
    public GameObject bullet = null;
    [Header("Pipe Duration")]
    public float bulletTimeTravel = 0;
    [SerializeField]
    public float nbrOfPipes = 0;
    [HideInInspector]
    public bool isEnter = false;
    [HideInInspector]
    public bool isExit = false;
    [HideInInspector]
    public bool bulletIsEnter = false;
    [HideInInspector]
    public bool bulletInsideZone = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
        {
            StartCoroutine(animEnter());
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
        yield return new WaitForSeconds(bulletTimeTravel * nbrOfPipes);
        StartCoroutine(AnimExit());
        bullet.SetActive(true);
        bullet.GetComponent<BulletComportement>().aimDirection = exit.up;
        bulletIsEnter = false;
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
