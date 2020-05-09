using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstWalkInBoss : MonoBehaviour
{
    ProgressionManager progressionManagerScript;

    [SerializeField] DialogueTrigger thisTrigger;

    public Transform BossPlaceFirstWalk;
    public Transform BossPlaceArena;

    private bool dialogueEnd = false;

    private void Start()
    {
        progressionManagerScript = GameManager.Instance.GetComponent<ProgressionManager>();

        if (!progressionManagerScript.firstBattleZeus)
        {
            progressionManagerScript.firstBattleZeus = true;
            thisTrigger.StartDialogue();
        }
    }

    private void Update()
    {
        if (thisTrigger.dialogueStarted)
        {
            BossManager.Instance.gameObject.transform.localPosition = BossPlaceFirstWalk.position;
        }
        if (thisTrigger.dialogueEnded && !dialogueEnd)
        {
            dialogueEnd = true;
            BossManager.Instance.gameObject.transform.localPosition = BossPlaceArena.position;
            SceneManager.LoadScene("MAH_Caverne");
        }

    }
}
