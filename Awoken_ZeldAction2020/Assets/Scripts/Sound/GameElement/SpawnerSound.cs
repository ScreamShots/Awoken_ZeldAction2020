using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpawnerSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Spawning Sound")]
    public AudioClip spawnSound;
    [Range(0f, 1f)] public float spawnSoundVolume = 0.5f;

    [Space]
    [Header("Destroy Sound")]
    public AudioClip spawnerDestroy;
    [Range(0f, 1f)] public float spawnerDestroyVolume = 0.5f;

    private EnemySpawner enemySpawnerScript;
    private GameElementsHealthSystem gameElementHealtSystemScript;

    private bool isSpawning = false;
    #endregion

    void Start()
    {
        enemySpawnerScript = GetComponentInParent<EnemySpawner>();
        gameElementHealtSystemScript = GetComponentInParent<GameElementsHealthSystem>();

        gameElementHealtSystemScript.onDead.AddListener(Dead);
    }

    void Update()
    {
        Spawning();
    }

    void Spawning()
    {
        if (enemySpawnerScript.spawnActivate)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                SoundManager.Instance.PlaySfx(spawnSound, spawnSoundVolume);
            }
        }
        else
        {
            isSpawning = false;
        }
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(spawnerDestroy, spawnerDestroyVolume);
    }
}
