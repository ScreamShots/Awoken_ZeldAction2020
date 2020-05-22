using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Made by Rémi Sécher
/// This script is used to managed shield behoviour (block and pary) following shieldZone informations.
/// Other elements behaviour depending on player block are managed specificly into these elements scripts.
/// </summary>

public class PlayerShield : MonoBehaviour
{
    #region HideInInspector var Statement

    
    private Dictionary<string, ShieldHitZone> allShieldZoneScrpit = new Dictionary<string, ShieldHitZone>();
    private Dictionary<string, ParyHitZone> allParyZoneScript = new Dictionary<string, ParyHitZone>();
    private PlayerMovement movementScript;
    private PlayerMovement.Direction l_Direction = PlayerMovement.Direction.left;

    private bool canReload = true;                  //use to know if the stamina can recover or not
    [HideInInspector]
    public bool onPary = false;
    [HideInInspector]
    public bool blockingAnElement;

    PlayerIndex playerIndex = PlayerIndex.One;                        //requiered for gamepad vibrations

    #endregion

    #region SerializeField var Statement

    [Header("Requiered Elements")]

    [SerializeField] [Tooltip("References of all shieldHitZones GameObject")]
     private GameObject[] allShieldHitZones = null;

    [SerializeField] [Tooltip("References of all paryHitZones GameObject")]
    private GameObject[] allParyHitZones = null;

    [Header("Stats")]

    [SerializeField] [Min(0)] 
    private float knockBackIntensity = 0;

    [Range(0f,1f)]    
    public float slowRatio = 0;

    [Header("Stamina Informations")]

    [Min(0)]
    public float maxStamina;

    [Min(0)]
    public float currentStamina;

    [SerializeField] [Min(0)] [Tooltip("Speed at which the stamina bar fall off")]
    private float staminaLoseSpeed = 1;
    [SerializeField] [Min(0)] [Tooltip("Speed at which the stamina recover")]
    private float staminaReloadSpeed = 1;
    [SerializeField] [Min(0)] [Tooltip("Time before the stamina start recover if it fall behind 0")]
    private float timeBeforeReload = 0;

    [Header("FeedBack")]

    [Min(0)]
    [SerializeField] float vibrateIntensity = 0;

    #endregion

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();

        for (int i = 0; i < allShieldHitZones.Length; i++)                                      // Getting the ShieldHitZone Component of the referenced gameobjects
        {
            allShieldZoneScrpit.Add(allShieldHitZones[i].name, allShieldHitZones[i].GetComponent<ShieldHitZone>());         
        }

        for (int i = 0; i < allParyHitZones.Length; i++)                                      // Getting the ParyHitZone Component of the referenced gameobjects
        {
            allParyZoneScript.Add(allParyHitZones[i].name, allParyHitZones[i].GetComponent<ParyHitZone>());
        }

        currentStamina = maxStamina;                                                            //Initializing stamina
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Running)
        {
            if (Input.GetButtonDown("Block") || Input.GetAxis("Block") != 0)
            {
                if (PlayerStatusManager.Instance.canBlock)
                {
                    ActivateBlock();
                }
            }

            if (!Input.GetButton("Block") && PlayerStatusManager.Instance.isBlocking && Input.GetAxis("Block") == 0)
            {
                DesactivateBlock();
            }


            if (!PlayerStatusManager.Instance.isBlocking)
            {
                ShieldRotation();

                if (canReload && currentStamina < maxStamina)                       //the stamina can reload only if the shield is not activated
                {

                    ReloadStamina();
                }
            }
            else
            {
                if (!onPary)
                {
                    UseStamina();                       //use stamina if the shield is activated
                }
               

                if (Input.GetButtonDown("Attack") && !PlayerStatusManager.Instance.cdOnAttack)              //Stop usage of shield when attack's input is pressed and attack.
                {
                    DesactivateBlock();
                    StartCoroutine(GetComponent<PlayerAttack>().LaunchAttack());
                }
            }

            if (PlayerStatusManager.Instance.cdOnBlock)
            {
                if (currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                    PlayerStatusManager.Instance.cdOnBlock = false;                // if the block is impossible to activate due to stamina value falling off behind 0, reunable the block utilisation when stamina goes back to max stamina value;
                }
            }
        }
          
    }

    void UseStamina()                       //reduce stamina value by time following staminaLoseSpeed value (ratio)
    {
        currentStamina -= Time.deltaTime * staminaLoseSpeed;        //Lose stamina every Tick (intensity of the lose depends on staminaLoseSpeed

        if (currentStamina <= 0.001f)                               //If the value fall to 0 on utilisation activate the linked behaviour
        {
            currentStamina = 0;
            canReload = false;                                      //Disable recover capacity;
            DesactivateBlock();
            StartCoroutine(TimerBeforeReload());                    //Start cd before stamina goes back to recover

            PlayerStatusManager.Instance.cdOnBlock = true;          //Giving information to the StatusManager that block is on Cd
        }
    }

    void ReloadStamina()                    //recover stamina value by time following staminaReloadSpeed value (ratio)
    {
        currentStamina += Time.deltaTime * staminaReloadSpeed;           //Recover stamina every Tick (intensity of the gain depends on staminaReloadSpeed)
    }

    void ShieldRotation()                   //Make the Shield zone accurate to the watch direction in real time if the shield is not activated;
    {
        if(l_Direction != movementScript.watchDirection)
        {
            l_Direction = movementScript.watchDirection;

            foreach(GameObject shieldZone in allShieldHitZones)
            {
                shieldZone.SetActive(false);
            }

            foreach(GameObject paryZone in allParyHitZones)
            {
                paryZone.SetActive(false);
            }

            switch (movementScript.watchDirection)              //Activate the only-one GameObject that match the watch direction
            {
                case PlayerMovement.Direction.up:
                    allShieldZoneScrpit["Up"].gameObject.SetActive(true);
                    allParyZoneScript["Up"].gameObject.SetActive(true);
                    break;
                case PlayerMovement.Direction.down:
                    allShieldZoneScrpit["Down"].gameObject.SetActive(true);
                    allParyZoneScript["Down"].gameObject.SetActive(true);
                    break;
                case PlayerMovement.Direction.left:
                    allShieldZoneScrpit["Left"].gameObject.SetActive(true);
                    allParyZoneScript["Left"].gameObject.SetActive(true);
                    break;
                case PlayerMovement.Direction.right:
                    allShieldZoneScrpit["Right"].gameObject.SetActive(true);
                    allParyZoneScript["Right"].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }

        }        
    }

    void ActivateBlock()                    //On dedicated input, Activate the shield following watch direction;
    {
        PlayerStatusManager.Instance.isBlocking = true;             //Giving information to the StatusManager That we are blocking



        GameObject pariedElement = null;

        switch (movementScript.watchDirection)                      //Activate on single ShieldHitZone (On specific element behaviour) following watch direction
        {
            case PlayerMovement.Direction.up:                

                for(int i =0; i< allParyZoneScript["Up"].detectedElements.Count; i++)
                {
                    if (allParyZoneScript["Up"].detectedElements[i] != null)
                    {
                        pariedElement = allParyZoneScript["Up"].detectedElements[i];
                        if (!pariedElement.GetComponent<BlockHandler>().isParied)
                        {
                            pariedElement.GetComponent<BlockHandler>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().bulletRgb.velocity = Vector2.zero;
                           // pariedElement.layer = LayerMask.NameToLayer("PlayerProjectile");       //change the layer of the bullet from enemyProjectile (for player interraction) to playerProjectile (for enemy intrraction).
                            GameManager.Instance.ProjectileParyStart(pariedElement);
                            onPary = true;
                        }                        
                        break;
                    }
                }
                if(pariedElement == null)
                {
                    allShieldZoneScrpit["Up"].isActivated = true;
                    movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                }                
                
                break;
            case PlayerMovement.Direction.down:

                for (int i = 0; i < allParyZoneScript["Down"].detectedElements.Count; i++)
                {
                    if (allParyZoneScript["Down"].detectedElements[i] != null)
                    {
                        pariedElement = allParyZoneScript["Down"].detectedElements[i];
                        if (!pariedElement.GetComponent<BlockHandler>().isParied)
                        {
                            pariedElement.GetComponent<BlockHandler>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().bulletRgb.velocity = Vector2.zero;
                            //pariedElement.layer = LayerMask.NameToLayer("PlayerProjectile");       //change the layer of the bullet from enemyProjectile (for player interraction) to playerProjectile (for enemy intrraction).
                            GameManager.Instance.ProjectileParyStart(pariedElement);
                            onPary = true;
                        }
                        break;
                    }
                }
                if (pariedElement == null)
                {
                    allShieldZoneScrpit["Down"].isActivated = true;
                    movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                }
                break;
            case PlayerMovement.Direction.left:

                for (int i = 0; i < allParyZoneScript["Left"].detectedElements.Count; i++)
                {
                    if (allParyZoneScript["Left"].detectedElements[i] != null)
                    {
                        pariedElement = allParyZoneScript["Left"].detectedElements[i];
                        if (!pariedElement.GetComponent<BlockHandler>().isParied)
                        {
                            pariedElement.GetComponent<BlockHandler>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().bulletRgb.velocity = Vector2.zero;
                            //pariedElement.layer = LayerMask.NameToLayer("PlayerProjectile");       //change the layer of the bullet from enemyProjectile (for player interraction) to playerProjectile (for enemy intrraction).
                            GameManager.Instance.ProjectileParyStart(pariedElement);
                            onPary = true;
                        }
                        break;
                    }
                }
                if (pariedElement == null)
                {
                    allShieldZoneScrpit["Left"].isActivated = true;
                    movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                }
                break;
            case PlayerMovement.Direction.right:

                for (int i = 0; i < allParyZoneScript["Right"].detectedElements.Count; i++)
                {
                    if (allParyZoneScript["Right"].detectedElements[i] != null)
                    {
                        pariedElement = allParyZoneScript["Right"].detectedElements[i];
                        if (!pariedElement.GetComponent<BlockHandler>().isParied)
                        {
                            pariedElement.GetComponent<BlockHandler>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().isParied = true;
                            pariedElement.GetComponent<BulletComportement>().bulletRgb.velocity = Vector2.zero;
                            //pariedElement.layer = LayerMask.NameToLayer("PlayerProjectile");       //change the layer of the bullet from enemyProjectile (for player interraction) to playerProjectile (for enemy intrraction).
                            GameManager.Instance.ProjectileParyStart(pariedElement);
                            onPary = true;
                        }
                        break;
                    }
                }
                if (pariedElement == null)
                {
                    allShieldZoneScrpit["Right"].isActivated = true;
                    movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                }
                break;
            default:
                break;
        }
    }

    public void AfterParyBlockActivation()
    {
        switch (movementScript.watchDirection)                      //Activate on single ShieldHitZone (On specific element behaviour) following watch direction
        {
            case PlayerMovement.Direction.up:
                allShieldZoneScrpit["Up"].isActivated = true;
                movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                break;
            case PlayerMovement.Direction.down:

                allShieldZoneScrpit["Down"].isActivated = true;
                movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block                
                break;
            case PlayerMovement.Direction.left:
                allShieldZoneScrpit["Left"].isActivated = true;
                movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                break;
            case PlayerMovement.Direction.right:
                allShieldZoneScrpit["Right"].isActivated = true;
                movementScript.speed *= slowRatio;                          //SlowPlayer Movement during block
                break;
            default:
                break;
        }
    }

    public void DesactivateBlock()                 //Desactivate block if the player relaese the dedicated input
    {
        PlayerStatusManager.Instance.needToEndBlock = true;             //Telling the StatusManager that we need to stop block behaviour


        if (!onPary)
        {
            movementScript.speed *= 1 / slowRatio;                          //Getting player movement speed back to normal
        }

        
        switch (movementScript.watchDirection)                          //Desactivated the ShieldHitZone Component that was activated during the block.
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

    public void OnElementBlocked(float staminaLose)         //Function called by extern element to apply block consequences on the player
    {
        currentStamina -= staminaLose;                              //Stamina lost due to attack block

        StartCoroutine(OnBlocked());                                //KnockBack & Feedback
        
    }

    IEnumerator OnBlocked()                                 //Manage the player KnockBack and the vibration feedback
    {
        PlayerStatusManager.Instance.canMove = false;               //Disable Movement during the knockBack
        PlayerStatusManager.Instance.isKnockBacked = true;
        blockingAnElement = true;
        yield return new WaitForFixedUpdate();                      //Erasing all physics before knockback
        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);

        GamePad.SetVibration(playerIndex, vibrateIntensity, vibrateIntensity);      //Start vibration (intensity depends on vibrate intensity)

        switch (movementScript.watchDirection)                  //KnockBack the player in the opposite direction of the watch direction (intensity depends on the knockback intensity)
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

        yield return new WaitForSeconds(0.1f);                          //Duration of the knockBack

        yield return new WaitForFixedUpdate();                          //Erasing all physics due to the knockback
        PlayerMovement.playerRgb.velocity = new Vector2(0, 0);

        GamePad.SetVibration(playerIndex, 0, 0);                        //Stopping vibrations
        PlayerStatusManager.Instance.isKnockBacked = false;
        PlayerStatusManager.Instance.canMove = true;                    //Enable Movement
        blockingAnElement = false;
    }

    IEnumerator TimerBeforeReload()                 //Manage Time before recover can restart after stamina value fell behind 0
    {
        yield return new WaitForSeconds(timeBeforeReload);
        canReload = true;        
    }
}
