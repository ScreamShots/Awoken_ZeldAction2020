using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Intro;

    private void Update()
    {
        if (Input.anyKeyDown && Intro.activeInHierarchy)
        {
            Intro.SetActive(false);
            Menu.SetActive(true);
        }
    }
}
