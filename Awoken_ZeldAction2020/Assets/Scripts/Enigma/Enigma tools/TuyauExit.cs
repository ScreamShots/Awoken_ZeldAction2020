using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyauExit : MonoBehaviour
{

    public Transform bulletTp;
    public Vector2 direction;

    void Start()
    {
        direction = bulletTp.transform.up;
    }

}
