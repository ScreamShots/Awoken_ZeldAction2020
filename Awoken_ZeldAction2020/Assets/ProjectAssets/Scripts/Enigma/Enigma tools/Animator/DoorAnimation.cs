using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Bastien Prigent
/// Animation of the doors
/// </summary>
public class DoorAnimation : MonoBehaviour
{
    private DoorBehavior doorAnim;
    private Animator anim;

    void Start()
    {
        doorAnim = GetComponentInParent<DoorBehavior>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        OpenDoor();
        CloseDoor();
    }

    void OpenDoor()
    {
        if(doorAnim.isDoorOpen == true)
        {
            anim.SetBool("isDoorOpen", true);
        }
    }

    void CloseDoor()
    {
        if(doorAnim.isDoorOpen == false)
        {
            anim.SetBool("isDoorOpen", false);
        }
    }
}
