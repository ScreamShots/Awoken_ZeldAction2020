using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyKnockBackCaller : MonoBehaviour
{
    public UnityEvent knockBackCall;

    [HideInInspector]
    public float knockBackStrength;
    [HideInInspector]
    public Vector2 knockBackDir;

    public float knockBackTime = 0;

    Vector2 playerRelativPos;

    public void KnockEnemy(float knockStr, Vector2 chargeDir)
    {
        playerRelativPos = transform.position - PlayerManager.Instance.transform.position;
        knockBackStrength = knockStr;

        if(Mathf.Abs(chargeDir.x) > Mathf.Abs(chargeDir.y))
        {
            knockBackDir = new Vector2(0, Mathf.Sign(playerRelativPos.y));
        }
        else
        {
            knockBackDir = new Vector2(Mathf.Sign(playerRelativPos.x), 0);
        }


        knockBackCall.Invoke();
    }

}
