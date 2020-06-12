using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerScene : MonoBehaviour
{
    public Image bannerUI = null;
    public Animator bannerAnimator = null;

    [Space] public Sprite[] banner;

    void Start()
    {
        bannerUI.gameObject.SetActive(false);
    }

    [ContextMenu("ShowBannerR1")]
    public void ShowBannerRegion1()         //function just for test with region 1
    {
        ShowBanner(4);
    }

    public void ShowBanner(int sceneIndex)
    {
        if (!bannerUI.gameObject.activeSelf)
        {
            switch (sceneIndex)
            {
                case 1:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[0];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 2:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[1];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 3:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[2];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 4:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[3];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 5:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[4];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 6:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[5];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 7:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[6];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 8:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[7];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 9:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[8];
                    bannerAnimator.SetTrigger("Appear");
                    break;
                case 10:
                    bannerUI.gameObject.SetActive(true);
                    bannerUI.sprite = banner[9];
                    bannerAnimator.SetTrigger("Appear");
                    break;
            }
        }   
    }

    [ContextMenu("Hideanner")]
    public void HideBanner()
    {
        if (bannerUI.gameObject.activeSelf)
        {
            bannerAnimator.SetTrigger("Disappear");
            StartCoroutine(WaitFade());
        }    
    }

    IEnumerator WaitFade()
    {
        yield return new WaitForSeconds(1.1f);
        bannerUI.gameObject.SetActive(false);
    }
}
