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

    [Header("Change Color")]
    public Color startColor;
    public Color endColor;
    public float speed = 1f;
    private float startTime;

    [Header("Dammage")]
    [Min(0)]
    [SerializeField] private float dmgShockWave = 0;

    [SerializeField]
    [Min(0)]
    float staminaLost = 0;


    private GameObject player;

    private PlayerMovement playerMoveScript;

    private Vector2 direction;
    public enum Direction { up, down, left, right, diagonalUpRight, diagonalUpLeft, diagonalDownRight, diagonalDownLeft }
    [HideInInspector] public Direction watchDirection;

    private PlayerHealthSystem playerHealtScript;

    #endregion

    private void Start()
    {
        player = PlayerManager.Instance.gameObject;

        playerHealtScript = player.GetComponent<PlayerHealthSystem>();

        playerMoveScript = player.GetComponent<PlayerMovement>();

        GetComponent<CircleCollider2D>().enabled = true;

        startTime = 0;

        minScale = transform.localScale;
        StartCoroutine(IncreaseScale(minScale, maxScale, durationToIncrease));
    }

    private void Update()
    {
        if (playerHealtScript.currentHp <= 0)
        {
            Destroy(gameObject);
        }

        if (transform.localScale == maxScale)                                                                                   //To destroy shock wave when max scale is reached
        {
            Destroy(gameObject);
        }

        if (transform.localScale.x >= maxScale.x / 2)
        {
            startTime += Time.deltaTime * speed;
            GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(startColor, endColor, startTime);
        }

        SetDirectionWithDiagonal();
    }

    void SetDirectionWithDiagonal()                                                                                                         
    {
        if(player != null)
        {
            direction = (player.transform.position - transform.position).normalized;                                                //Calculate direction between shock wave && player  
        }                                           

        if (direction.x <= 0.20f && direction.x >= -0.20f && direction.y > 0)
        {
            watchDirection = Direction.up;
        }
        else if (direction.x <= 0.20f && direction.x >= -0.20f && direction.y < 0)
        {
            watchDirection = Direction.down;
        }
        else if (direction.y <= 0.20f && direction.y >= -0.20f && direction.x < 0)
        {
            watchDirection = Direction.left;
        }
        else if (direction.y <= 0.20f && direction.y >= -0.20f && direction.x > 0)
        {
            watchDirection = Direction.right;
        }

        else if (direction.x >= 0.20f && direction.y >= 0.40f)
        {
            watchDirection = Direction.diagonalUpRight;
        }
        else if (direction.x <= 0.20f && direction.y >= 0.40f)
        {
            watchDirection = Direction.diagonalUpLeft;
        }
        else if (direction.x >= 0.20f && direction.y <= -0.40f)
        {
            watchDirection = Direction.diagonalDownRight;
        }
        else if (direction.x <= 0.20f && direction.y <= -0.40f)
        {
            watchDirection = Direction.diagonalDownLeft;
        }     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HitBox" && other.gameObject.transform.root.tag == "Player")
        {
            if(player != null)
            {
                player.GetComponent<PlayerHealthSystem>().TakeDmg(dmgShockWave, transform.position);                                                 //Inflige dmg to Player when shock wave touch the Player
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

                                                                                                                            //cases when Player is on diagonal of ShockWave

            else if (watchDirection == Direction.diagonalUpLeft && (playerMoveScript.watchDirection == PlayerMovement.Direction.right || playerMoveScript.watchDirection == PlayerMovement.Direction.down) && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else if (watchDirection == Direction.diagonalDownLeft && (playerMoveScript.watchDirection == PlayerMovement.Direction.right || playerMoveScript.watchDirection == PlayerMovement.Direction.up) && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else if (watchDirection == Direction.diagonalUpRight && (playerMoveScript.watchDirection == PlayerMovement.Direction.left || playerMoveScript.watchDirection == PlayerMovement.Direction.down) && PlayerStatusManager.Instance.isBlocking)
            {
                player.GetComponent<PlayerShield>().OnElementBlocked(staminaLost);
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else if (watchDirection == Direction.diagonalDownRight && (playerMoveScript.watchDirection == PlayerMovement.Direction.left || playerMoveScript.watchDirection == PlayerMovement.Direction.up) && PlayerStatusManager.Instance.isBlocking)
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
