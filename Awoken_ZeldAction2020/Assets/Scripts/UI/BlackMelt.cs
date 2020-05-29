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
    [HideInInspector]
    public Animator blackMeltAnimator;
    [SerializeField]
    GameObject loadingPoulion = null;

    private void Start()
    {
        blackMeltAnimator = GetComponent<Animator>();
        if (onMeltInEnd == null) onMeltInEnd = new UnityEvent();
        if (onMeltOutEnd == null) onMeltOutEnd = new UnityEvent();
    }

    private void OnEnable()
    {
        loadingPoulion.SetActive(false);
    }

    public void MeltIn()
    {
        blackMeltAnimator.SetTrigger("MeltIn");
    }

    public void MeltOut()
    {
        loadingPoulion.SetActive(false);
        blackMeltAnimator.SetTrigger("MeltOut");
    }

    public void EndMeltIn()
    {
        loadingPoulion.SetActive(true);
        onMeltInEnd.Invoke();
        onMeltInEnd.RemoveAllListeners();
    }

    public void EndMeltOut()
    {
        onMeltOutEnd.Invoke();
        onMeltOutEnd.RemoveAllListeners();
    }

    void LoadingPoulionAppear()
    {
        loadingPoulion.SetActive(true);
    }

    void LoadingPoulionDiseappear()
    {
        loadingPoulion.SetActive(false);

    }
}
