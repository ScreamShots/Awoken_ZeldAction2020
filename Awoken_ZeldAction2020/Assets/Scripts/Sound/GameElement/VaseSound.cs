using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VaseSound : MonoBehaviour
{
    #region Variables
    [Space]
    [Header("Destroy Sound")]
    public AudioClip destroyVase;
    [Range(0f, 1f)] public float destroyVaseVolume = 0.5f;

    private EnemyHealthSystem enemyHealthSystemScript;
    #endregion

    void Start()
    {
        enemyHealthSystemScript = GetComponentInParent<EnemyHealthSystem>();
        enemyHealthSystemScript.onDead.AddListener(Dead);
    }

    void Dead()
    {
        SoundManager.Instance.PlaySfx(destroyVase, destroyVaseVolume);
    }
}
