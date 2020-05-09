using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusBigor : MonoBehaviour
{
    public bool isStatueActivated;
    [SerializeField]
    bool canPlayerActivateStatue;
    private InterractionButton xButton;
    [SerializeField]
    private AreaManager areaEnnemies;

    void Start()
    {
        isStatueActivated = false;
        xButton = GetComponentInChildren<InterractionButton>();
    }
    void Update()
    {
        if(canPlayerActivateStatue && Input.GetButtonDown("Interraction") && areaEnnemies.allEnemyAreDead)
        {
            isStatueActivated = true;
        }

        if(isStatueActivated)
        {
            xButton.HideButton();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            canPlayerActivateStatue = true;
            xButton.ShowButton();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            canPlayerActivateStatue = false;
            xButton.HideButton();
        }
    }
}
