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


        if (hpScript.canTakeDmg)
        {
            if(hpScript.currentHp <= hpScript.maxHp / 2 && !broken)
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

    IEnumerator Shoot()
    {
        turretAnimator.SetTrigger("Shot");
        isShooting = true;
        
        yield return new WaitUntil(() => turretAnimator.GetCurrentAnimatorStateInfo(0).tagHash == Animator.StringToHash("endShot"));

        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        bulletInstance.GetComponent<BulletComportement>().aimDirection = shootPoint.transform.right;
        bulletInstance.GetComponent<BulletComportement>().bulletSpeed = bulletSpeed;


        isShooting = false;
        timer = timeBtwShots;
    }

  
}
