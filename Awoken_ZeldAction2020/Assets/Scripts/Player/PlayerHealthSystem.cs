using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using Cinemachine;

public class PlayerHealthSystem : BasicHealthSystem
{
    #region Serialized var Statement
    [Header("KnockBack On Hit")]
    [SerializeField] [Min(0)]
    private float knockBackIntensity = 200;
    [SerializeField] [Min(0)]
    private float knockBackDuration = 0.1f;
    [SerializeField] [Min(0)]
    private float blockIntensityReductionRatio = 0.7f;

    [Header("Vibration On Hit")]
    [SerializeField] [Min(0)]
    float vibrationIntensity = 0.1f;

    [Header("Invunerability On Hit")]
    [SerializeField] [Min(0)]
    private float invulnerabilityTime = 1;

    [Header("Flash On Hit")]
    [SerializeField]
    Color positivFlashColor = Color.red;
    [SerializeField]
    Color negativFlashColor = Color.white;
    [SerializeField]
    float flashFrequency = 0.1f;
    public GameObject bloodParticle;
    private GameObject bloodParticleInstance;

    [Header("Death")]
    [SerializeField]
    GameObject deathCam = null;
    bool playerDead = false;
    #endregion

    #region HideInInspector var Statement


    PlayerShield playerShieldScript = null;
    [HideInInspector]
    public int knockBackDir;            //0 = Down 1 = Up 2 = Left 3 = Right.

    private PlayerMovement playerMoveScript;
    private SpriteRenderer playerRenderer;

    private float flashTimer = 0;
    private float colorSwapTimer = 0;
    private int colorPhase = 0;
    [HideInInspector]
    public bool canFlash = false;

    #endregion

    protected override void Start()
    {
        base.Start();
        playerShieldScript = GetComponent<PlayerShield>();
        playerMoveScript = GetComponent<PlayerMovement>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (canFlash)
        {
            HitFlash();
        }
    }

    public override void TakeDmg(float dmgTaken, Vector3 sourcePos)
    {
        base.TakeDmg(dmgTaken);

        if(canTakeDmg && currentHp > 0) HitEffect(sourcePos);

        bloodParticleInstance = Instantiate(bloodParticle, transform.position, bloodParticle.transform.rotation);
        bloodParticleInstance.transform.parent = gameObject.transform;
    }

    public override void Death()
    {
        if (!playerDead)
        {
            StartCoroutine(DeathFeedBack());           
        }
    }

    IEnumerator DeathFeedBack()
    {
        playerDead = true;
        PlayerMovement.playerRgb.velocity = Vector2.zero;

        if (PlayerStatusManager.Instance.isBlocking)
        {
            GetComponent<PlayerShield>().DesactivateBlock();
        }
        if (PlayerStatusManager.Instance.isCharging)
        {
            GetComponent<PlayerCharge>().FastEndCharge();
        }

        yield return new WaitForEndOfFrame();

        GameManager.Instance.PlayerDeath();
        LvlManager.Instance.lvlCamBrain.m_DefaultBlend.m_Time = 0.75f;
        deathCam.SetActive(true);

        GetComponentInChildren<PlayerAnimator>().Die();
    }

    public void Respawn()
    {
        currentHp = maxHp;
        playerShieldScript.currentStamina = playerShieldScript.maxStamina;
        GetComponentInChildren<PlayerAnimator>().Respawn();
        deathCam.SetActive(false);
        LvlManager.Instance.lvlCamBrain.m_DefaultBlend.m_Time = LvlManager.Instance.defaultblendTime;
        LvlManager.Instance.InitializeLvl(LvlManager.Instance.lastLoadedstartZoneIndex);
        playerDead = false;
    }

    void HitFlash()
    {
        if(flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;

            if(colorSwapTimer > 0)
            {
                colorSwapTimer -= Time.deltaTime;

                if (colorPhase == 0)
                {
                    playerRenderer.color = Color.Lerp(playerRenderer.color, positivFlashColor, 1 - (colorSwapTimer / flashFrequency));
                }
                else
                {
                    playerRenderer.color = Color.Lerp(playerRenderer.color, negativFlashColor, 1 - (colorSwapTimer / flashFrequency));
                }
            }
            else
            {
                if(colorPhase == 0)
                {
                    colorPhase = 1;
                    colorSwapTimer = flashFrequency;
                }
                else
                {
                    colorPhase = 0;
                    colorSwapTimer = flashFrequency;
                }
            }
        }
        else
        {
            colorSwapTimer = 0;
            canFlash = false;
            playerRenderer.color = Color.white;
        }
    }

    void HitEffect(Vector3 sourcePos)
    {
        switch (PlayerStatusManager.Instance.currentState)
        {
            case PlayerStatusManager.State.neutral:
                StartCoroutine(Invulerability());
                StartCoroutine(KnockBack(sourcePos, false));
                StartCoroutine(Vibration());
                canFlash = true;
                flashTimer = invulnerabilityTime;
                break;
            case PlayerStatusManager.State.block:
                StartCoroutine(Invulerability());
                StartCoroutine(KnockBack(sourcePos, true));
                StartCoroutine(Vibration());
                canFlash = true;
                flashTimer = invulnerabilityTime;
                break;
            default:
                StartCoroutine(Invulerability());
                StartCoroutine(Vibration());
                canFlash = true;
                flashTimer = invulnerabilityTime;
                break;
        }
    }

    IEnumerator KnockBack (Vector3 sourcePos, bool isOnbloc)
    {
        if(GameManager.Instance.gameState == GameManager.GameState.Running)
        {
            Vector2 sourceRelativPos = sourcePos - transform.position;
            float reductionRatio = 0;
            PlayerStatusManager.Instance.isKnockBacked = true;

            if (isOnbloc)
            {
                reductionRatio = blockIntensityReductionRatio;
            }
            else
            {
                reductionRatio = 1;
            }

            yield return new WaitForFixedUpdate();

            PlayerMovement.playerRgb.velocity = Vector2.zero;
            PlayerMovement.playerRgb.AddForce(-sourceRelativPos.normalized * knockBackIntensity * reductionRatio);

            if (!PlayerStatusManager.Instance.isBlocking)
            {
                if (Mathf.Abs(sourceRelativPos.y) > Mathf.Abs(sourceRelativPos.x))
                {
                    if (sourceRelativPos.y > 0)
                    {
                        knockBackDir = 0;
                        playerMoveScript.watchDirection = PlayerMovement.Direction.up;
                    }
                    else
                    {
                        knockBackDir = 1;
                        playerMoveScript.watchDirection = PlayerMovement.Direction.down;
                    }
                }
                else
                {
                    if (sourceRelativPos.x > 0)
                    {
                        knockBackDir = 2;
                        playerMoveScript.watchDirection = PlayerMovement.Direction.right;
                    }
                    else
                    {
                        knockBackDir = 3;
                        playerMoveScript.watchDirection = PlayerMovement.Direction.left;
                    }
                }
            }

            yield return new WaitForSeconds(knockBackDuration);

            PlayerMovement.playerRgb.velocity = Vector2.zero;
            PlayerStatusManager.Instance.needToEndKnockBack = true;
        }       
    }

    IEnumerator Invulerability()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(invulnerabilityTime);
        canTakeDmg = true;
    }

    IEnumerator Vibration()
    {       
        GamePad.SetVibration(PlayerIndex.One, vibrationIntensity, vibrationIntensity);
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
    }
}
