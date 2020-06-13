using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspirateur : MonoBehaviour
{
    [SerializeField]
    float speed = 0;
    [SerializeField]
    float detectionDistance = 0;

    Vector2 distanceToPlayer;

    private void Update()
    {
        distanceToPlayer = PlayerManager.Instance.transform.position - transform.parent.position;

        if(distanceToPlayer.magnitude < detectionDistance)
        {
            distanceToPlayer.Normalize();
            transform.parent.Translate(distanceToPlayer * Time.deltaTime * speed);
        }
    }
}

