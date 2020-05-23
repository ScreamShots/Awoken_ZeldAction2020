using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Made by Antoine
/// Tool for make cutscene easily (transition cam + dialogues) for game elements 
/// </summary>

public class CSTriggerManager : MonoBehaviour
{
    #region Variables

    private float timeToTransition;

    private GameObject camInstantiate;
    
    [HideInInspector] public bool transitionCamFinish = false;
    private bool camExist = false;
    private bool camDestroy = false;
    private bool actionLever = false;
    private bool distanceLever = false;
    private bool stayPlate = false;
    private bool instantPlate = false;
    private bool dialogueExist = false;
    private bool dialogueIsFinish = false;

    ActionLever actionLeverScript;
    DistanceLever distanceLeverScript;
    InstantPressurePlate instantPlateScript;
    StayPressurePlate stayPlateScript;

    #endregion

    #region Inspector Settings
    [Header ("Elements Required")]
    public GameObject elementToActivate;
    public GameObject elementTriggered;
    public DialogueTrigger dialogue;

    [Header ("Camera Settings")]
    [Space] public GameObject camBrain;
    public GameObject camPrefab;
    public float ortographicSizeCam = 4;

    #endregion

    void Start()
    {
        if (elementToActivate.GetComponent<ActionLever>() != null)                                              //depend on which element need to be activate 
        {
            actionLever = true;
            actionLeverScript = elementToActivate.GetComponent<ActionLever>();
        }
        else if (elementToActivate.GetComponent<DistanceLever>() != null)
        {
            distanceLever = true;
            distanceLeverScript = elementToActivate.GetComponent<DistanceLever>();
        }
        else if (elementToActivate.GetComponent<InstantPressurePlate>() != null)
        {
            instantPlate = true;
            instantPlateScript = elementToActivate.GetComponent<InstantPressurePlate>();
        }
        else if (elementToActivate.GetComponent<StayPressurePlate>() != null)
        {
            stayPlate = true;
            stayPlateScript = elementToActivate.GetComponent<StayPressurePlate>();
        }
        
        if(dialogue != null)
        {
            dialogueExist = true;
        }

        camPrefab.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = ortographicSizeCam;        
        timeToTransition = camBrain.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time;                     //get the default tranistion time 
    }

    void Update()
    {
        WhichElement();
        PlayDialogue();  
    }

    void PlayDialogue()
    {
        if (transitionCamFinish)
        {
            if (dialogueExist && !dialogueIsFinish)
            {
                if (!camDestroy)
                {
                    camDestroy = true;
                    //dialogue.
                }
                if (dialogue.dialogueEnded)
                {
                    dialogueIsFinish = true;
                    StartCoroutine(CamDestroy());
                }
            }
            else
            {
                if (!camDestroy)
                {
                    camDestroy = true;
                    StartCoroutine(CamDestroy());
                }
            }
        }
    }

    void WhichElement()
    {
        if (actionLever)
        {
            if (actionLeverScript.isPressed && !camExist)
            {
                camExist = true;
                GameManager.Instance.gameState = GameManager.GameState.Dialogue;
                PlayerMovement.playerRgb.velocity = Vector2.zero;

                camInstantiate = Instantiate(camPrefab, new Vector3(elementTriggered.transform.position.x, elementTriggered.transform.position.y, -1), Quaternion.identity);
                camInstantiate.transform.parent = transform;

                StartCoroutine(TransitionCam());
            }
        }
        else if (distanceLever)
        {
            if (distanceLeverScript.isPressed && !camExist)
            {
                camExist = true;
                GameManager.Instance.gameState = GameManager.GameState.Dialogue;
                PlayerMovement.playerRgb.velocity = Vector2.zero;

                camInstantiate = Instantiate(camPrefab, new Vector3(elementTriggered.transform.position.x, elementTriggered.transform.position.y, -1), Quaternion.identity);
                camInstantiate.transform.parent = transform;

                StartCoroutine(TransitionCam());
            }
        }
        else if (instantPlate)
        {
            if (instantPlateScript.isPressed && !camExist)
            {
                camExist = true;
                GameManager.Instance.gameState = GameManager.GameState.Dialogue;
                PlayerMovement.playerRgb.velocity = Vector2.zero;

                camInstantiate = Instantiate(camPrefab, new Vector3(elementTriggered.transform.position.x, elementTriggered.transform.position.y, -1), Quaternion.identity);
                camInstantiate.transform.parent = transform;

                StartCoroutine(TransitionCam());
            }
        }
        else if (stayPlate)
        {
            if (stayPlateScript.isPressed && !camExist)
            {
                camExist = true;
                GameManager.Instance.gameState = GameManager.GameState.Dialogue;
                PlayerMovement.playerRgb.velocity = Vector2.zero;

                camInstantiate = Instantiate(camPrefab, new Vector3(elementTriggered.transform.position.x, elementTriggered.transform.position.y, -1), Quaternion.identity);
                camInstantiate.transform.parent = transform;

                StartCoroutine(TransitionCam());
            }
        }
    }

    IEnumerator TransitionCam()
    {
        yield return new WaitForSeconds(timeToTransition + 0.5f);
        transitionCamFinish = true;
    }

    IEnumerator CamDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        camInstantiate.SetActive(false);
        
        yield return new WaitForSeconds(timeToTransition);
        GameManager.Instance.gameState = GameManager.GameState.Running;
        Destroy(camInstantiate);
    }
}
