using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlympeLRCutSceneTrigger : MonoBehaviour
{
    [SerializeField]
    BasicCutSceneManager cutsceneToTrigger = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        if (cutsceneToTrigger.gameObject.activeInHierarchy)
        {
            cutsceneToTrigger.StartCutScene();
        }
    }

}
