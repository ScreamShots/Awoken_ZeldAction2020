using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DestructibleStoneSound : MonoBehaviour
{
    #region Variables
    public bool partialEngimaSound = false;
    public bool globalEnigmaSound = false;

    [Space]
    [Header("Destroy Sound")]
    public AudioClip destroySound;
    [Range(0f, 1f)] public float destroySoundVolume = 0.5f;

    [Space]
    [Header("Engima Sound")]
    public AudioClip partialResolve;
    [Range(0f, 1f)] public float partialResolveVolume = 0.5f;

    public AudioClip globalResolve;
    [Range(0f, 1f)] public float globalResolveVolume = 0.5f;

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

                if (partialEngimaSound)
                {
                    SoundManager.Instance.PlaySfx(partialResolve, partialResolveVolume);
                }
                else if (globalEnigmaSound)
                {
                    SoundManager.Instance.PlaySfx(globalResolve, globalResolveVolume);
                }
            }
        }
        else
        {
            blocIsDestroy = false;
        }
    }
}
