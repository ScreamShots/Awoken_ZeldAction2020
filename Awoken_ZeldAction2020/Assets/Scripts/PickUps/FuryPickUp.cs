using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryPickUp : MonoBehaviour
{
    #region Inspector Settings
    private PlayerAttack playerAttackScript;

    [Space]
    [Header("Stats")]

    [SerializeField]
    private bool canPickFullFury = false;

    [Space]
    [SerializeField]
    [Range(0, 200)]
    float furyToReload = 0;
    public bool reloadFullFury;

    [Space]
    [Header("Destroy")]

    [SerializeField]
    private bool destroyAfterTime = false;

    [Space]
    [SerializeField]
    [Min(0)]
    float timeBeforeDestroy = 0;
    [SerializeField]
    [Min(0)]
    float destroyTime = 0;
    [SerializeField]
    [Min(0)]
    float maxTimeBtwFlash = 0;

    private float timer;
    private bool isDestroying;
    [HideInInspector]
    public bool isPicked;
    #endregion

    private PickUpSound pickUpSoundScript;

    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            playerAttackScript = PlayerManager.Instance.gameObject.GetComponent<PlayerAttack>();       // getting the player's attack script
        }

        timer = timeBeforeDestroy;

        pickUpSoundScript = GetComponentInChildren<PickUpSound>();
    }

    private void Update()
    {
        if (reloadFullFury)
        {
            furyToReload = playerAttackScript.maxFury;
        }

        if (destroyAfterTime)                                       //if we set that PickUp can be destroy
        {
            timer -= Time.deltaTime;

            if (timer <= 0 && !isDestroying)
            {
                isDestroying = true;
                StartCoroutine(FlashBeforeDestroy());
            }
            else if (isDestroying)
            {
                destroyTime -= Time.deltaTime;

                if (maxTimeBtwFlash < 0.1f)
                {
                    maxTimeBtwFlash = 0.1f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            if (canPickFullFury)
            {
                detectedElement.transform.root.gameObject.GetComponent<PlayerAttack>().RegenFurry(furyToReload);
                Destroy(gameObject);
                SoundManager.Instance.PlaySfx(pickUpSoundScript.pickUp, pickUpSoundScript.pickUpVolume);
            }
            else
            {
                if (detectedElement.transform.root.gameObject.GetComponent<PlayerAttack>().currentFury < detectedElement.transform.root.gameObject.GetComponent<PlayerAttack>().maxFury)
                {
                    detectedElement.transform.root.gameObject.GetComponent<PlayerAttack>().RegenFurry(furyToReload);
                    Destroy(gameObject);
                    SoundManager.Instance.PlaySfx(pickUpSoundScript.pickUp, pickUpSoundScript.pickUpVolume);
                }
            }
        }
    }

    IEnumerator FlashBeforeDestroy()                                //blink PickUp before destroy it
    {
        while (destroyTime > 0)
        {
            yield return new WaitForSeconds(maxTimeBtwFlash);

            maxTimeBtwFlash -= 0.02f;
            GetComponentInChildren<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(maxTimeBtwFlash);

            maxTimeBtwFlash -= 0.02f;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        Destroy(gameObject);
    }
}
