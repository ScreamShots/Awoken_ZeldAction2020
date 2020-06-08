using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField]
    int shieldUpgradeCost = 0;
    [SerializeField]
    int healthUpgradeCost = 0;


    [SerializeField]
    Button upgradeShieldButton = null;
    [SerializeField]
    Button upgradeHealthButton = null;

    [SerializeField]
    TextMeshProUGUI buttonTextHealth = null;
    [SerializeField]
    TextMeshProUGUI buttonTextShield = null;

    [SerializeField]
    TextMeshProUGUI errorMessageHealth = null;
    [SerializeField]
    TextMeshProUGUI errorMessageShield = null;

    [SerializeField]
    TextMeshProUGUI numberAvailableFragments = null;
    [SerializeField]
    TextMeshProUGUI numberTotalFragment = null;

    [SerializeField]
    Image healthUpgradeBar = null;
    [SerializeField]
    Image shieldUpgradeBar = null;

    [SerializeField]
    Sprite[] healthUpgradeBarAspect = new Sprite[4];
    [SerializeField]
    Sprite[] shieldUpgradeBarAspect = new Sprite[4];

    [SerializeField]
    GameObject ResumeButton = null;

    private void OnEnable()
    {
        buttonTextHealth.text = "UP ( " + healthUpgradeCost + " )";
        buttonTextShield.text = "UP ( " + shieldUpgradeCost + " )";
        numberAvailableFragments.text = ProgressionManager.Instance.availableFragments.ToString();
        numberTotalFragment.text = ProgressionManager.Instance.totalFragments + " / " + ProgressionManager.Instance.totalFragmentsIG;

        if (ProgressionManager.Instance.availableFragments < healthUpgradeCost)
        {
            upgradeHealthButton.interactable = false;
            errorMessageHealth.gameObject.SetActive(true);
            errorMessageHealth.text = "not enough fragments";
        }
        else if (ProgressionManager.Instance.lvlOfHealUpgrade >= 3)
        {
            upgradeHealthButton.interactable = false;
            errorMessageHealth.gameObject.SetActive(true);
            errorMessageHealth.text = "Max";
        }
        else
        {
            upgradeHealthButton.interactable = true;
            errorMessageHealth.gameObject.SetActive(false);
        }

        if (ProgressionManager.Instance.availableFragments < shieldUpgradeCost)
        {
            upgradeShieldButton.interactable = false;
            errorMessageShield.gameObject.SetActive(true);
            errorMessageShield.text = "not enough fragments";
        }
        else if (ProgressionManager.Instance.lvlOfShieldUpgrade >= 3)
        {
            upgradeShieldButton.interactable = false;
            errorMessageShield.gameObject.SetActive(true);
            errorMessageShield.text = "Max";
        }
        else
        {
            upgradeShieldButton.interactable = true;
            errorMessageShield.gameObject.SetActive(false);
        }

        healthUpgradeBar.sprite = healthUpgradeBarAspect[ProgressionManager.Instance.lvlOfHealUpgrade];

        shieldUpgradeBar.sprite = shieldUpgradeBarAspect[ProgressionManager.Instance.lvlOfShieldUpgrade];

        StartCoroutine(LateOnEnable());

    }

    IEnumerator LateOnEnable()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(ResumeButton);
    }

    public void AugmentHealth()
    {
        ProgressionManager.Instance.lvlOfHealUpgrade += 1;
        ProgressionManager.Instance.availableFragments -= healthUpgradeCost;
        ProgressionManager.Instance.maxHp += 20;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().maxHp += 20;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().currentHp = PlayerManager.Instance.GetComponent<PlayerHealthSystem>().maxHp;

        numberAvailableFragments.text = ProgressionManager.Instance.availableFragments.ToString();
        healthUpgradeBar.sprite = healthUpgradeBarAspect[ProgressionManager.Instance.lvlOfHealUpgrade];

        if (ProgressionManager.Instance.availableFragments < healthUpgradeCost)
        {
            upgradeHealthButton.interactable = false;
            errorMessageHealth.gameObject.SetActive(true);
            errorMessageHealth.text = "not enough fragments";
            EventSystem.current.SetSelectedGameObject(ResumeButton);
        }
        else if (ProgressionManager.Instance.lvlOfHealUpgrade >= 3)
        {
            upgradeHealthButton.interactable = false;
            errorMessageHealth.gameObject.SetActive(true);
            errorMessageHealth.text = "Max";
            EventSystem.current.SetSelectedGameObject(ResumeButton);
        }
        else
        {
            upgradeHealthButton.interactable = true;
            errorMessageHealth.gameObject.SetActive(false);
        }

        if (ProgressionManager.Instance.availableFragments < shieldUpgradeCost)
        {
            upgradeShieldButton.interactable = false;
            errorMessageShield.gameObject.SetActive(true);
            errorMessageShield.text = "not enough fragments";
        }
        else if (ProgressionManager.Instance.lvlOfShieldUpgrade >= 3)
        {
            upgradeShieldButton.interactable = false;
            errorMessageShield.gameObject.SetActive(true);
            errorMessageShield.text = "Max";
        }
        else
        {
            upgradeShieldButton.interactable = true;
            errorMessageShield.gameObject.SetActive(false);
        }
    }

    public void AugmentShield()
    {
        ProgressionManager.Instance.lvlOfShieldUpgrade += 1;
        ProgressionManager.Instance.availableFragments -= shieldUpgradeCost;
        ProgressionManager.Instance.maxHp += 20;
        PlayerManager.Instance.GetComponent<PlayerShield>().maxStamina += 10;
        PlayerManager.Instance.GetComponent<PlayerShield>().currentStamina = PlayerManager.Instance.GetComponent<PlayerShield>().maxStamina;

        numberAvailableFragments.text = ProgressionManager.Instance.availableFragments.ToString();
        shieldUpgradeBar.sprite = shieldUpgradeBarAspect[ProgressionManager.Instance.lvlOfShieldUpgrade];

        if (ProgressionManager.Instance.availableFragments < healthUpgradeCost)
        {
            upgradeHealthButton.interactable = false;
            errorMessageHealth.gameObject.SetActive(true);
            errorMessageHealth.text = "not enough fragments";
        }
        else if (ProgressionManager.Instance.lvlOfHealUpgrade >= 3)
        {
            upgradeHealthButton.interactable = false;
            errorMessageHealth.gameObject.SetActive(true);
            errorMessageHealth.text = "Max";
        }
        else
        {
            upgradeHealthButton.interactable = true;
            errorMessageHealth.gameObject.SetActive(false);
        }

        if (ProgressionManager.Instance.availableFragments < shieldUpgradeCost)
        {
            upgradeShieldButton.interactable = false;
            errorMessageShield.gameObject.SetActive(true);
            errorMessageShield.text = "not enough fragments";
            EventSystem.current.SetSelectedGameObject(ResumeButton);
        }
        else if (ProgressionManager.Instance.lvlOfShieldUpgrade >= 3)
        {
            upgradeShieldButton.interactable = false;
            errorMessageShield.gameObject.SetActive(true);
            errorMessageShield.text = "Max";
            EventSystem.current.SetSelectedGameObject(ResumeButton);
        }
        else
        {
            upgradeShieldButton.interactable = true;
            errorMessageShield.gameObject.SetActive(false);
        }
    }

    public void Resume()
    {
        StartCoroutine(GameManager.Instance.QuitUpgradeMenu());
    }
}
