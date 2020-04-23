using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather the settings of Lightning
/// </summary>

public class LightningComportement : MonoBehaviour
{
    #region Variables
    [Header ("Fade In settings")]
    public float FadeInTime;
    public Color alphaStart;
    public Color alphaEnd;

    private float timeLeft;

    [HideInInspector] public bool thunderIsSlam;

    [Header("Dammage")]
    [Min(0)]
    [SerializeField] private float dmgLightning = 0;

    private int sameGameObjectList;
    [Space] public List<GameObject> inLightningZone;
    #endregion

    private void Start()
    {
        StartCoroutine(LightningFadeIn(alphaStart, alphaEnd, FadeInTime));

        timeLeft = FadeInTime;
    }

    private void Update()                               
    {
       timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            StartCoroutine(ThunderSlam());

            if (inLightningZone.Count > 0)
            {
                for (int i = 0; i < inLightningZone.Count; i++)
                {
                    inLightningZone[i].GetComponentInParent<BasicHealthSystem>().TakeDmg(dmgLightning);
                    inLightningZone.Remove(inLightningZone[i].gameObject);
                }
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            if (!inLightningZone.Contains(other.gameObject) && other.gameObject.GetComponentInParent<BasicHealthSystem>())                     //if Game Object contain a Health system
            {
                inLightningZone.Add(other.gameObject.GetComponentInParent<BasicHealthSystem>().gameObject);

                for (int i = 0; i < inLightningZone.Count; i++)                                                                                //if Game Object have many hitbox
                {
                    if (other.gameObject.GetComponentInParent<BasicHealthSystem>().gameObject == inLightningZone[i].gameObject.GetComponentInParent<BasicHealthSystem>().gameObject)
                    {
                        sameGameObjectList++;
                    }
                }

                if (sameGameObjectList > 1)                                                                                                     //if Game Object have many hitbox, just take one 
                {
                    inLightningZone.Remove(other.gameObject.GetComponentInParent<BasicHealthSystem>().gameObject);
                }

                sameGameObjectList = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("HitBox")))
        {
            if (inLightningZone.Contains(other.gameObject.GetComponentInParent<BasicHealthSystem>().gameObject))
            {
                inLightningZone.Remove(other.gameObject.GetComponentInParent<BasicHealthSystem>().gameObject);

                sameGameObjectList = 0;
            }
        }
    }

    IEnumerator LightningFadeIn(Color start, Color end, float duration)
    {
        for(float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);

            yield return null;
        }
    }

    IEnumerator ThunderSlam()
    {
        thunderIsSlam = true;
        yield return new WaitForSeconds(0.3f); 
        thunderIsSlam = false;
        Destroy(gameObject);
    }
}
