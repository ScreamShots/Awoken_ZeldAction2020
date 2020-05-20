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
    public GameObject shieldOfEnemy;    
    private GameObject shieldInstance;

    [Header("On Hit Flash")]

    [SerializeField]
    Color flashColor = Color.red;
    [SerializeField] [Min(0)]
    float flashTime = 0.5f;
    [SerializeField] [Min(0)]
    float flashFadeTime = 0.5f;
    public GameObject bloodParticle;
    private GameObject bloodParticleInstance;

    [Header("Death")]
    [SerializeField]
    private bool dontHaveCorps = false;

    [SerializeField]
    GameObject corps = null;
    [SerializeField]
    Transform dropPoint = null;

    public float timeBeforeDestroy;

    SpriteRenderer enemyRenderer;
    [HideInInspector]
    public SpawnPlateform linkedSpawnPlateform = null;

    float flashTimer = 0;
    float flashFadeTimer = 0;
    [HideInInspector]
    public bool canFlash = false;
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
        if(shieldOfEnemy != null)
        {
            shieldInstance = Instantiate(shieldOfEnemy, transform.position, Quaternion.identity);
            shieldInstance.transform.parent = transform;
            shieldInstance.SetActive(false);
        }
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

        if (canTakeDmg)
        {
            bloodParticleInstance = Instantiate(bloodParticle, transform.position, bloodParticle.transform.rotation);
            bloodParticleInstance.transform.parent = gameObject.transform;
        }
    }

    public override void Death()
    {
        if(linkedSpawnPlateform != null)
        {
            linkedSpawnPlateform.enemyIsDead = true;
        }
        if (!dontHaveCorps)
        {
            Instantiate(corps, dropPoint.position, Quaternion.identity);        //Instanciate a corps before destroy the object

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

    public void ActivatePegaseProtection()
    {
        if (!shieldInstance.activeInHierarchy)
        {
            shieldInstance.SetActive(true);
            canTakeDmg = false;
        }
    }

    public void DesactivatePegaseProtection()
    {
        if (shieldInstance.activeInHierarchy)
        {
            shieldInstance.SetActive(false);
            canTakeDmg = true;
        }
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
            itemDrop.transform.position = new Vector2(dropPoint.position.x + Random.Range(minDropDistance, maxDropDistance), dropPoint.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
        if (randomNum1 <= dropChanceItem1 & !alreadyDropItem)
        {
            itemNum = 1;
            alreadyDropItem = true;

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(dropPoint.position.x + Random.Range(minDropDistance, maxDropDistance), dropPoint.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
        if (randomNum2 <= dropChanceItem2 & !alreadyDropItem)
        {
            itemNum = 2;
            alreadyDropItem = true;

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(dropPoint.position.x + Random.Range(minDropDistance, maxDropDistance), dropPoint.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);

        Destroy(gameObject);
        DropItem();
    }
}
