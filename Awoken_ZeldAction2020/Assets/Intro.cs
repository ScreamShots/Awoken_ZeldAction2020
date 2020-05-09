using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject UI;
    public GameObject Video;

    private void Update()
    {
        if(Input.anyKeyDown && Video.activeInHierarchy)
        {
            Video.SetActive(false);
            UI.SetActive(true);
        }
    }
}
