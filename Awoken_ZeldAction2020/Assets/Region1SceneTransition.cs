using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region1SceneTransition : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            SceneHandler.Instance.SceneTransition(scene, 0);
        }
    }
}
