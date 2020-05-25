using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicProgressionHandler : MonoBehaviour
{
    ProgressionManager.ProgressionTimeLine currentTL;

    protected virtual void Start()
    {
        currentTL = ProgressionManager.Instance.thisSessionTimeLine;
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

    }

    protected virtual void OnVegetablesStart()
    {

    }

    protected virtual void OnVegetablesEnd()
    {

    }

    protected virtual void OnZeusReveal()
    {

    }

    protected virtual void OnTempleFirstEntrance()
    {

    }

    protected virtual void OnShieldBlockUnlock()
    {

    }

    protected virtual void OnCaveOut()
    {

    }

    protected virtual void OnTempleSecondEntrance()
    {

    }

    protected virtual void OnOlympeFloorOneStart()
    {

    }

    protected virtual void OnOlympeFloorOneLREntrance()
    {

    }

    protected virtual void OnOlympeFloorOneEnd()
    {

    }

    protected virtual void OnSecondRegionEntrance()
    {

    }

    protected virtual void OnSecondRegionBrazeros()
    {

    }

    protected virtual void OnShieldParyUnlocked()
    {

    }

    protected virtual void OnSecondRegionOut()
    {

    }

    protected virtual void OnOlympeFloorTwoStart()
    {

    }

    protected virtual void OnOlympeFloorTwoLREntrance()
    {

    }

    protected virtual void OnOlympeFloorTwoEnd()
    {

    }

    protected virtual void OnThirdRegionEntrance()
    {

    }

    protected virtual void OnShieldChargeUnlock()
    {

    }

    protected virtual void OnThirdRegionOut()
    {

    }

    protected virtual void OnOlympeFloorThreeStart()
    {

    }

    protected virtual void OnOlympeFloorThreeLREntrance()
    {

    }

    protected virtual void OnOlympeFloorThreeEnded()
    {

    }

    protected virtual void OnZeusFightStarted()
    {

    }

    protected virtual void OnEndAdventure()
    {

    }

}
