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

    public float FadeInTime;
    public Color alphaStart;
    public Color alphaEnd;

    private float timeLeft;

    [Min(0)]
    [SerializeField] private float dmgLightning = 0;

    public List<GameObject> inLightningZone;
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
            if (inLightningZone.Count > 0)
            {
                for (int i = 0; i < inLightningZone.Count; i++)
                {
                    Debug.Log("inside");
                    inLightningZone[i].GetComponentInParent<BasicHealthSystem>().TakeDmg(dmgLightning);
                }
            }

            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            if (!inLightningZone.Contains(other.gameObject) && other.gameObject.GetComponentInParent<BasicHealthSystem>())
            {
                inLightningZone.Add(other.gameObject.GetComponentInParent<BasicHealthSystem>().gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("HitBox")))
        {
            if (inLightningZone.Contains(other.gameObject))
            {
                inLightningZone.Remove(other.gameObject);
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
}
