using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    GameObject deathUI = null;
    Animator deathUIAnimator = null;

    [Space]
    [Header("Death Sound")]
    public AudioClip deathJungle;
    [Range(0f, 1f)] public float deathJungleVolume = 0.5f;

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
        SoundManager.Instance.PlayPause(deathJungle, deathJungleVolume);
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
