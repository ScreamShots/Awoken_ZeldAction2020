using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : BasicHealthSystem
{
    [SerializeField]
    GameObject corps = null;

    public override void Death()
    {
        Instantiate(corps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
