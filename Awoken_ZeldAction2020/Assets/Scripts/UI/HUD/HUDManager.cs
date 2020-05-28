using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance = null;

    [SerializeField]
    GameObject staminaBar = null;
    [SerializeField]
    StaminaBar staminaBarScript = null;
    [SerializeField]
    GameObject furryBar = null;
    [SerializeField]
    FurryBar furryBarScript = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeHUD();
    }

    void InitializeHUD()
    {
        if (!ProgressionManager.Instance.PlayerCapacity["Block"])
        {
            staminaBar.SetActive(false);
            staminaBarScript.enabled = false;
        }

        if (!ProgressionManager.Instance.PlayerCapacity["Charge"])
        {
            furryBar.SetActive(false);
            furryBarScript.enabled = false;
        }
    }

    public void ActivateBlockHUD()
    {
        if(Instance.gameObject != null)
        {
            staminaBar.SetActive(true);
            staminaBarScript.enabled = true;
        }
    }

    public void ActivateChargeHUD()
    {
        if (Instance.gameObject != null)
        {
            furryBar.SetActive(true);
            furryBarScript.enabled = true;
        }
    }

    public void ActivateAllHUD()
    {
        ActivateBlockHUD();
        ActivateChargeHUD();
    }
}
