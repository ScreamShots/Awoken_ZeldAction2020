using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicScript : MonoBehaviour
{
    private TransitionArena zoneTransitoire;

    private bool l_playerInZone;
    // Start is called before the first frame update
    void Start()
    {
        zoneTransitoire = GetComponentInParent<TransitionArena>();
    }

    // Update is called once per frame
    void Update()
    {
        if(l_playerInZone != zoneTransitoire.playerInZone)
        {
            if(zoneTransitoire.playerInZone == true)
            {
                PlayBossMusic();
            }
            l_playerInZone = zoneTransitoire.playerInZone;
        }
    }

    void PlayBossMusic()
    {
        MusicManager.instance.Play("ZeusMusique");
    }
}
