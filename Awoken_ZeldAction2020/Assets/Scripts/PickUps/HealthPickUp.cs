﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    #region Inspector Settings
    [Space] [Header("Stats")]

    [SerializeField]
    private bool canPickFullLife;

    [Space] [SerializeField]
    [Range(0, 200)]
    float healToRegen = 0;

    [Space] [Header("Destroy")]

    [SerializeField]
    private bool destroyAfterTime;

    [Space] [SerializeField]
    [Min(0)]
    float timeBeforeDestroy = 0;
    [SerializeField]
    [Min(0)]
    float destroyTime = 0;
    [SerializeField]
    [Min(0)]
    float maxTimeBtwFlash = 0;

    private float timer;
    private bool isDestroying;
    [HideInInspector]
    public bool isPicked;
    #endregion

    private void Start()
    {
        timer = timeBeforeDestroy;
    }

    private void Update()
    {
        if (destroyAfterTime)                                       //if we set that PickUp can be destroy
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && !isDestroying)
            {
                isDestroying = true;
                StartCoroutine(FlashBeforeDestroy());
            }
            else if (isDestroying)
            {
                destroyTime -= Time.deltaTime;
                
                if (maxTimeBtwFlash < 0.1f)
                {
                    maxTimeBtwFlash = 0.1f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            if (canPickFullLife)
            {
                detectedElement.transform.root.gameObject.GetComponent<BasicHealthSystem>().Heal(healToRegen);
                isPicked = true;
                Destroy(gameObject);
            }
            else
            {
                if (detectedElement.transform.root.gameObject.GetComponent<BasicHealthSystem>().currentHp < detectedElement.transform.root.gameObject.GetComponent<BasicHealthSystem>().maxHp)
                {
                    detectedElement.transform.root.gameObject.GetComponent<BasicHealthSystem>().Heal(healToRegen);
                    isPicked = true;
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator FlashBeforeDestroy()                                //blink PickUp before destroy it
    {
        while (destroyTime > 0)
        {
            yield return new WaitForSeconds(maxTimeBtwFlash);

            maxTimeBtwFlash -= 0.02f;
            GetComponentInChildren<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(maxTimeBtwFlash);

            maxTimeBtwFlash -= 0.02f;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        Destroy(gameObject);
    }
}
