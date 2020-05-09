using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AubergeSceneTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            SceneHandler.Instance.SceneTransition("Region_1", 0);
        }
    }
}
