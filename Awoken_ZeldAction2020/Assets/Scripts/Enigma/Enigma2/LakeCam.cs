using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeCam : MonoBehaviour
{
    #region Variables
    LakeDetectionZone detectionZoneScript;

    private bool isBlending = false;
    private bool canTransiBack = false;

    #endregion

    #region Inspector Settings
    [Header("Cams")]
    [Space] public GameObject camArena8 = null;
    public GameObject mermaidArenaCam = null;

    #endregion

    void Start()
    {
        detectionZoneScript = GetComponent<LakeDetectionZone>();
        mermaidArenaCam.SetActive(false);
    }

    void Update()
    {
        if (detectionZoneScript.playerInZone && !isBlending)                         
        {
            isBlending = true;
            camArena8.SetActive(false);
            mermaidArenaCam.SetActive(true);

            detectionZoneScript.playerInZone = false;
        }
        else if (detectionZoneScript.playerInZone && isBlending)
        {
            isBlending = false;
            camArena8.SetActive(true);
            mermaidArenaCam.SetActive(false);

            detectionZoneScript.playerInZone = false;
        }
    }
}
