using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulionMovementReworked : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D poulionRgb;
    private GameObject player;

    Vector2 direction;

    float playerDistance;
    [HideInInspector]
    public bool canMove = true;

    public enum Direction { up, down, left, right }
    //[HideInInspector] 
    public Direction watchDirection = Direction.down;

    //Phase1 - Random Mouvement
    Vector3 startPos;
    [SerializeField] [Min(0)]
    float maxRandomWalkDistance = 0;
    [SerializeField] [Min(0)]
    float stayDuration = 0;
    float stayTimer;
    [SerializeField] [Min(0)]
    float randomMovSpeed = 0;
    bool randomDirSet;
    [SerializeField] [Min(0)]
    float randomWalkDuration = 0;
    float randomWalkTimer;
    [HideInInspector]
    public bool isOnRandomMove;

    //Phase2 - Chase
    [HideInInspector]
    public bool playerDetected;
    [SerializeField] [Min(0)]
    float playerDetectionDistance = 0;
    [SerializeField] [Min(0)]
    float chaseSpeed = 0;
    [SerializeField] [Min(0)]
    float frequency = 0;
    [SerializeField] [Range(1, 10)]
    float minAmplitude = 0;
    [SerializeField] [Range(1, 10)]
    float maxAmplitude = 0;

    //Phase3 - Attack
    [HideInInspector]
    public bool playerInAttackRange;
    [SerializeField] [Min(0)]
    float attackDistance = 0;





    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            player = PlayerManager.Instance.gameObject;
        }

        poulionRgb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        stayTimer = stayDuration;
    }

    private void Update()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;

        if (playerDistance <= attackDistance)
        {
            if (!playerInAttackRange && canMove)
            {
                poulionRgb.velocity = Vector2.zero;
            }
            playerInAttackRange = true;
            playerDetected = false;
        }
        else if (playerDistance <= playerDetectionDistance)
        {
            playerInAttackRange = false;
            playerDetected = true;
        }
        else
        {
            playerInAttackRange = false;
            playerDetected = false;
        }

        SetDirection();

    }

    private void FixedUpdate()
    {
        if (!playerInAttackRange && canMove)
        {
            if (playerDetected)
            {
                Chase();
            }
            else
            {
                if (stayTimer > 0)
                {
                    if(poulionRgb.velocity != Vector2.zero)
                    {
                        poulionRgb.velocity = Vector2.zero;
                    }
                    if (randomDirSet)
                    {
                        randomDirSet = false;
                    }
                    stayTimer -= Time.deltaTime;
                    isOnRandomMove = false;
                }
                else
                {
                    isOnRandomMove = true;
                    RandomMove();
                }
            }
        }
    }

    void RandomMove()
    {
        float distanceFromStartPos = (startPos - transform.position).magnitude;

        if (!randomDirSet)
        {
            if (distanceFromStartPos > maxRandomWalkDistance)
            {
                direction = startPos - transform.position;
                direction.Normalize();
            }
            else
            {
                direction = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                direction.Normalize();
            }
            randomWalkTimer = randomWalkDuration;
            randomDirSet = true;
        }

        if (randomWalkTimer > 0)
        {
            poulionRgb.velocity = direction * randomMovSpeed * Time.fixedDeltaTime;
            randomWalkTimer -= Time.fixedDeltaTime;
        }
        else
        {
            poulionRgb.velocity = Vector2.zero;
            stayTimer = stayDuration;
        }
    }

    void Chase()
    {
        Vector2 playerDirection = player.transform.position - transform.position;
        Vector2 sinRandomMove = Vector2.Perpendicular(playerDirection) * Mathf.Sin(Time.fixedTime * frequency) * Random.Range(minAmplitude, maxAmplitude);
        direction = playerDirection + sinRandomMove;
        direction.Normalize();

        poulionRgb.velocity = direction * chaseSpeed * Time.fixedDeltaTime;
    }

    void SetDirection()
    {
        if (!playerDetected)
        {
            if (poulionRgb.velocity.x != 0 || poulionRgb.velocity.y != 0)
            {
                if (Mathf.Abs(poulionRgb.velocity.x) > Mathf.Abs(poulionRgb.velocity.y))
                {
                    if (poulionRgb.velocity.x > 0)
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
                    if (poulionRgb.velocity.y > 0)
                    {
                        watchDirection = Direction.up;
                    }
                    else
                    {
                        watchDirection = Direction.down;
                    }
                }
            }
        }
        else if (playerDetected)
        {
            Vector2 playerdirection = player.transform.position - transform.position;
            playerdirection.Normalize();

            if (Mathf.Abs(playerdirection.x) > Mathf.Abs(playerdirection.y))
            {
                if (playerdirection.x > 0)
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
                if (playerdirection.y > 0)
                {
                    watchDirection = Direction.up;
                }
                else
                {
                    watchDirection = Direction.down;
                }
            }
        }
        
    }
}
