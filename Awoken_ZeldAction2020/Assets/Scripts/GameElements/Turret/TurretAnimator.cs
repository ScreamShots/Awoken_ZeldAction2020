using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather animations relative to turrets
/// </summary>

public class TurretAnimator : MonoBehaviour
{
    Animator turretAnim;
    TurretShoot turretShootScript;

    private void Start()
    {
        turretAnim = GetComponent<Animator>();
        turretShootScript = GetComponentInParent<TurretShoot>();
    }

    private void Update()
    {
        turretAnim.SetBool("Shoot", turretShootScript.turretIsShooting);
        turretAnim.SetBool("inZone", turretShootScript.inZoneAnim);
        turretAnim.SetBool("Broken", turretShootScript.turretIsBroken);
    }
}
