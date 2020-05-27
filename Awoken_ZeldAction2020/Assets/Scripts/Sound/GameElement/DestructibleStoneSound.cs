using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DestructibleStoneSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Destroy Sound")]
    public AudioClip destroySound;
    [Range(0f, 1f)] public float destroySoundVolume = 0.5f;

    private ChargableElement chargeElementScript;

    private bool blocIsDestroy = false;

    #endregion

    void Start()
    {
        chargeElementScript = GetComponentInParent<ChargableElement>();
    }

    void Update()
    {
        BlockDestroyed();
    }

    void BlockDestroyed()
    {
        if (chargeElementScript.isDestroyed)
        {
            if (!blocIsDestroy)
            {
                blocIsDestroy = true;
                SoundManager.Instance.PlaySfx(destroySound, destroySoundVolume);
            }
        }
        else
        {
            blocIsDestroy = false;
        }
    }
}
