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

    #region Variables
    private int itemNum;

    private bool alreadyDropItem;
    #endregion

    #region Inspector Settings
    [Header("Drop Position")]
    [SerializeField] private float minDropDistance = 0;
    [SerializeField] private float maxDropDistance = 0;

    [Header("% Drop Chance %")]
    public float dropChanceItem0;
    public float dropChanceItem1;
    public float dropChanceItem2;

    [Space] public GameObject[] DropItemList;
    #endregion

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
        DropItem();                                                   //When enemy PV <= 0, enemy is Destroy so call this function
    }

    private void DropItem()
    {
        float randomNum = Random.Range(0, 101);                       //100% for determining loot chance
        float randomNum1 = Random.Range(0, 101);
        float randomNum2 = Random.Range(0, 101);

        if (randomNum <= dropChanceItem0 & !alreadyDropItem)           //if a random number is inferior to % of dropping item we set in Inspector
        {
            itemNum = 0;
            alreadyDropItem = true;                                   //can't drop 2 items on same enemy

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(transform.position.x + Random.Range(minDropDistance, maxDropDistance), transform.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
        if (randomNum1 <= dropChanceItem1 & !alreadyDropItem)
        {
            itemNum = 1;
            alreadyDropItem = true;

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(transform.position.x + Random.Range(minDropDistance, maxDropDistance), transform.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
        if (randomNum2 <= dropChanceItem2 & !alreadyDropItem)
        {
            itemNum = 2;
            alreadyDropItem = true;

            GameObject itemDrop = Instantiate(DropItemList[itemNum], transform.position, Quaternion.identity);
            itemDrop.transform.position = new Vector2(transform.position.x + Random.Range(minDropDistance, maxDropDistance), transform.position.y + Random.Range(minDropDistance, maxDropDistance));
        }
    }
}
