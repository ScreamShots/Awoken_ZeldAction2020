using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Spawning Sound")]
    public AudioClip doorOpen;
    [Range(0f, 1f)] public float doorOpenVolume = 0.5f;

    private DoorBehavior doorBehaviorScript;

    private bool isOpen = false;

    #endregion

    void Start()
    {
        doorBehaviorScript = GetComponentInParent<DoorBehavior>();
    }

    void Update()
    {
        OpenDoor();
    }

    void OpenDoor()
    {
        if (doorBehaviorScript.isDoorOpen)
        {
            if (!isOpen)
            {
                isOpen = true;
                SoundManager.Instance.PlaySfx(doorOpen, doorOpenVolume);
            }
        }
        else
        {
            isOpen = false;
        }
    }
}
