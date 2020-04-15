using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// Place this Script on the main parent object of an element that could be block by the player
/// </summary>

public class BlockHandler: MonoBehaviour
{
    public bool isBlocked = false;              //Test this bool in other behaviour script of the element to creat a behaviour on blocked
    public bool isParied = false;               //Test this bool in other behaviour script of the element to creat a behaviour on paried
    public bool hasBeenLaunchBack = false;

    //[HideInInspector]
    public Vector2 projectileDirection;
    
}
