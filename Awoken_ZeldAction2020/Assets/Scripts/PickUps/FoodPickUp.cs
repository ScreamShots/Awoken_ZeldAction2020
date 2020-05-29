using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickUp : MonoBehaviour
{

    [SerializeField]
    int vegetableID = 0;

    private void Start()
    {
        if (ProgressionManager.Instance.R1Vegetables[vegetableID])
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            IsEnigma1Done.CollectFood();
            ProgressionManager.Instance.R1Vegetables[vegetableID] = true;
            ProgressionManager.Instance.SaveTheProgression();
            Destroy(gameObject);
        }
    }
}

