using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryPlate : PressurePlateBehavior
{
    [SerializeField]
    private GameObject furyPickUp = null;
    [SerializeField]
    private Transform spawnPoint = null;
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
}
