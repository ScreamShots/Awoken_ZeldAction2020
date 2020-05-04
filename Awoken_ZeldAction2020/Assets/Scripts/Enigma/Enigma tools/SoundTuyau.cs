using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons des tuyau.
/// </summary>

public class SoundTuyau : MonoBehaviour
{
    private TuyauEnter scriptTuyau;

    private bool l_projectileEntrer;
    private bool l_projectileSortie;

    // Start is called before the first frame update
    void Start()
    {
        scriptTuyau = GetComponentInParent<TuyauEnter>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ajout d'un bool projectileEntrer dans le script TuyauEntrer
        //Set le bool en "true" avant le début de la coroutine "BulletTrajectory" et après "bullet = other.GameObject()"
        //Set le bool en "false" à la fin de la coroutine "BulletTrajectory"

        /*if(l_projectileEntrer != scriptTuyau.projectileEntrer)
        {
            if(scriptTuyau.projectileEntrer == true)
            {
                ProjectileEntrer();
            }
            l_projectileEntrer = scriptTuyau.projectileEntrer;
        }*/
    }

    void ProjectileEntrer()
    {
        Debug.Log("woosh");
        SoundManager.Instance.Play("TuyauEntrer");
    }
}
