using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAutel3 : MonoBehaviour
{
    public GameObject autel;

    void Update()
    {
        if (autel.GetComponent<Autel>().isAutelActivated)
        {
            ProgressionManager.Instance.unlockCharge = true;
        }
    }
}
