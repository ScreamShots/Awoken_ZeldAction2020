using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BasicProgressionHandler : MonoBehaviour
{
    ProgressionManager.ProgressionTimeLine currentTL;

    public int numberOfFragmentsInThisScene = 0;
    public int sceneIndex = 1;

    protected virtual void Start()
    {
        currentTL = ProgressionManager.Instance.thisSessionTimeLine;
        InitializeEnv();
    }

    protected void InitializeEnv()
    {
        switch (currentTL)
        {
            case ProgressionManager.ProgressionTimeLine.NewAdventure:
                OnNewAdventure();
                break;
            case ProgressionManager.ProgressionTimeLine.VegeteablesStart:
                OnVegetablesStart();
                break;
            case ProgressionManager.ProgressionTimeLine.VegetablesEnd:
                OnVegetablesEnd();
                break;
            case ProgressionManager.ProgressionTimeLine.ZeusReveal:
                OnZeusReveal();
                break;
            case ProgressionManager.ProgressionTimeLine.TempleFirstEntrance:
                OnTempleFirstEntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.ShieldBlockUnlock:
                OnShieldBlockUnlock();
                break;
            case ProgressionManager.ProgressionTimeLine.CaveOut:
                OnCaveOut();
                break;
            case ProgressionManager.ProgressionTimeLine.TempleSecondEntrance:
                OnTempleSecondEntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorOneStart:
                OnOlympeFloorOneStart();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorOneLREntrance:
                OnOlympeFloorOneLREntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorOneEnd:
                OnOlympeFloorOneEnd();
                break;
            case ProgressionManager.ProgressionTimeLine.SecondRegionEntrance:
                OnSecondRegionEntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.SecondRegionBrazeros:
                OnSecondRegionBrazeros();
                break;
            case ProgressionManager.ProgressionTimeLine.ShieldParyUnlocked:
                OnShieldParyUnlocked();
                break;
            case ProgressionManager.ProgressionTimeLine.SecondRegionOut:
                OnSecondRegionOut();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorTwoStart:
                OnOlympeFloorTwoStart();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorTwoLREntrance:
                OnOlympeFloorTwoLREntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorTwoEnd:
                OnOlympeFloorTwoEnd();
                break;
            case ProgressionManager.ProgressionTimeLine.ThirdRegionEntrance:
                OnThirdRegionEntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.ShieldChargeUnlock:
                OnShieldChargeUnlock();
                break;
            case ProgressionManager.ProgressionTimeLine.ThirdRegionOut:
                OnThirdRegionOut();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorThreeStart:
                OnOlympeFloorThreeStart();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorThreeLREntrance:
                OnOlympeFloorThreeLREntrance();
                break;
            case ProgressionManager.ProgressionTimeLine.OlympeFloorThreeEnded:
                OnOlympeFloorThreeEnded();
                break;
            case ProgressionManager.ProgressionTimeLine.ZeusFightStarted:
                OnZeusFightStarted();
                break;
            case ProgressionManager.ProgressionTimeLine.EndAdventure:
                OnEndAdventure();
                break;
            default:
                OnEndAdventure();
                break;
        }
    }

    protected virtual void OnNewAdventure()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnVegetablesStart()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnVegetablesEnd()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnZeusReveal()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnTempleFirstEntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnShieldBlockUnlock()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnCaveOut()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnTempleSecondEntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorOneStart()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorOneLREntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorOneEnd()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnSecondRegionEntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnSecondRegionBrazeros()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnShieldParyUnlocked()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnSecondRegionOut()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorTwoStart()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorTwoLREntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorTwoEnd()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnThirdRegionEntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnShieldChargeUnlock()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnThirdRegionOut()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorThreeStart()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorThreeLREntrance()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnOlympeFloorThreeEnded()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnZeusFightStarted()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    protected virtual void OnEndAdventure()
    {
        GameManager.Instance.blackMelt.onMeltOutEnd.AddListener(BannerShow);
    }

    public virtual void BannerShow()
    {
        GameManager.Instance.showBanner(sceneIndex);
        StartCoroutine(HideBanner());
    }

    public virtual IEnumerator HideBanner()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.hideBanner();
    }

#if UNITY_EDITOR

    [ContextMenu("Set Fragements ID")]
    public virtual void SetFragmentsID()
    {
        var currentScene = SceneManager.GetActiveScene();
        var allGameObjInScene = currentScene.GetRootGameObjects();

        List<FragmentPickUp> allFragmentInScene = new List<FragmentPickUp>();

        foreach(GameObject sceneObj in allGameObjInScene)
        {
            if (sceneObj.CompareTag("Fragment"))
            {
                allFragmentInScene.Add(sceneObj.GetComponent<FragmentPickUp>());
            }
            else
            {
                if(sceneObj.GetComponentInChildren<FragmentPickUp>(true) != null)
                {
                    foreach(FragmentPickUp fragmentChildren in sceneObj.GetComponentsInChildren<FragmentPickUp>(true))
                    {
                        allFragmentInScene.Add(fragmentChildren);
                    }                    
                }
            }
        }

        for (int i = 0; i < allFragmentInScene.Count; i++)
        {
            Undo.RecordObject(allFragmentInScene[i], "change Fragment instance ID");
            allFragmentInScene[i].fragmentID = i;
        }

        numberOfFragmentsInThisScene = allFragmentInScene.Count;

        foreach (GameObject sceneObj in allGameObjInScene)
        {
            if (sceneObj.CompareTag("GM"))
            {               
                sceneObj.GetComponent<ProgressionManager>().SetFrgValue(numberOfFragmentsInThisScene, currentScene.buildIndex);
            }
        }
        
        Debug.Log("their is " + allFragmentInScene.Count + " in this Scene, dont forget to apply changes in the context Menu");
    }

    [ContextMenu("Apply Modifications")]
    public void ApplyToProgressionManagerPrefab()
    {
        var currentScene = SceneManager.GetActiveScene();
        var allGameObjInScene = currentScene.GetRootGameObjects();

        foreach (GameObject sceneObj in allGameObjInScene)
        {
            if (sceneObj.CompareTag("GM"))
            {
                sceneObj.GetComponent<ProgressionManager>().ApplyToPrefab(currentScene.buildIndex);
            }
        }

        Debug.Log("Modification on fragment managment for this scene are now saved for the project overall");
    }

#endif

}
