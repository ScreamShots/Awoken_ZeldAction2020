using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// Class that inherit from basic health system class and is specification for enemy 
/// </summary>
public class EnemyHealthSystem : BasicHealthSystem
{
    [Header("On Hit Flash")]
    [SerializeField]
    Color flashColor = Color.red;
    [SerializeField] [Min(0)]
    float flashTime = 0.5f;
    [SerializeField] [Min(0)]
    float flashFadeTime = 0.5f;

    [Header("Death")]
    [SerializeField]
    GameObject corps = null;

    SpriteRenderer enemyRenderer;

    float flashTimer = 0;
    float flashFadeTimer = 0;
    bool canFlash = false;
    bool canFadeFlash = false;

    /// <summary>
    /// The rest of the Health system behaviour (like maxHp, currentHp or TakeDmg() method are inherited from the basic system class
    /// Even if they are not visible here they are enable and u can call and use them
    /// If u need to modify method from the parent class just class the method preceed by override
    /// If u want to add content to a method but keep all that's on the parent one just overide the method and place the line base.MethodName() in the overide method
    /// </summary>

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
        if(enemyRenderer.color != flashColor)
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
        if(enemyRenderer.color != Color.white)
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
