using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLeverAnim : MonoBehaviour
{
    private DistanceLever disLeverAnim;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        disLeverAnim = GetComponentInParent<DistanceLever>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LeverToHit();
        LeverIsHit();
    }

    void LeverToHit()
    {
        if(disLeverAnim.isPressed == false)
        {
            anim.SetBool("isHit", false);
        }
    }

    void LeverIsHit()
    {
        if ( disLeverAnim.isPressed == true)
        {
            anim.SetBool("isHit", true);
        }
    }
}
