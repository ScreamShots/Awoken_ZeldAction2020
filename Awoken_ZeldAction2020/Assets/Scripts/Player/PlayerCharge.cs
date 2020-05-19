using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    #region Serialized Var Statement
    [Header("Requiered Elements")]
    [SerializeField]
    [Tooltip("References of all chargeZones GameObject")]
    private GameObject[] allChargeHitZones = null;
    [SerializeField]
    private GameObject collisionDetectionSecurity = null;
    [SerializeField]
    private GameObject collisionDetection = null;
    [Header("Stats")]
    [SerializeField]
    float chargeDistance = 0;
    [SerializeField]
    float chargeSpeed = 0;
    #pragma warning disable CS0414
    [SerializeField]
    AnimationCurve chargeSpeedProgression = null;
    #pragma warning restore CS0414
    [SerializeField]
    float finishChargeDuration = 0;
    #pragma warning disable CS0414
    [SerializeField]
    AnimationCurve finishSpeedReduction = null;
    #pragma warning restore CS0414
    [SerializeField]
    float chargeDamage = 0;
    [SerializeField]
    float areaRadius = 0;
    [SerializeField]
    float knockBackStrenght = 0;
    [SerializeField]
    ParticleSystem explosion = null;
    [SerializeField]
    ParticleSystem trail = null;


    #endregion

    #region HideInInspector Var Statement
    Dictionary<string, ShieldHitZone> allChargeHitZoneScript = new Dictionary<string, ShieldHitZone>();
    PlayerMovement playerMoveScript;
    [HideInInspector]
    public bool canCharge;
    bool canFinishCharge;
    Vector2 chargeDir;
    float traveledDistance;
    Vector2 lastPos;
    float finishTimer;

    #endregion

    private void Start()
    {
        for (int i = 0; i < allChargeHitZones.Length; i++)                                      // Getting the ShieldHitZone Component of the referenced gameobjects
        {
            allChargeHitZoneScript.Add(allChargeHitZones[i].name, allChargeHitZones[i].GetComponent<ShieldHitZone>());
        }
        playerMoveScript = GetComponent<PlayerMovement>();
        trail.gameObject.SetActive(false);
    }

    private void Update()
    {        
        if(GameManager.Instance.gameState == GameManager.GameState.Running)
        {
            if (Input.GetButtonDown("Charge") && PlayerStatusManager.Instance.canCharge && GetComponent<PlayerAttack>().currentFury == GetComponent<PlayerAttack>().maxFury)
            {
                StartCharge();
            }
            else if (canCharge)
            {
                if (Input.GetButtonDown("Charge") && PlayerStatusManager.Instance.isCharging && chargeSpeedProgression.Evaluate(traveledDistance / chargeDistance) == 1)
                {
                    FinishCharge();
                }
            }
        }
    }

    private void FixedUpdate()
    {      

        if (canCharge)
        {
            if (traveledDistance < chargeDistance)
            {
                PlayerMovement.playerRgb.velocity = chargeDir * chargeSpeed * chargeSpeedProgression.Evaluate(traveledDistance / chargeDistance) * Time.fixedDeltaTime;
                traveledDistance += new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y).magnitude;
                lastPos = transform.position;
            }
            else
            {
                FinishCharge();
            }
        }
        if (canFinishCharge)
        {
            if (finishTimer < finishChargeDuration)
            {
                PlayerMovement.playerRgb.velocity = chargeDir * chargeSpeed * finishSpeedReduction.Evaluate(finishTimer / finishChargeDuration) * Time.fixedDeltaTime;
                finishTimer += Time.fixedDeltaTime;
            }
            else
            {
                StartCoroutine(EndCharge());
            }
        }
    }

    void StartCharge()
    {
        PlayerStatusManager.Instance.isCharging = true;
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        GetComponent<PlayerAttack>().currentFury = 0;
        canCharge = true;
        lastPos = transform.position;
        collisionDetection.layer = LayerMask.NameToLayer("Enemy");
        collisionDetectionSecurity.SetActive(true);
        trail.gameObject.SetActive(true);

        switch (playerMoveScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                chargeDir = new Vector2(0, 1);
                allChargeHitZoneScript["Up"].gameObject.SetActive(true);
                allChargeHitZoneScript["Up"].isActivated = true;
                break;
            case PlayerMovement.Direction.down:
                chargeDir = new Vector2(0, -1);
                allChargeHitZoneScript["Down"].gameObject.SetActive(true);
                allChargeHitZoneScript["Down"].isActivated = true;
                break;
            case PlayerMovement.Direction.right:
                chargeDir = new Vector2(1, 0);
                allChargeHitZoneScript["Right"].gameObject.SetActive(true);
                allChargeHitZoneScript["Right"].isActivated = true;
                break;
            case PlayerMovement.Direction.left:
                chargeDir = new Vector2(-1, 0);
                allChargeHitZoneScript["Left"].gameObject.SetActive(true);
                allChargeHitZoneScript["Left"].isActivated = true;
                break;
            default:
                break;
        }
    }

    void FinishCharge()
    {
        canCharge = false;
        canFinishCharge = true;
        finishTimer = 0;
        GetComponentInChildren<PlayerAnimator>().EndCharge();
    }

    public void FastEndCharge()
    {
        canFinishCharge = false;
        canCharge = false;
        trail.gameObject.SetActive(false);
        explosion.Play();
        GetComponentInChildren<PlayerAnimator>().HardEndCharge();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, areaRadius);
        foreach (Collider2D enemy in hitEnemies)
        {

            if (enemy.CompareTag("HitBox") && enemy.transform.root.CompareTag("Enemy"))
            {
                if (enemy.transform.root.gameObject.GetComponent<BasicHealthSystem>() != null)
                {
                    enemy.transform.root.gameObject.GetComponent<BasicHealthSystem>().TakeDmg(chargeDamage);
                }

            }
        }

        PlayerMovement.playerRgb.velocity = Vector2.zero;
        PlayerStatusManager.Instance.needToEndCharge = true;

        traveledDistance = 0;
        collisionDetection.layer = LayerMask.NameToLayer("Player");
        collisionDetectionSecurity.SetActive(false);

        foreach (GameObject chargeZone in allChargeHitZones)
        {
            chargeZone.GetComponent<ShieldHitZone>().isActivated = false;
            chargeZone.SetActive(false);
        }
    }
    public IEnumerator EndCharge()
    {
        canFinishCharge = false;
        canCharge = false;
        PlayerMovement.playerRgb.velocity = Vector2.zero;
        trail.gameObject.SetActive(false);
        GetComponentInChildren<PlayerAnimator>().Slam();
        yield return new WaitForSeconds(0.40f);
        explosion.Play();


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, areaRadius);
        foreach (Collider2D enemy in hitEnemies)
        {
            
            if(enemy.CompareTag("HitBox") && enemy.transform.root.CompareTag("Enemy"))
            {
                if(enemy.transform.root.gameObject.GetComponent<BasicHealthSystem>() != null)
                {
                    enemy.transform.root.gameObject.GetComponent<BasicHealthSystem>().TakeDmg(chargeDamage);
                }
                
            }
        }


        PlayerStatusManager.Instance.needToEndCharge = true;

        traveledDistance = 0;
        collisionDetection.layer = LayerMask.NameToLayer("Player");
        collisionDetectionSecurity.SetActive(false);
        
        foreach(GameObject chargeZone in allChargeHitZones)
        {
            chargeZone.GetComponent<ShieldHitZone>().isActivated = false;
            chargeZone.SetActive(false);
        }
    }

    public void KnockBackEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyKnockBackCaller>().KnockEnemy(knockBackStrenght, chargeDir);
        enemy.GetComponent<BasicHealthSystem>().TakeDmg(chargeDamage / 3);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
