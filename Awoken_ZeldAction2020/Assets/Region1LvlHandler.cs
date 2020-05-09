using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region1LvlHandler : MonoBehaviour
{
    public GameObject Pnj;
    public GameObject vegetablesManager; 
    public GameObject troncAuberge;
    public GameObject[] traceEclaire;
    public GameObject[] legumes;
    public DoorBehavior porteRegion2;
    public DoorBehavior porteRegion3;
    void Start()
    {
        if (ProgressionManager.Instance.zeusRevealCutsceneDone)
        {
            Pnj.SetActive(false);
            vegetablesManager.SetActive(false);
            troncAuberge.SetActive(false);
            foreach(GameObject eclaire in traceEclaire)
            {
                eclaire.SetActive(true);
            }
            foreach(GameObject leg in legumes)
            {
                leg.SetActive(false);
            }
        }
        else
        {
            vegetablesManager.SetActive(true);
            Pnj.SetActive(true);
            troncAuberge.SetActive(true);
            foreach (GameObject eclaire in traceEclaire)
            {
                eclaire.SetActive(false);
            }
        }

        if (ProgressionManager.Instance.transformFirstStatue)
        {
            porteRegion2.isDoorOpen = true;
        }
        else
        {
            porteRegion2.isDoorOpen = false;
        }

        if (ProgressionManager.Instance.transformSecondStatue)
        {
            porteRegion3.isDoorOpen = true;
        }
        else
        {
            porteRegion3.isDoorOpen = false;
        }

    }


}
