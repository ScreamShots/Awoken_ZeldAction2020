using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject[] allShieldHitZones;
    private Dictionary<string, ShieldHitZone> allShieldZoneScrpit = new Dictionary<string, ShieldHitZone>();
    private PlayerMovement movementScript;
    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        for (int i = 0; i < allShieldHitZones.Length; i++)
        {
            allShieldZoneScrpit.Add(allShieldHitZones[i].name, allShieldHitZones[i].GetComponent<ShieldHitZone>());
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Block"))
        {
            ActivateBlock();
        }

        if (Input.GetButtonUp("Block"))
        {
            DesactivateBlock();
        }
    }

    void ActivateBlock()
    {
        PlayerStatusManager.Instance.isBlocking = true;

        switch (movementScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                allShieldZoneScrpit["Up"].isActivated = true;
                break;
            case PlayerMovement.Direction.down:
                allShieldZoneScrpit["Down"].isActivated = true;
                break;
            case PlayerMovement.Direction.left:
                allShieldZoneScrpit["Left"].isActivated = true;
                break;
            case PlayerMovement.Direction.right:
                allShieldZoneScrpit["Right"].isActivated = true;
                break;
            default:
                break;
        }
    }

    void DesactivateBlock()
    {
        PlayerStatusManager.Instance.needToEndBlock = true;

        switch (movementScript.watchDirection)
        {
            case PlayerMovement.Direction.up:
                allShieldZoneScrpit["Up"].isActivated = false;
                break;
            case PlayerMovement.Direction.down:
                allShieldZoneScrpit["Down"].isActivated = false;
                break;
            case PlayerMovement.Direction.left:
                allShieldZoneScrpit["Left"].isActivated = false;
                break;
            case PlayerMovement.Direction.right:
                allShieldZoneScrpit["Right"].isActivated = false;
                break;
            default:
                break;
        }
    }
}
