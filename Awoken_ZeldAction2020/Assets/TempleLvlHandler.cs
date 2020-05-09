using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleLvlHandler : MonoBehaviour
{
    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject normalCam;
    public GameObject dialogueCam;

    private void Start()
    {
        if (!ProgressionManager.Instance.firstReachTheTemple)
        {
            dialogue1.SetActive(true);
        }
        else if (!ProgressionManager.Instance.secondReachTheTemple)
        {
            dialogue2.SetActive(true);
        }
        else
        {
            normalCam.SetActive(true);
            dialogueCam.SetActive(false);
        }
    }
}
