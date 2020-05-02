using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public enum Enemies { Minotaure, Pegase, Poulion, Cyclope }

    [SerializeField]
    public GameObject[] allEnemies = null;
    public Dictionary<Enemies, GameObject> enemiesToSpawn = new Dictionary<Enemies, GameObject>();

    [HideInInspector]
    public List<GameObject> allProjectile = new List<GameObject>();

    public GameObject cloud;

    private void Awake()
    {
        #region Make Singletion
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        enemiesToSpawn.Add(Enemies.Minotaure, allEnemies[1]);
        enemiesToSpawn.Add(Enemies.Pegase, allEnemies[2]);
        enemiesToSpawn.Add(Enemies.Poulion, allEnemies[3]);
        enemiesToSpawn.Add(Enemies.Cyclope, allEnemies[0]);

    }

    public void DestroyAllProjectile()
    {
        for (int i = 0; i < allProjectile.Count; i++)
        {
            if (allProjectile[i] != null)
            {
                Instantiate(cloud, allProjectile[i].transform.position, Quaternion.identity);
                Destroy(allProjectile[i]);
            }
        }
        allProjectile = new List<GameObject>();
    }
}
