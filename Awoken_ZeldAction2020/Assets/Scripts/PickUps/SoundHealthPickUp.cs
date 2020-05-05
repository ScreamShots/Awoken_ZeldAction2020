using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHealthPickUp : MonoBehaviour
{
    private HealthPickUp healthPickUp;

    private bool l_heartIsPicked;
    // Start is called before the first frame update
    void Start()
    {
        healthPickUp = GetComponentInParent<HealthPickUp>();
    }

    private void OnDestroy()
    {
        if(l_heartIsPicked != healthPickUp.isPicked)  //Ajout d'une variable bool heartIsPicked à mettre avant le Destroy(gameObject)
        {
             if(healthPickUp.isPicked == true) 
             {
                 if (healthPickUp.healToRegen == 100)         //nécessite le passage de healToRegen à public
                 {
                     AmbroisiePickUp();
                 }
                 else
                 {
                     HealthPickUp();
                 }
                 l_heartIsPicked = healthPickUp.isPicked;
             }
         }

    }

    void AmbroisiePickUp()
    {
        Debug.Log("Gulp!");
        SoundManager.Instance.Play("PlayerDrinking");
    }

    void HealthPickUp()
    {
        Debug.Log("Heal!");
        SoundManager.Instance.Play("HeartPicked");
    }
}
