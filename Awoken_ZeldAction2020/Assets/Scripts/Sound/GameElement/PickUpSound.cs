using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PickUpSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("PickUp Sound")]
    public AudioClip pickUp;
    [Range(0f, 1f)] public float pickUpVolume = 0.5f;

    [Space] public bool destroyByTime = false;

    #endregion

}
