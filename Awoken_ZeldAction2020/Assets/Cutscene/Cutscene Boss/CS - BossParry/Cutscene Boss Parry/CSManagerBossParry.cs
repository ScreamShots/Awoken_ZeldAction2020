using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossParry : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject cutSceneUI = null;
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;
    [SerializeField]
    GameObject bossUI = null;
    [SerializeField]
    GameObject realBossRendering = null;
    #pragma warning disable CS0414
    [SerializeField]
    AnimationCurve timelineTimeProgression = null;
    #pragma warning restore CS0414

    [Space]
    [Header("Boss Shoot Sound")]
    public AudioClip bossShoot;
    [Range(0f, 1f)] public float bossShootVolume = 0.5f;

    [Space]
    [Header("Parry Sound")]
    public AudioClip parrySound;
    [Range(0f, 1f)] public float parrySoundVolume = 0.5f;

    private bool stopTimeline = false;

    private float curveValue = 1;
    private float curveTime = 0;

    override public void EndOfCutScene()
    {
        zeus.SetActive(false);
        cutSceneUI.SetActive(false);
        //cinemachine.SetActive(false);
        //playerUI.SetActive(true);
        //bossUI.SetActive(true);
        BossManager.Instance.currentHp = 0;

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneBossParry")]
    public override void StartCutScene()
    {
        realBossRendering.SetActive(false);
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        zeus.SetActive(true);

        base.StartCutScene();
    }

    public override void Update()
    {
        base.Update();

        if (stopTimeline)
        {
            if (curveValue > 0)
            {
                curveTime += Time.deltaTime;
                curveValue = timelineTimeProgression.Evaluate(curveTime);
                currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(curveValue);
            }

            if (Input.GetButtonDown("Block"))
            {
                currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
                inQTE = false;
                stopTimeline = false;

                SoundManager.Instance.sfxSource.Stop();
            }
        }
    }

    public void StopTimelineTime()
    {
        stopTimeline = true;
        inQTE = true;

        SoundManager.Instance.PlaySfx(bossShoot, bossShootVolume);
        SoundManager.Instance.PlaySfx(parrySound, parrySoundVolume);
    }
}
