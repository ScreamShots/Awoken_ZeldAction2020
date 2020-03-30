using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// Class that inherit from -asic health system class and is specification for enemy 
/// </summary>
public class EnemyHealthSystem : BasicHealthSystem
{
    [SerializeField]
    GameObject corps = null;

    /// <summary>
    /// The rest of the Health system behaviour (like maxHp, currentHp or TakeDmg() method are inherited from the basic system class
    /// Even if they are not visible here they are enable and u can call and use them
    /// If u need to modify method from the parent class just class the method preceed by override
    /// If u want to add content to a method but keep all that's on the parent one just overide the method and place the line base.MethodName() in the overide method
    /// </summary>

    public override void Death()
    {
        Instantiate(corps, transform.position, Quaternion.identity);        //Instanciate a corps before destroy the object
        Destroy(gameObject);
    }
}
