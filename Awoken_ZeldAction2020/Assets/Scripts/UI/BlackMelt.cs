using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackMelt : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onMeltInEnd = null;
    [HideInInspector]
    public UnityEvent onMeltOutEnd = null;

    public Animator blackMeltAnimator;

    private void Start()
    {
        blackMeltAnimator = GetComponent<Animator>();
        if (onMeltInEnd == null) onMeltInEnd = new UnityEvent();
        if (onMeltOutEnd == null) onMeltOutEnd = new UnityEvent();
    }

    public void MeltIn()
    {
        blackMeltAnimator.SetTrigger("MeltIn");
    }

    public void MeltOut()
    {
        blackMeltAnimator.SetTrigger("MeltOut");
    }

    public void EndMeltIn()
    {
        onMeltInEnd.Invoke();
        onMeltInEnd.RemoveAllListeners();
    }

    public void EndMeltOut()
    {
        onMeltOutEnd.Invoke();
        onMeltOutEnd.RemoveAllListeners();
    }
}
