using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Made by Rémi Sécher
///This script is a root version of a basic health system. 
///Feel free to use it as a base to Make children script that inheritate of this.
/// </summary>

public abstract class BasicHealthSystem : MonoBehaviour
{
    #region SerializeField var Statement
    [Header("Stats")]

    [Min(0)] [Tooltip("max hp value (min:0")]
    public float maxHp;
    [Min(0)] [Tooltip("current hp value (min:0")]
    public float currentHp;
    [Space]
    public bool canTakeDmg = true;
    #endregion

    protected virtual void Start()
    {
        currentHp = maxHp;                              //Initializing base Hp 
    }

    protected virtual void Update()                    //can be override in children class
    {
        if (currentHp <= 0 )
        {
            Death();
        }

    }

    public virtual void TakeDmg(float dmgTaken)         //can be override in children class
    {
        if (canTakeDmg)
        {
            currentHp -= dmgTaken;                      //use this function to Infligt Dmg     
        }
    }

    public virtual void TakeDmg(float dmgTaken, Vector3 sourcePos)      //same methode as upward but with an upcharge for the player; //can be override in children class
    {
        if (canTakeDmg)
        {
            currentHp -= dmgTaken;                      //use this function to Infligt Dmg     
        }
    }

    public virtual void Heal(float healValue)           //can be override in children class
    {
        currentHp += healValue;                         //use this function to Heal
        if (currentHp > maxHp) currentHp = maxHp;
    }

    public virtual void Death()                         //can be override in children class
    {
        Destroy(gameObject);
    }

  
}
