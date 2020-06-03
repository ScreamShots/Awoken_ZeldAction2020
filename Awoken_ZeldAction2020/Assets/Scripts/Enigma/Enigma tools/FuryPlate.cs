using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryPlate : PressurePlateBehavior
{
    [SerializeField]
    private GameObject furyPickUp = null;
    [SerializeField]
    private Transform spawnPoint = null;

    [Header("Particle")]
    public Transform particleSpawn = null;
    public GameObject particlePouf = null;
    private bool particleExist = false;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            isPressed = true;
            Instantiate(EnemyManager.Instance.cloud, spawnPoint.position, Quaternion.identity);
            Instantiate(furyPickUp, spawnPoint.position, Quaternion.identity);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            isPressed = false;
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {

    }

    void Update()
    {
        if (isPressed)
        {
            if (!particleExist)
            {
                particleExist = true;

                GameObject particleInstance = Instantiate(particlePouf, particleSpawn.position, particlePouf.transform.rotation);
                Destroy(particleInstance, 0.5f);
            }
        }
        else
        {
            particleExist = false;
        }
    }
}
