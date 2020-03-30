using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// This script is adaptable class that allow the creation of dead corps for every enemy in the game
/// Simple create a nested prefabs of the original prefab holding this class and assigned animation proper to that corps
/// </summary>

public class CorpsBehaviour : MonoBehaviour
{
    #region HideInInspector Var Statement

    Animator corpsAnimator;
    AnimatorOverrideController animatorOverrideController;

    #endregion

    #region Serialize Var Statement

    [Header("Corps Animation")]
    [SerializeField]
    AnimationClip corpsAppear = null;
    [SerializeField]
    AnimationClip corpsDisappear = null; 
    [Space]
    [SerializeField]
    bool destroyCorps = false;

    #endregion

    private void Start()
    {
        corpsAnimator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(corpsAnimator.runtimeAnimatorController);       //We are replacing the base animator by and overide version of it with the two animation replace by the assigned in the inspector one
        corpsAnimator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["CorpsAppear"] = corpsAppear;
        animatorOverrideController["CorpsDisappear"] = corpsDisappear;
    }

    private void Update()
    {
        if (destroyCorps)
        {
            corpsAnimator.SetTrigger("Destroy");
            if (corpsAnimator.GetCurrentAnimatorStateInfo(0).IsName("CorpsDisappear"))          //if destroyed corps is ticked destroye this Go after the disapear animation end
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
