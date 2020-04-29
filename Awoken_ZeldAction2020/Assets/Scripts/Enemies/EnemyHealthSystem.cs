using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// Class that inherit from basic health system class and is specification for enemy 
/// </summary>
public class EnemyHealthSystem : BasicHealthSystem
{
    //Settings for instantiation of shield of Pegase
    public bool ProtectByPegase;
    public GameObject shieldOfEnemy;
    
    private GameObject shieldInstance;
    private bool shieldExist;
    private bool pegaseIsDie;

    [Header("On Hit Flash")]

    [SerializeField]
    Color flashColor = Color.red;
    [SerializeField] [Min(0)]
    float flashTime = 0.5f;
    [SerializeField] [Min(0)]
    float flashFadeTime = 0.5f;

    [Header("Death")]
    [SerializeField]
    private bool dontHaveCorps = false;

    [SerializeField]
    GameObject corps = null;

    public float timeBeforeDestroy;

    SpriteRenderer enemyRenderer;

    float flashTimer = 0;
    float flashFadeTimer = 0;
    bool canFlash = false;
    bool canFadeFlash = false;

    #region Variables
    private int itemNum;

    private bool alreadyDropItem;
    #endregion

    #region Inspector Settings
    [Header("Drop Position")]
    [SerializeField] private float minDropDistance = 0;
    [SerializeField] private float maxDropDistance = 0;

    [Header("% Drop Chance %")]
    public float dropChanceItem0;
    public float dropChanceItem1;
    public float dropChanceItem2;

    [Space] public GameObject[] DropItemList;
    #endregion

    [HideInInspector]
    public bool corouDeathPlay;

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
        PegaseProtection();

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
        if (!dontHaveCorps)
        {
            Instantiate(corps, transform.position, Quaternion.identity);        //Instanciate a corps before destroy the object
            Destroy(gameObject);

            DropItem();
        }
        else
        {
            if (!corouDeathPlay)
            {
                corouDeathPlay = true;
                StartCoroutine(DestroyTime());
            }
        }
    }

    void PegaseProtection()
    {
        if (ProtectByPegase)
        {
            canTakeDmg = false;
            
            if (!shieldExist)
            {
                pegaseIsDie = false;
                shieldExist = true;
                PegaseShield();
            }
        }
        else
        {
            if (!pegaseIsDie)
            {
                pegaseIsDie = true;
                WhenPegaseDie();
            }       
        }
    }

    void WhenPegaseDie()
    {
        canTakeDmg = true;
        shieldExist = false;
        Destroy(shieldInstance);
    }

    void PegaseShield()
    {
        shieldInstance = Instantiate(shieldOfEnemy, transform.position, shieldOfEnemy.transform.rotation);
        shieldInstance.transform.parent = gameObject.transform;
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

    private void DropItem()
    {
        float randomNum = Random.Range(0, 101);                       //100% for determining loot chance
        float randomNum1 = Random.Range(0, 101);
        float randomNum2 = Random.Range(0, 101);

        if (randomNum <= dropChanceItem0 & !alreadyDropItem)           //if a random number is inferior to % of dropping item we set in Inspector
        {
            itemNum = 0;
            alreadyDropItem = true;                                   //can't drop 2 items on same enemy

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(transform.position.x + Random.Range(minDropDistance, maxDropDistance), transform.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
        if (randomNum1 <= dropChanceItem1 & !alreadyDropItem)
        {
            itemNum = 1;
            alreadyDropItem = true;

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(transform.position.x + Random.Range(minDropDistance, maxDropDistance), transform.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
        if (randomNum2 <= dropChanceItem2 & !alreadyDropItem)
        {
            itemNum = 2;
            alreadyDropItem = true;

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(transform.position.x + Random.Range(minDropDistance, maxDropDistance), transform.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(gameObject);
        DropItem();
    }
}
