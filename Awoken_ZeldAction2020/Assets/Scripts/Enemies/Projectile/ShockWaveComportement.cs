using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script involve the comportement of a schok wave
/// </summary>

public class ShockWaveComportement : MonoBehaviour
{
    #region Variables
    [Header ("Increase Settings")]
    public Vector3 maxScale;
    Vector3 minScale;
    public float speedToIncrease;
    public float durationToIncrease;

    [Header("Dammage")]
    [Min(0)]
    [SerializeField] private float dmgShockWave = 0;

    [SerializeField]
    [Min(0)]
    float staminaLost = 0;


    private GameObject player;

    private PlayerMovement playerMoveScript;

    private Vector2 direction;
    public enum Direction { up, down, left, right }
    [HideInInspector] public Direction watchDirection;

    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        playerMoveScript = player.GetComponent<PlayerMovement>();

        GetComponent<CircleCollider2D>().enabled = true;

        minScale = transform.localScale;
        StartCoroutine(IncreaseScale(minScale, maxScale, durationToIncrease));
    }

    private void Update()
    {
        if (transform.localScale == maxScale)                                                                                   //To destroy shock wave when max scale is reached
        {
            Destroy(gameObject);
        }

        SetDirection();
    }

    void SetDirection()                                                                                                         
    {
        if(player != null)
        {
            direction = (player.transform.position - transform.position).normalized;                                                //Calculate direction between shock wave && player  
        }                                           

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                watchDirection = Direction.right;
            }
            else
            {
                watchDirection = Direction.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                watchDirection = Direction.up;
            }
            else
            {
                watchDirection = Direction.down;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HitBox" && other.gameObject.transform.root.tag == "Player")
        {
            if(player != null)
            {
                player.GetComponent<PlayerHealthSystem>().TakeDmg(dmgShockWave);                                                 //Inflige dmg to Player when shock wave touch the Player
            }
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (other.tag == "ShieldZone" && other.transform.root.gameObject.tag == "Player")                                   //Detect if the Player blocking in the good direction
        {
            if (watchDirection == Direction.left && playerMoveScript.watchDirection == PlayerMovement.Direction.right && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else if (watchDirection == Direction.right && playerMoveScript.watchDirection == PlayerMovement.Direction.left && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else if (watchDirection == Direction.up && playerMoveScript.watchDirection == PlayerMovement.Direction.down && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else if (watchDirection == Direction.down && playerMoveScript.watchDirection == PlayerMovement.Direction.up && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }

    IEnumerator IncreaseScale(Vector3 a, Vector3 b, float time)
    {
        float i = 0f;
        float rate = (1f / time) * speedToIncrease;

        while(i < 1f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
