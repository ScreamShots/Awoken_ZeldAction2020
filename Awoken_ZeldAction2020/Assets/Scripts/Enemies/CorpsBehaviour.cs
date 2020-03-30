using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsBehaviour : MonoBehaviour
{
    Animator corpsAnimator;
    AnimatorOverrideController animatorOverrideController;
    [SerializeField]
    AnimationClip corpsAppear = null;
    [SerializeField]
    AnimationClip corpsDisappear = null;    
    [SerializeField]
    bool destroyCorps = false;

    private void Start()
    {
        corpsAnimator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(corpsAnimator.runtimeAnimatorController);
        corpsAnimator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["CorpsAppear"] = corpsAppear;
        animatorOverrideController["CorpsDisappear"] = corpsDisappear;
    }

    private void Update()
    {
        if (destroyCorps)
        {

            corpsAnimator.SetTrigger("Destroy");
            if (corpsAnimator.GetCurrentAnimatorStateInfo(0).IsName("CorpsDisappear"))
            {
                StartCoroutine(DestroyCorps());
            }               
        }       
    }

    IEnumerator DestroyCorps()
    {
        yield return new WaitForSeconds(corpsDisappear.length);
        Destroy(gameObject);
    }
}
