using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenu : MonoBehaviour
{
    public void GoToRegion()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToOlympe()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToBoss()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
