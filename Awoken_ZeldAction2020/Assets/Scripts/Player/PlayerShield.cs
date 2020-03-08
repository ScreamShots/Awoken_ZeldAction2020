using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject[] allShieldHitZones;
    private Dictionary<string, ShieldHitZone> allShieldZoneScrpit = new Dictionary<string, ShieldHitZone>();
    private PlayerMovement movementScript;
    [Min(0)]
    [SerializeField] private float knockBackIntensity;
    [Range(0.1f,1f)]
    [SerializeField]
    private float slowRatio;
    [Min(0)]
    public float maxStamina;
    [Min(0)]
    public float currentStamina;
    [Min(0)]
    [SerializeField] private float staminaLoseSpeed = 1;
    [Min(0)]
    [SerializeField] private float staminaReloadSpeed = 1;
    [SerializeField] private float timeBeforeReload;
    private bool canReload = true;
    PlayerIndex playerIndex;                        //requiered for gamepad vibrations
    [Min(0)]
    [SerializeField] float vibrateIntensity;


    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        for (int i = 0; i < allShieldHitZones.Length; i++)
        {
            allShieldZoneScrpit.Add(allShieldHitZones[i].name, allShieldHitZones[i].GetComponent<ShieldHitZone>());
        }
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Block") && PlayerStatusManager.Instance.canBlock)
        {
            ActivateBlock();
        }

        if (Input.GetButtonUp("Block") && PlayerStatusManager.Instance.isBlocking)
        {
            DesactivateBlock();
        }


        if (!PlayerStatusManager.Instance.isBlocking)
        {
            ShieldRotation();
            if (canReload && currentStamina < maxStamina)
            {
                
                ReloadStamina();
            }
        }
        else
        {
            UseStamina();
        }

        if (PlayerStatusManager.Instance.cdOnBlock)
        {
            if(currentStamina >= maxStamina)
            {
                PlayerStatusManager.Instance.cdOnBlock = false;
            }
        }
    }

    void UseStamina()
    {
        currentStamina -= Time.deltaTime * staminaLoseSpeed;
        if(currentStamina <= 0.001f)
        {
            canReload = false;
            DesactivateBlock();
            StartCoroutine(TimerBeforeReload());
            PlayerStatusManager.Instance.cdOnBlock = true;
        }
    }

    void ReloadStamina()
    {
        currentStamina += Time.deltaTime * staminaReloadSpeed;
    }

    void ShieldRotation()
    {

        foreach(GameObject shieldZone in allShieldHitZones)
        {
            shieldZone.SetActive(false);
        }

        switch (movementScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                allShieldZoneScrpit["Up"].gameObject.SetActive(true);
                break;
            case PlayerMovement.Direction.down:
                allShieldZoneScrpit["Down"].gameObject.SetActive(true);
                break;
            case PlayerMovement.Direction.left:
                allShieldZoneScrpit["Left"].gameObject.SetActive(true);
                break;
            case PlayerMovement.Direction.right:
                allShieldZoneScrpit["Right"].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    void ActivateBlock()
    {
        PlayerStatusManager.Instance.isBlocking = true;
        movementScript.speed *= slowRatio;

        switch (movementScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                allShieldZoneScrpit["Up"].isActivated = true;
                break;
            case PlayerMovement.Direction.down:
                allShieldZoneScrpit["Down"].isActivated = true;
                break;
            case PlayerMovement.Direction.left:
                allShieldZoneScrpit["Left"].isActivated = true;
                break;
            case PlayerMovement.Direction.right:
                allShieldZoneScrpit["Right"].isActivated = true;
                break;
            default:
                break;
        }
    }

    void DesactivateBlock()
    {
        PlayerStatusManager.Instance.needToEndBlock = true;
        movementScript.speed *= 1 / slowRatio;

        switch (movementScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                allShieldZoneScrpit["Up"].isActivated = false;
                break;
            case PlayerMovement.Direction.down:
                allShieldZoneScrpit["Down"].isActivated = false;
                break;
            case PlayerMovement.Direction.left:
                allShieldZoneScrpit["Left"].isActivated = false;
                break;
            case PlayerMovement.Direction.right:
                allShieldZoneScrpit["Right"].isActivated = false;
                break;
            default:
                break;
        }
    }

    public void OnElementBlocked(float staminaLose)
    {
        currentStamina -= staminaLose;
        StartCoroutine(OnBlocked());
        
    }

    IEnumerator OnBlocked()
    {
        PlayerStatusManager.Instance.canMove = false;
        yield return new WaitForFixedUpdate();
        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);
        GamePad.SetVibration(playerIndex, vibrateIntensity, vibrateIntensity);

        switch (movementScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                PlayerMovement.playerRgb.AddForce(new Vector2(0, -1) * knockBackIntensity);
                break;
            case PlayerMovement.Direction.down:
                PlayerMovement.playerRgb.AddForce(new Vector2(0, 1) * knockBackIntensity);
                break;
            case PlayerMovement.Direction.left:
                PlayerMovement.playerRgb.AddForce(new Vector2(1, 0) * knockBackIntensity);
                break;
            case PlayerMovement.Direction.right:
                PlayerMovement.playerRgb.AddForce(new Vector2(-1, 0) * knockBackIntensity);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.1f);
        GamePad.SetVibration(playerIndex, 0, 0);
        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);
        PlayerStatusManager.Instance.canMove = true;
    }

    IEnumerator TimerBeforeReload()
    {
        yield return new WaitForSeconds(timeBeforeReload);
        canReload = true;
        
    }
}
