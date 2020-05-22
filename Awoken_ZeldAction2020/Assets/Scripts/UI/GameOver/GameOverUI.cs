using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    GameObject deathUI = null;
    Animator deathUIAnimator = null;

    private void Start()
    {
        deathUI.SetActive(false);
        deathUIAnimator = deathUI.GetComponent<Animator>();
        deathUIAnimator.enabled = false;
    }

    public void ActiveDeathUI()
    {
        deathUI.SetActive(true);
        deathUIAnimator.enabled = true;
        deathUIAnimator.SetTrigger("Display");
    }

    public void DisableDeathUIAnimator()
    {
        deathUIAnimator.enabled = false;
    }

    public void DesactiveDeathUI()
    {
        deathUI.SetActive(false);
    }
}
