using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
///This script is a basicl projectile behaviour created for test
///the projectile following this behaviour will have a target to deal dmg and a direction (from 4 direction logic) that it will follow until destruction.
///This projectile has a deffine life time in seconds, it get destroyed if it's been created since this life time.
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ClassicProjectile : MonoBehaviour
{
    #region HideInInspedctor var Statement

    public enum Direction { up, down, right, left };

    private Rigidbody2D projectileRgb;    
    private BlockHandler blockHandle;

    private Vector2 aimDirection;

    #endregion

    #region SerializeField var Statement

    public Direction projectileOrientation = Direction.up;

    [Header("Stats")]

    [Min(0f)]
    public float speed;
    [SerializeField] [Min(0f)]
    private float lifeTime = 0;
    [SerializeField] [Min(0f)]
    private float dmg = 0;
    [SerializeField] [Min(0f)] [Tooltip("The amount of stamina the player will lost if he block this projectile")]
    private float staminaLoseOnBlock = 0;

    [Header("Target Tag Selection")]

    [SerializeField] private string targetedElement = null;
    [SerializeField] private string[] allTags;

    #endregion

    private void Start()
    {
        projectileRgb = GetComponent<Rigidbody2D>();        // set requiered elements
        blockHandle = GetComponent<BlockHandler>();

        switch (projectileOrientation)                      //deffine the direction the projectile will go following the direction value;
        {
            case Direction.up:
                aimDirection = new Vector2(0, 1);
                break;
            case Direction.down:
                aimDirection = new Vector2(0, -1);
                break;
            case Direction.right:
                aimDirection = new Vector2(1, 0);
                break;
            case Direction.left:
                aimDirection = new Vector2(-1, 0);
                break;
            default:
                break;
        }

        StartCoroutine(TimerDestroy());                     // start life Time Timer
    }
    private void FixedUpdate()              //Usage of fixed update for rgb manipulation
    {
        projectileRgb.velocity = aimDirection * speed * Time.fixedDeltaTime;        //create a movement for the projectile following targeted direction and depending on speed
    }

    private void Update()
    {
        if (blockHandle.isBlocked)          //Testin if the projectile has been blocked
        {
            OnBlocked();                    //Apply behaviour design for the projectile when it's blocked
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;          //store the gameobject involve in the collision for easier reference in the following code

        if(element.transform != element.transform.root)     //security test to prevent low level error caused by detection with object with no parent's transform
        {
            if (element.transform.parent.tag == targetedElement && element.tag == "HitBox")     //if the projectile is in collision with the Hitbox of an element it is targeting
            {
                element.transform.parent.GetComponent<BasicHealthSystem>().TakeDmg(dmg);        //Inflict dmg to the collisioned target element
                Destroy(gameObject);                                                            
            }
        }        
    }

    void OnBlocked()                        //What happen when the projectile is blocked
    {
        PlayerManager.Instance.gameObject.GetComponent<PlayerShield>().OnElementBlocked(staminaLoseOnBlock);        //cause player lost stamina
        Destroy(gameObject);                                                                                        
    }

    IEnumerator TimerDestroy()              //LifeTime Timer (destroy this after the time of the lifeTime is passed
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    [ContextMenu("Refresh Tag List")]
    void RefreshTagList()
    {
        allTags = UnityEditorInternal.InternalEditorUtility.tags;                               //Function to access the tag's List from unity in the inspector even if you are not in PlayMode
    }                                                                                           //To do so just right click on the script name and click on the function name in the menu

}
