using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather the pickUp for fragments : you can modify by how much you increment the fragment counter
/// </summary>

public class FragmentPickUp : MonoBehaviour
{
    #region Inspector Settings
    [Space]
    [Header("Stats")]

    [SerializeField]
    [Range(0, 200)]
    int fragmentToAdd = 0;
    #endregion

    PickUpSound pickUpSoundScript = null;

    private void Start()
    {
        pickUpSoundScript = GetComponentInChildren<PickUpSound>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            PlayerManager.fragmentNumber += fragmentToAdd;
            SoundManager.Instance.PlaySfx(pickUpSoundScript.pickUp, pickUpSoundScript.pickUpVolume);
            Destroy(gameObject);
        }
    }
}
