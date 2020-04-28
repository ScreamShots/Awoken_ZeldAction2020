using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AreaManager : MonoBehaviour
{
    [HideInInspector]
    public CinemachineVirtualCamera thisAreaCam;

    private void Awake()
    {
        thisAreaCam = GetComponentInChildren<CinemachineVirtualCamera>();
        if (thisAreaCam != null)
        {
            thisAreaCam.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
            thisAreaCam.Follow = PlayerManager.Instance.gameObject.transform;
            thisAreaCam.Priority = 0;
            thisAreaCam.gameObject.SetActive(false);
        }
    }

    public void InitializeFirstCam()
    {
        thisAreaCam.gameObject.SetActive(true);
        thisAreaCam.Priority = 1;
    }

    public void ActivateCam()
    {
        thisAreaCam.gameObject.SetActive(true);
        thisAreaCam.Priority = 1;
    }

    public void DesactivateCam()
    {
        thisAreaCam.gameObject.SetActive(false);
        thisAreaCam.Priority = 0;
    }
}
