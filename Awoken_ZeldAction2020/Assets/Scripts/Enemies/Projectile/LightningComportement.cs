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

    [Header("Target Tag Selection")]
    [SerializeField] private string targetedElement = null;

    [Header("Element in Zone")]
    [Space]

    public List<GameObject> inLightningZone;
    #endregion

    private bool infligeDmg = false;

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

            if (!infligeDmg)
            {
                infligeDmg = true;

                if (inLightningZone.Count > 0)
                {
                    for (int i = 0; i < inLightningZone.Count; i++)
                    {
                        inLightningZone[i].GetComponentInParent<PlayerHealthSystem>().TakeDmg(dmgLightning, transform.position);
                        inLightningZone.Remove(inLightningZone[i].gameObject);
                    }
                }
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "CollisionDetection" && element != null)
            {
                for (int i = 0; i < inLightningZone.Count; i++)                                                                        //for not add twice same element in the list
                {
                    if (inLightningZone[i].gameObject.transform.root.name == element.transform.root.name)
                    {
                        inLightningZone.Remove(element.transform.parent.gameObject);
                    }
                }

                inLightningZone.Add(element.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform != element.transform.root)
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "CollisionDetection" && element != null)
            {
                inLightningZone.Remove(element.transform.parent.gameObject);
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
