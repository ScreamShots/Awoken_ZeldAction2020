using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is use to spawn something each x seconds
/// </summary>

public class SpawnSomething : MonoBehaviour
{
    public GameObject objectToSpawn = null;
    public float timeBtwSpawn = 0;
    private float time = 0;

    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Instantiate(objectToSpawn, transform.position, objectToSpawn.transform.rotation);

            time = timeBtwSpawn;
        }
    }
}
