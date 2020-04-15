using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevTools;

public class DrawReOrientation : MonoBehaviour
{
    public float rotationAngle;
    public Quaternion rotationTarget;
    public Vector2 projectileDirection;
    public float projectileAngle;

    public Vector2 maxRotation;
    public float maxAngle;
    public Vector2 minRotation;
    public float minAngle;
    public float degreeRotationAngle;

    public float objtRot;
    public float rotValue = 0;


    void Start()
    {
        projectileDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        projectileDirection.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);


        projectileAngle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;

        maxAngle = projectileAngle + 60;
        minAngle = projectileAngle - 60;

        /*if (projectileAngle > 0)
        {
            maxAngle = projectileAngle + 60;
            minAngle = projectileAngle - 60;
        }
        else
        {
            maxAngle = projectileAngle - 60;
            minAngle = projectileAngle + 60;
        }

        if (maxAngle > 180)
        {
            float excess = maxAngle - 180;
            maxAngle = -180 + excess;
        }
        else if (maxAngle < -180)
        {
            float excess = maxAngle + 180;
            maxAngle = 180 + excess;
        }

        if (minAngle > 180)
        {
            float excess = minAngle - 180;
            minAngle = -180 + excess;
        }
        else if (minAngle < -180)
        {
            float excess = minAngle + 180;
            minAngle = 180 + excess;
        }*/


        //Debug.Log(MakeAngleRadiant(maxAngle));
        //Debug.Log(MakeAngleRadiant(minAngle));

        maxRotation = new Vector2(Mathf.Cos(MakeAngleRadiant(maxAngle)), Mathf.Sin(maxAngle * Mathf.Deg2Rad));
        minRotation = new Vector2(Mathf.Cos(MakeAngleRadiant(minAngle)), Mathf.Sin(minAngle * Mathf.Deg2Rad));
        maxRotation.Normalize();
        minRotation.Normalize();

        //maxRotation.y = Mathf.Sqrt(1 - Mathf.Pow(maxRotation.x, 2)) * Mathf.Sign(maxRotation.y);
        //minRotation.y = Mathf.Sqrt(1 - Mathf.Pow(minRotation.x, 2)) * Mathf.Sign(minRotation.y);

        /*if (minAngle < 0)
        {
            minAngle = Mathf.Abs(minAngle) + 180;
        }
        if (maxAngle < 0)
        {
            maxAngle = Mathf.Abs(maxAngle) + 180;
        }*/
    }

    void ChangeDir()
    {
        projectileDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        projectileDirection.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);


        projectileAngle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;

        maxAngle = projectileAngle + 60;
        minAngle = projectileAngle - 60;
        maxRotation = new Vector2(Mathf.Cos(MakeAngleRadiant(maxAngle)), Mathf.Sin(maxAngle * Mathf.Deg2Rad));
        minRotation = new Vector2(Mathf.Cos(MakeAngleRadiant(minAngle)), Mathf.Sin(minAngle * Mathf.Deg2Rad));
        maxRotation.Normalize();
        minRotation.Normalize();
    }

    float MakeAngleRadiant(float degreeAngle)
    {
        float radiantAngle;

        radiantAngle = degreeAngle * Mathf.PI;
        radiantAngle = radiantAngle / 180;

        return radiantAngle;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDir();
        }

        Debug.DrawRay(transform.localPosition, minRotation, Color.red);
        Debug.DrawRay(transform.localPosition, maxRotation, Color.yellow);
        Debug.DrawRay(transform.localPosition, projectileDirection, Color.cyan);

        float horizontal = Input.GetAxis("AimHorizontalAxis");
        float vertical = -Input.GetAxis("AimVerticalAxis");
        Quaternion orientationQuaternion;


        if (horizontal < -0.15  || horizontal > 0.15 || vertical < -0.15 || vertical > 0.15)
            {

            orientationQuaternion = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg));
            transform.rotation = Quaternion.Slerp(transform.rotation, orientationQuaternion, 1);
            //gameObject.transform.rotation = orientationQuaternion;
        }

        /*if (Input.GetAxis("AimHorizontalAxis") > 0.1 || Input.GetAxis("AimHorizontalAxis") < -0.1 || Input.GetAxis("AimVerticalAxis") > 0.1 || Input.GetAxis("AimVerticalAxis") < -0.1)


        //if (Input.GetAxis("AimHorizontalAxis") != 0 || Input.GetAxis("AimVerticalAxis") != 0)
        {
            rotationAngle = Mathf.Acos(Input.GetAxis("AimHorizontalAxis"));
            degreeRotationAngle = rotationAngle * (180 / Mathf.PI) * Mathf.Sign(Input.GetAxis("AimVerticalAxis"));


            rotationTarget = Quaternion.Euler(0, 0, degreeRotationAngle);
            if (transform.rotation.eulerAngles.z < Mathf.Max(maxAngle, minAngle) && transform.rotation.eulerAngles.z > Mathf.Min(maxAngle, minAngle))
            {


            }
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, 1);

            if (transform.rotation.eulerAngles.z != degreeRotationAngle)
            {
                if (degreeRotationAngle < 0)
                {
                    degreeRotationAngle = 180 + (180 - Mathf.Abs(degreeRotationAngle));
                    Debug.Log(degreeRotationAngle);
                }
                if (transform.rotation.eulerAngles.z < 0)
                {
                    objtRot = 180 + (180 - Mathf.Abs(transform.rotation.eulerAngles.z));
                }
                else
                {
                    objtRot = transform.rotation.eulerAngles.z;
                }

                if (degreeRotationAngle > objtRot)
                {
                    rotValue = degreeRotationAngle - objtRot;

                    if (rotValue > 180)
                    {


                        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 1);


                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 1);
                    }
                }
                else
                {
                    rotValue = objtRot - degreeRotationAngle;
                    if (rotValue > 180)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 1);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 1);
                    }
                }
            }








        }*/
    }
}
