using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MovableTurretSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Shooting Sound")]
    public AudioSource turretShoot;
    [Range(0f, 1f)] public float turretShootVolume = 0.5f;

    [Space]
    [Header("Turret Moving Sound")]
    public AudioSource turretMoving;
    [Range(0f, 1f)] public float turretMovingVolume = 0.5f;

    [Space]
    [Header("Destroy Sound")]
    public AudioClip turretDestroy;
    [Range(0f, 1f)] public float turretDestroyVolume = 0.5f;

    private TurretShoot turretShootScript;
    private GameElementsHealthSystem gameElementHealthSystemScript;
    private CubeToPush cubeToPushScript;
    private PlayerHealthSystem playerHealthScript;

    private bool turretIsShooting = false;
    #endregion

    void Start()
    {
        turretShootScript = GetComponentInParent<TurretShoot>();
        gameElementHealthSystemScript = GetComponentInParent<GameElementsHealthSystem>();
        cubeToPushScript = GetComponentInParent<CubeToPush>();
        gameElementHealthSystemScript.onDead.AddListener(Dead);
        playerHealthScript = PlayerManager.Instance.GetComponent<PlayerHealthSystem>();
    }

    void Update()
    {
        if (playerHealthScript.currentHp >= 0)
        {
            TurretShoot();
        }

        TurretMove();
    }

    void TurretMove()
    {
        if (cubeToPushScript.playerPushing)
        {
            SoundManager.Instance.PlayCubePushed(turretMoving, turretMovingVolume);
        }
        else
        {
            SoundManager.Instance.StopCubePushed(turretMoving);
        }
    }

    void TurretShoot()
    {
        if (turretShootScript.isShooting)
        {
            if (!turretIsShooting)
            {
                turretIsShooting = true;
                SoundManager.Instance.PlaySfx3D(turretShoot, turretShootVolume);
            }
        }
        else
        {
            turretIsShooting = false;
        }
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(turretDestroy, turretDestroyVolume);
    }
}
