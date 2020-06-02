using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Made by Rémi Sécher
/// This Script manage the pary.
/// For the projectile make a arrow appear, it represent the new target direction of the projectile. Launch back the projectile following the new direction when block input is pressed 
/// </summary>
public class ProjectileParyBehaviour : MonoBehaviour
{
    #region Serialized Var Statement
    [Header("Requiered Elements")]

    #pragma warning disable CS0414
    [SerializeField]
    bool showRequieredElements = false;
    #pragma warning restore CS0414

    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    GameObject orientationArrow = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    GameObject highAngleLimit = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    GameObject lowAngleLimit = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    Image clockCircleDown = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    Image clockCircleUp = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    RectTransform clockTransform = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    ParticleSystem particlesDuringPary = null;
    [SerializeField]
    [ConditionalHide("showRequieredElements", true)]
    ParticleSystem particleEndPary = null;
    [ConditionalHide("showRequieredElements", true)]
    [SerializeField]
    GameObject orientationElements = null;

    [Header("Values")]
    [SerializeField]
    [Range(0, 180)]
    float angleAmplitude = 0;
    [SerializeField]
    float rotaSpeed = 0;
    [SerializeField]
    [Min(0)]
    float timeBeforeLaunchBack = 0;
    #endregion

    #region HideInInspector Var Statement
    GameObject projectile;
    BlockHandler projectileBlockHandler;

    Vector2 projectileDirection;
    Vector2 maxRotation;
    Vector2 minRotation;


    float projectileAngle;
    float maxAngle;
    float minAngle;

    float distancetoMax;
    float distancetoMin;
    [HideInInspector]
    public bool buttonIsPressed = true;

    float timer = 0;

    #endregion

    private void Start()
    {
        orientationElements.SetActive(false);
        clockCircleDown.fillAmount = 0;
        clockCircleUp.fillAmount = 0;
    }

    private void Update()
    {        
        if(GameManager.Instance.gameState == GameManager.GameState.ProjectilePary)      //if the game is in pary gamestate (pary active)
        {

            Rotate();                   

            
            if(!Input.GetButton("Block") && Input.GetAxis("Block") == 0)  //preventing probleme with usage of the block input
            {
                if (buttonIsPressed)
                {
                    buttonIsPressed = false;
                }
            }

            if (Input.GetButtonDown("Block") || Input.GetAxis("Block") != 0)   //if the block input as been released and is pressed back stop the aim orientation and throw back projectile.
            {
                if (!buttonIsPressed)
                {
                    StopOrientation();
                    PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().AfterParyBlockActivation();          //reactivate the shield normaly at the end
                }
            }

            if(timer > 0)               //seconde way to throw back the projectile is when this timer reach 0
            {
                timer -= Time.unscaledDeltaTime;
                clockCircleDown.fillAmount = 1 -(timer / timeBeforeLaunchBack);
                clockCircleUp.fillAmount = 1-(timer / timeBeforeLaunchBack);
            }
            else
            {
                StopOrientation();                
                PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().AfterParyBlockActivation();      //reactivate the shield normaly at the end
            }
        }
    }

    public void LaunchOrientation(GameObject thisProjectile)                    //this fucntion is called from the gamemanger after all timescale and gamestate modification are done
    {
        projectile = thisProjectile;

        orientationElements.SetActive(true);       //display the orientation bar


        projectileBlockHandler = projectile.GetComponent<BlockHandler>();

        orientationElements.transform.position = projectile.transform.position;        //set the orientation of the bar depending on the projectile position

        projectileDirection = -projectileBlockHandler.projectileDirection;
        projectileDirection.Normalize();

        orientationArrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg); //set the orientation of the bar depending on the projectile aim direction
        clockTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);


        projectileAngle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;        //transforming the aim direction into a degree angle

        maxAngle = projectileAngle + angleAmplitude/2;                                                                  //setting the max and min rotation for the target rotation - start
        minAngle = projectileAngle -angleAmplitude/2;
        highAngleLimit.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);
        lowAngleLimit.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);
        StartCoroutine(CadranStartAnimation());
        maxRotation = new Vector2(Mathf.Cos(maxAngle * Mathf.Deg2Rad), Mathf.Sin(maxAngle * Mathf.Deg2Rad));
        minRotation = new Vector2(Mathf.Cos(minAngle * Mathf.Deg2Rad), Mathf.Sin(minAngle * Mathf.Deg2Rad));
        maxRotation.Normalize();
        minRotation.Normalize();                                                                                        //setting the max and min rotation for the target rotation - end            
        timer = timeBeforeLaunchBack;
        particlesDuringPary.Play();
    }

    public void StopOrientation()                                       
    {
        if(projectile != null)
        {
            projectile.GetComponent<BulletComportement>().isParied = false;
            projectile.GetComponent<BulletComportement>().dmg = projectile.GetComponent<BulletComportement>().launchBackDmg;
            projectileBlockHandler.hasBeenLaunchBack = true;                                                    //tell the projectile he has been paried and launch back (all modification on the projectile are managed on intern)
        }

        particlesDuringPary.Stop();
        particleEndPary.Play();
        orientationElements.SetActive(false);                                                                  //hide the orientation bar
        buttonIsPressed = true;                                                                             //set the security test to prevent probleme with block input back as base
        

        clockCircleDown.fillAmount = 0;
        clockCircleUp.fillAmount = 0;

        GameManager.Instance.ProjectileParyStop();                                                          //set gamestate and time scale value to base
        GetComponentInChildren<PlayerAnimator>().Pary();
        PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().onPary = false;                      //saying that we are not on pary anymore to the script managing shield (for stamina consuming)
    }

    void Rotate()
    {
        float horizontal = Input.GetAxis("AimHorizontalAxis");      
        float vertical = -Input.GetAxis("AimVerticalAxis");

        Quaternion orientationQuaternion = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg)); 

        distancetoMax = Mathf.DeltaAngle(Quaternion.Slerp(orientationArrow.transform.rotation, orientationQuaternion, rotaSpeed * Time.unscaledDeltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(orientationArrow.transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles.z, maxAngle);
        distancetoMin = Mathf.DeltaAngle(Quaternion.Slerp(orientationArrow.transform.rotation, orientationQuaternion, rotaSpeed * Time.unscaledDeltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(orientationArrow.transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles.z, minAngle);

        if (horizontal < -0.15 || horizontal > 0.15 || vertical < -0.15 || vertical > 0.15)
        {
            if (distancetoMax > 0 && distancetoMax < angleAmplitude && distancetoMin < 0 && distancetoMin > -angleAmplitude)
            {
                orientationArrow.transform.rotation = Quaternion.Slerp(orientationArrow.transform.rotation, orientationQuaternion, rotaSpeed * Time.unscaledDeltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(orientationArrow.transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z))));
                clockTransform.rotation = orientationArrow.transform.rotation;
            }
        }

        Vector2 newDir = new Vector2(Mathf.Cos(orientationArrow.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(orientationArrow.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        projectileBlockHandler.projectileDirection = newDir.normalized;
    }

    IEnumerator CadranStartAnimation()
    {
         while (highAngleLimit.transform.rotation != Quaternion.Euler(0, 0, maxAngle))
        {
            highAngleLimit.transform.rotation = Quaternion.Slerp(highAngleLimit.transform.rotation, Quaternion.Euler(0, 0, maxAngle), 0.05f);
            lowAngleLimit.transform.rotation = Quaternion.Slerp(lowAngleLimit.transform.rotation, Quaternion.Euler(0, 0, minAngle), 0.05f);
            yield return new WaitForEndOfFrame();
        }        
    }
}
