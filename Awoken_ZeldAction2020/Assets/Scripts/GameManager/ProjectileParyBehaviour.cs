using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParyBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject orientationArrow;
    GameObject projectile;
    BlockHandler projectileBlockHandler;

    Vector2 projectileDirection;
    Vector2 maxRotation;
    Vector2 minRotation;

    [SerializeField][Range(0,180)]
    float angleAmplitude;
    float projectileAngle;
    float maxAngle;
    float minAngle;

    float distancetoMax;
    float distancetoMin;

    public bool buttonIsPressed = true;

    [SerializeField]
    float rotaSpeed;

    private void Start()
    {
        orientationArrow.SetActive(false);
    }

    private void Update()
    {        
        if(GameManager.Instance.gameState == GameManager.GameState.ProjectilePary)
        {
            Debug.DrawRay(projectile.transform.localPosition, minRotation, Color.red);
            Debug.DrawRay(projectile.transform.localPosition, maxRotation, Color.red);

            Rotate();

            if(Input.GetButtonUp("Block") && buttonIsPressed)
            {
                buttonIsPressed = false;
            }

            if (Input.GetButtonDown("Block") && buttonIsPressed == false)
            {
                StopOrientation();
            }
        }
    }

    public void LaunchOrientation(GameObject thisProjectile)
    {
        orientationArrow.SetActive(true);

        projectile = thisProjectile;
        projectileBlockHandler = projectile.GetComponent<BlockHandler>();

        orientationArrow.transform.position = projectile.transform.position;

        projectileDirection = -projectileBlockHandler.projectileDirection;
        projectileDirection.Normalize();

        orientationArrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);


        projectileAngle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;

        maxAngle = projectileAngle + angleAmplitude/2;
        minAngle = projectileAngle -angleAmplitude/2;
        maxRotation = new Vector2(Mathf.Cos(maxAngle * Mathf.Deg2Rad), Mathf.Sin(maxAngle * Mathf.Deg2Rad));
        minRotation = new Vector2(Mathf.Cos(minAngle * Mathf.Deg2Rad), Mathf.Sin(minAngle * Mathf.Deg2Rad));
        maxRotation.Normalize();
        minRotation.Normalize();
    }

    public void StopOrientation()
    {
        orientationArrow.SetActive(false);
        buttonIsPressed = true;
        projectileBlockHandler.hasBeenLaunchBack = true;

        GameManager.Instance.ProjectileParyStop();
    }

    void Rotate()
    {
        float horizontal = Input.GetAxis("AimHorizontalAxis");
        float vertical = -Input.GetAxis("AimVerticalAxis");
        Quaternion orientationQuaternion = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg)); ;

        distancetoMax = Mathf.DeltaAngle(Quaternion.Slerp(orientationArrow.transform.rotation, orientationQuaternion, rotaSpeed * Time.fixedUnscaledDeltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(orientationArrow.transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles.z, maxAngle);
        distancetoMin = Mathf.DeltaAngle(Quaternion.Slerp(orientationArrow.transform.rotation, orientationQuaternion, rotaSpeed * Time.fixedUnscaledDeltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(orientationArrow.transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles.z, minAngle);

        if (horizontal < -0.15 || horizontal > 0.15 || vertical < -0.15 || vertical > 0.15)
        {
            if (distancetoMax > 0 && distancetoMax < 120 && distancetoMin < 0 && distancetoMin > -120)
            {
                orientationArrow.transform.rotation = Quaternion.Slerp(orientationArrow.transform.rotation, orientationQuaternion, rotaSpeed * Time.fixedUnscaledDeltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(orientationArrow.transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z))));
            }
        }

        Vector2 newDir = new Vector2(Mathf.Cos(orientationArrow.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(orientationArrow.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        projectileBlockHandler.projectileDirection = newDir.normalized;
    }
}
