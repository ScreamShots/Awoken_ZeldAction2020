using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableElement : MonoBehaviour
{
    Animator elementAnimator = null;
    Collider2D elementCollider = null;
    bool isDestroyed = false;

    [SerializeField]
    ChargableElement[] linkedElements = null;

    private void Start()
    {
        elementAnimator = GetComponentInChildren<Animator>();
        elementCollider = GetComponent<Collider2D>();
    }

    public void ChargeDestroy()
    {
        isDestroyed = true;
        elementAnimator.SetTrigger("Destroy");
        elementCollider.enabled = false;

        foreach (ChargableElement element in linkedElements)
        {
            if (!element.isDestroyed)
            {
                element.DestroyTogether();
            }
        }
    }

    public void DestroyTogether()
    {
        isDestroyed = true;
        elementAnimator.SetTrigger("Destroy");
        elementCollider.enabled = false;
    }
}
