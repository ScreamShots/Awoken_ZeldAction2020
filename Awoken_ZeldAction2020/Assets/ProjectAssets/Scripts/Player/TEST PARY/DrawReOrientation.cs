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


    void Start()
    {
        projectileDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        projectileDirection.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Acos(projectileDirection.x) * (180 / Mathf.PI) * Mathf.Sign(projectileDirection.y));


        projectileAngle =  Mathf.Acos(projectileDirection.x) * (180 / Mathf.PI) * Mathf.Sign(projectileDirection.y);
        
        if(projectileAngle > 0)
        {
            maxAngle = projectileAngle + 60;
            minAngle = projectileAngle - 60;
        }
        else
        {
            maxAngle = projectileAngle - 60;
            minAngle = projectileAngle + 60;
        }

        if(maxAngle > 180)
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
        }


        Debug.Log(MakeAngleRadiant(maxAngle));
        Debug.Log(MakeAngleRadiant(minAngle));

        maxRotation = new Vector2(Mathf.Cos(MakeAngleRadiant(maxAngle)), Mathf.Sign(maxAngle));
        minRotation = new Vector2(Mathf.Cos(MakeAngleRadiant(minAngle)), Mathf.Sign(minAngle));

        maxRotation.y = Mathf.Sqrt(1 - Mathf.Pow(maxRotation.x, 2)) * Mathf.Sign(maxRotation.y);
        minRotation.y = Mathf.Sqrt(1 - Mathf.Pow(minRotation.x, 2)) * Mathf.Sign(minRotation.y);        

        if(minAngle < 0)
        {
            minAngle = Mathf.Abs(minAngle) + 180;
        }
        if (maxAngle < 0)
        {
            maxAngle = Mathf.Abs(maxAngle) + 180;
        }
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
        Debug.DrawRay(transform.localPosition, minRotation, Color.red);
        Debug.DrawRay(transform.localPosition, maxRotation, Color.yellow);
        Debug.DrawRay(transform.localPosition, projectileDirection, Color.cyan);

        
        if (Input.GetAxis("AimHorizontalAxis") != 0 || Input.GetAxis("AimVerticalAxis") != 0)
        {
            rotationAngle = Mathf.Acos(Input.GetAxis("AimHorizontalAxis"));
            degreeRotationAngle = rotationAngle * (180 / Mathf.PI) * Mathf.Sign(Input.GetAxis("AimVerticalAxis"));

            rotationTarget = Quaternion.Euler(0, 0, degreeRotationAngle);
            Debug.Log(transform.rotation.eulerAngles);
            if (transform.rotation.eulerAngles.z < Mathf.Max(maxAngle,minAngle) && transform.rotation.eulerAngles.z > Mathf.Min(maxAngle, minAngle))
            {
                
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime);
            }
            
           
            
        }
    }
}
