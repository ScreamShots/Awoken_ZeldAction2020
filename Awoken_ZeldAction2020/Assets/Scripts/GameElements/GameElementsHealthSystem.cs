using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script is inherited of basicHealthSystem and it use on GameElements
/// </summary>

public class GameElementsHealthSystem : BasicHealthSystem
{
    [Header("On Hit Flash")]
    [Header("Corps")]

    [SerializeField]
    Color flashColor = Color.red;
    [SerializeField]
    [Min(0)]
    float flashTime = 0.5f;
    [SerializeField]
    [Min(0)]
    float flashFadeTime = 0.5f;

    [Header("Death")]

    [SerializeField]
    GameObject corps = null;

    SpriteRenderer enemyRenderer;

    float flashTimer = 0;
    float flashFadeTimer = 0;
    bool canFlash = false;
    bool canFadeFlash = false;

    protected override void Start()
    {
        base.Start();
        enemyRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if (canFlash)
        {
            HitFlash();
        }
        else if (canFadeFlash)
        {
            FadeFlash();
        }
    }

    public override void TakeDmg(float dmgTaken)
    {
        base.TakeDmg(dmgTaken);
        if (canTakeDmg)
        {
            canFlash = true;
            canFadeFlash = false;
            flashTimer = flashTime;
        }
    }

    public override void Death()
    {
        Instantiate(corps, transform.position, Quaternion.identity);        //Instanciate a corps before destroy the object
        Destroy(gameObject);
    }

    void HitFlash()
    {
        if (enemyRenderer.color != flashColor)
        {
            flashTimer -= Time.deltaTime;
            enemyRenderer.color = Color.Lerp(enemyRenderer.color, flashColor, 1 - (flashTimer / flashTime));
        }
        else
        {
            flashTimer = flashTime;
            canFlash = false;
            canFadeFlash = true;
        }
    }

    void FadeFlash()
    {
        if (enemyRenderer.color != Color.white)
        {
            flashFadeTimer -= Time.deltaTime;
            enemyRenderer.color = Color.Lerp(enemyRenderer.color, Color.white, 1 - (flashFadeTimer / flashFadeTime));
        }
        else
        {
            flashFadeTimer = flashFadeTime;
            canFadeFlash = false;
        }
    }
}
