using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ClassicProjectile : MonoBehaviour
{
    public enum Direction { up, down, right, left };
    public Direction projectileOrientation = Direction.up;
    [SerializeField] private float speed;
    private Rigidbody2D projectileRgb;
    private Vector2 aimDirection;
    [SerializeField] private float lifeTime;
    [SerializeField] private float dmg;

    [Header("Target Tag Selection")]

    [SerializeField] private string targetedElement;
    [SerializeField] private string[] allTags;

    private void Start()
    {
        projectileRgb = GetComponent<Rigidbody2D>();

        switch (projectileOrientation)
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

        StartCoroutine(TimerDestroy());
    }
    private void FixedUpdate()
    {
        projectileRgb.velocity = aimDirection * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject element = collision.gameObject;

        if (element.transform.parent.tag == targetedElement && element.tag == "HitBox")          
        {
            element.transform.parent.GetComponent<BasicHealthSystem>().TakeDmg(dmg);
            Destroy(gameObject);
        }
    }

    IEnumerator TimerDestroy()
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
