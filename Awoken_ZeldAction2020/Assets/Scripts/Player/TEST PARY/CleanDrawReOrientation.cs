using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDrawReOrientation : MonoBehaviour
{
    Vector2 projectileDirection;
    float projectileAngle;
    public float maxAngle;
    public float minAngle;
    Vector2 maxRotation;
    Vector2 minRotation;
    public float distancetoMax;
    public float distancetoMin;
    public float rotaSpeed;

    // Start is called before the first frame update
    void Start()
    {
        projectileDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        projectileDirection.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);


        projectileAngle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;

        maxAngle = projectileAngle + 60;
        minAngle = projectileAngle - 60;
        maxRotation = new Vector2(Mathf.Cos(maxAngle * Mathf.Deg2Rad), Mathf.Sin(maxAngle * Mathf.Deg2Rad));
        minRotation = new Vector2(Mathf.Cos(minAngle * Mathf.Deg2Rad), Mathf.Sin(minAngle * Mathf.Deg2Rad));
        maxRotation.Normalize();
        minRotation.Normalize();
    }

    // Update is called once per frame
    void Update()
    {


        Debug.DrawRay(transform.localPosition, minRotation, Color.red);
        Debug.DrawRay(transform.localPosition, maxRotation, Color.yellow);
        Debug.DrawRay(transform.localPosition, projectileDirection, Color.cyan);

        Rotate();
    }

    void Rotate()
    {
        float horizontal = Input.GetAxis("AimHorizontalAxis");
        float vertical = -Input.GetAxis("AimVerticalAxis");
        Quaternion orientationQuaternion;
        orientationQuaternion = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg));
        distancetoMax = Mathf.DeltaAngle(Quaternion.Slerp(transform.rotation, orientationQuaternion, rotaSpeed * Time.deltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles.z, maxAngle);
        distancetoMin = Mathf.DeltaAngle(Quaternion.Slerp(transform.rotation, orientationQuaternion, rotaSpeed * Time.deltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles.z, minAngle);

        if (horizontal < -0.15 || horizontal > 0.15 || vertical < -0.15 || vertical > 0.15)
        {
            if(distancetoMax > 0 && distancetoMax < 120 && distancetoMin < 0 && distancetoMin > -120)
            {
                Debug.Log(Quaternion.Slerp(transform.rotation, orientationQuaternion, rotaSpeed * Time.deltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z)))).eulerAngles);
                transform.rotation = Quaternion.Slerp(transform.rotation, orientationQuaternion, rotaSpeed * Time.deltaTime * (1 / Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, orientationQuaternion.eulerAngles.z))));
            }
            
            
        }
    }
}
