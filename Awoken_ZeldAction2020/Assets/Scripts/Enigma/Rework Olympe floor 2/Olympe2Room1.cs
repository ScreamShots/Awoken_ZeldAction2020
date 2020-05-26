using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olympe2Room1 : EnigmaTool
{
    [SerializeField]
    private ActionLever lever1 = null;
    [SerializeField]
    private ActionLever lever2 = null;
    [SerializeField]
    private ActionLever lever3 = null;
    [SerializeField]
    private DoorBehavior door1 = null;
    private bool state1;
    private bool state2;
    private bool state3;
    protected override void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Lever1Done();
        Lever2Done();
        Lever3Done();
        EnigmaDone();
        OpenDoor();
    }

    void Lever1Done()
    {
        if(lever1.isPressed == true && lever2.isPressed == false && lever3.isPressed == false)
        {
            state1 = true;
        }
        else if (lever1.isPressed == false)
        {
            state1 = false;
        }
    }

    void Lever2Done()
    {
        if (lever2.isPressed == true && state1 == true)
        {
            state2 = true;
        }
        else if (lever2.isPressed == true && state1 == false)
        {
            StartCoroutine(Reset());
        }
    }

    void Lever3Done()
    {
        if(lever3.isPressed == true && state1 == true && state2 == true)
        {
            state3 = true;
        }
        else if (lever3.isPressed == true && state1 == false || lever3.isPressed == true && state2 == false || lever3.isPressed == true && state1 == false && state2 == false)
        {
            StartCoroutine(Reset());
        }
    }

    void EnigmaDone()
    {
        if(state1 == true && state2 == true && state3 == true)
        {
            isEnigmaDone = true;
        }
    }

    void OpenDoor()
    {
        if (isEnigmaDone == true)
        {
            door1.isDoorOpen = true;
        }
        else door1.isDoorOpen = false;
    }

    public void DoTheEnigma()
    {
        isEnigmaDone = true;

        state1 = true;
        state2 = true;
        state3 = true;

        lever1.isPressed = true;
        lever2.isPressed = true;
        lever3.isPressed = true;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);
        lever1.isPressed = false;
        lever2.isPressed = false;
        lever3.isPressed = false;
        state1 = false;
        state2 = false;
        state3 = false;
    }
}
