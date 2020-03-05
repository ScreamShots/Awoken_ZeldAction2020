using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackZone;
    private PlayerMovement playerMoveScript;

    private void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        AttackRotation();

        if (Input.GetButtonDown("Attack"))
        {
           
        }
        
    }

    void AttackRotation()
    {
        switch (playerMoveScript.watchDirection)
        {
            case PlayerMovement.Direction.down:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case PlayerMovement.Direction.up:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case PlayerMovement.Direction.right:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case PlayerMovement.Direction.left:
                attackZone.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }

}
