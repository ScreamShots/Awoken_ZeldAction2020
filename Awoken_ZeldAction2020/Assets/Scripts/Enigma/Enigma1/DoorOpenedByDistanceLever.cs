﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenedByDistanceLever : EnigmaTool
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoorDistanceLever();
    }
}
