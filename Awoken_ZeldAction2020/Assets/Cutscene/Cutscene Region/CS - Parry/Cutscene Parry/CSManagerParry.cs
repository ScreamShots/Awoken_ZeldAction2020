using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerParry : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject cinemachineAltar = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        cinemachineAltar.SetActive(false);
        playerUI.SetActive(true);

        SoundManager.Instance.Cutscene(false);

        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.ShieldParyUnlocked;
        ProgressionManager.Instance.maxHp += 20f;
        ProgressionManager.Instance.maxStamina += 10f;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().maxHp = ProgressionManager.Instance.maxHp;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().currentHp = PlayerManager.Instance.GetComponent<PlayerHealthSystem>().maxHp;
        PlayerManager.Instance.GetComponent<PlayerShield>().maxStamina = ProgressionManager.Instance.maxStamina;
        PlayerManager.Instance.GetComponent<PlayerShield>().currentStamina = PlayerManager.Instance.GetComponent<PlayerShield>().maxStamina;
        GameManager.Instance.areaToLoad = 1;
        ProgressionManager.Instance.PlayerCapacity["Pary"] = true;
        PlayerManager.Instance.ActivatePary();
        ProgressionManager.Instance.SaveTheProgression();
    }

    [ContextMenu("StartCutSceneParry")]
    public override void StartCutScene()
    {
        cinemachineAltar.SetActive(true);
        playerUI.SetActive(false);

        SoundManager.Instance.Cutscene(true);

        base.StartCutScene();
    }
}
