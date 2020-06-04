using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Made by Antoine
/// This script gather the pickUp for fragments : you can modify by how much you increment the fragment counter
/// </summary>

public class FragmentPickUp : MonoBehaviour
{
    #region Inspector Settings
    [Space]
    [Header("Stats")]

    [SerializeField]
    [Range(0, 200)]
    int fragmentToAdd = 0;
    
    public int fragmentID = 0;
    #endregion

    PickUpSound pickUpSoundScript = null;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 4:
                if (ProgressionManager.Instance.R1Fragments[fragmentID])
                {
                    Destroy(gameObject);
                }
                break;
            case 5:
                if (ProgressionManager.Instance.R2Fragments[fragmentID])
                {
                    Destroy(gameObject);
                }
                break;
            case 6:
                if (ProgressionManager.Instance.R3Fragments[fragmentID])
                {
                    Destroy(gameObject);
                }
                break;
            case 7:
                if (ProgressionManager.Instance.F1Fragments[fragmentID])
                {
                    Destroy(gameObject);
                }
                break;
            case 8:
                if (ProgressionManager.Instance.F2Fragments[fragmentID])
                {
                    Destroy(gameObject);
                }
                break;
            case 9:
                if (ProgressionManager.Instance.F3Fragments[fragmentID])
                {
                    Destroy(gameObject);
                }
                break;
            default:
                Destroy(gameObject);
                break;
        }

        pickUpSoundScript = GetComponentInChildren<PickUpSound>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject detectedElement = collision.gameObject;

        if (detectedElement.tag == "HitBox" && detectedElement.transform.root.gameObject.tag == "Player")
        {
            PlayerManager.fragmentNumber += fragmentToAdd;
            SoundManager.Instance.PlaySfx(pickUpSoundScript.pickUp, pickUpSoundScript.pickUpVolume);

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 4:
                    ProgressionManager.Instance.R1Fragments[fragmentID] = true;
                    break;
                case 5:
                    ProgressionManager.Instance.R2Fragments[fragmentID] = true;
                    break;
                case 6:
                    ProgressionManager.Instance.R3Fragments[fragmentID] = true;
                    break;
                case 7:
                    ProgressionManager.Instance.F1Fragments[fragmentID] = true;
                    break;
                case 8:
                    ProgressionManager.Instance.F2Fragments[fragmentID] = true;
                    break;
                case 9:
                    ProgressionManager.Instance.F3Fragments[fragmentID] = true;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
