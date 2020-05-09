using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autel : MonoBehaviour
{
    [SerializeField]
    bool canPlayerActivateAutel;
    public bool isAutelActivated;
    [SerializeField]
    InterractionButton xButton;
    [SerializeField]
    AreaManager areaAutel;

    void Start()
    {
        xButton = GetComponentInChildren<InterractionButton>();
    }


    void Update()
    {
        if (canPlayerActivateAutel && Input.GetButtonDown("Interraction") && areaAutel.allEnemyAreDead)
        {
            isAutelActivated = true;
        }
        if (isAutelActivated)
        {
            xButton.HideButton();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (areaAutel.allEnemyAreDead)
            {
                xButton.ShowButton();
            }
            canPlayerActivateAutel = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            canPlayerActivateAutel = false;
            xButton.HideButton();
        }
    }
}
