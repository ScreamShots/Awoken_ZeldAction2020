using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Antoine
/// This script gather informations relative to enemy drop. You can chose probabilty of dropping item when enemy die
/// </summary>

public class DropSystem : MonoBehaviour
{
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

    private void OnDestroy()
    {
        DropItem();                                                   //When enemy PV <= 0, enemy is Destroy so call this function
    }

    private void DropItem()
    {
        float randomNum = Random.Range(0, 101);                       //100% for determining loot chance
        float randomNum1 = Random.Range(0, 101);
        float randomNum2 = Random.Range(0, 101);

        if(randomNum <= dropChanceItem0 & !alreadyDropItem)           //if a random number is inferior to % of dropping item we set in Inspector
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
