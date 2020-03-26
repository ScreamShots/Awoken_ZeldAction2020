using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve the comportement of a schok wave
/// </summary>

public class ShockWaveComportement : MonoBehaviour
{
    #region Variables
    [Header ("Increase Settings")]
    public Vector3 maxScale;
    Vector3 minScale;
    public float speedToIncrease;
    public float durationToIncrease;

    [Header("Dammage")]
    [Min(0)]
    [SerializeField] private float dmgShockWave = 0;
    private bool canTakeDmg = true;

    private GameObject player;
    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        minScale = transform.localScale;
        StartCoroutine(IncreaseScale(minScale, maxScale, durationToIncrease));
    }

    private void Update()
    {
        if (transform.localScale == maxScale)                                                               //To destroy shock wave when max scale is reached
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox") && other.gameObject.transform.root.CompareTag("Player"))
        {
            if (canTakeDmg && !PlayerStatusManager.Instance.isBlocking)
            {
                canTakeDmg = false;
                player.GetComponent<BasicHealthSystem>().TakeDmg(dmgShockWave);                             //Inflige dmg to Player when shock wave touch the Player
            }

        }
    }

    IEnumerator IncreaseScale(Vector3 a, Vector3 b, float time)
    {
        float i = 0f;
        float rate = (1f / time) * speedToIncrease;

        while(i < 1f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
