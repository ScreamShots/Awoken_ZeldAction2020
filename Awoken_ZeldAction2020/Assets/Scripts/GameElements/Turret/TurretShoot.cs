using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretShoot : MonoBehaviour
{
    #region Variables
    [Header("Turret Settings")]
    [Space] 
    
    [SerializeField] 
    float timeBeforeFirstShot = 0;

    [Min(0.8f)] [SerializeField] 
    float timeBtwShots = 0;

    [Header("Bullet initiate")]

    [SerializeField]
    GameObject bullet = null;

    [SerializeField] 
    float bulletSpeed = 0;

    [SerializeField]
    Transform shootPoint = null;

    public bool isActivated = false;

    [SerializeField]
    bool isMovable = false;

    #endregion

    #region HideInInspector Var

    Animator turretAnimator;
    GameElementsHealthSystem hpScript;
    AreaManager linkedAreaManager;
    float timer = 0;
    [HideInInspector]
    public bool isShooting = false;
    bool broken = false;

    #endregion

    private void Start()
    {
        timer = timeBeforeFirstShot;
        turretAnimator = GetComponentInChildren<Animator>();
        hpScript = GetComponent<GameElementsHealthSystem>();
        
        if(GetComponentInParent<AreaManager>() != null)
        {
            linkedAreaManager = GetComponentInParent<AreaManager>();
            linkedAreaManager.allTurrets.Add(gameObject);
        }
        else
        {
            isActivated = true;
        }

        if (isMovable)
        {
            turretAnimator.SetBool("isMovable", true);
        }
    }

    private void Update()
    {
        if (isActivated && !isShooting)
        {
            if(timer <= 0)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        if(hpScript != null)
        {
            if (hpScript.canTakeDmg)
            {
                if (hpScript.currentHp <= hpScript.maxHp / 2 && !broken)
                {
                    turretAnimator.SetFloat("Statut", 1);
                    broken = true;
                }
                else if (hpScript.currentHp > hpScript.maxHp / 2 && broken)
                {
                    turretAnimator.SetFloat("Statut", 0);
                    broken = false;
                }
            }
        }

        if (!isActivated)
        {
            StopAllCoroutines();
            isShooting = false;
            timer = timeBtwShots;
        }
    }    

    IEnumerator Shoot()
    {
        turretAnimator.SetTrigger("Shot");
        isShooting = true;

        if (!isMovable)
        {
            yield return new WaitUntil(() => turretAnimator.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("endShot"));
        }
        else
        {
            yield return new WaitUntil(() => turretAnimator.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("endShot"));
        }


        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = shootPoint.transform.right;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = bulletSpeed;
        EnemyManager.Instance.allProjectile.Add(bulletInstance);


        isShooting = false;
        timer = timeBtwShots;
    }

  
}
