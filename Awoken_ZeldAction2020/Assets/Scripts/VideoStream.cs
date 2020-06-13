using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoStream : MonoBehaviour
{
    [SerializeField]
    RawImage myRawImage = null;
    [SerializeField]
    VideoPlayer myVideoPlayer = null;
    [SerializeField]
    AudioSource myAudioSource = null;
    [SerializeField]
    bool playOnStart = true;
    [HideInInspector]
    public UnityEvent onVideoPrepared = null;
    [HideInInspector]
    public UnityEvent onVideoEnd = null;

    private void Awake()
    {
        if(onVideoPrepared == null)
        {
            onVideoPrepared = new UnityEvent();
        }

        if(onVideoEnd == null)
        {
            onVideoEnd = new UnityEvent();
        }
    }

    private void Start()
    {
        if (playOnStart)
        {
            StartCoroutine(PlayVideo());
        }

        myVideoPlayer.loopPointReached += OnVideoEnd;
    }

    public IEnumerator PlayVideo()
    {       
        myVideoPlayer.Prepare();
        yield return new WaitUntil(() => myVideoPlayer.isPrepared);
        onVideoPrepared.Invoke();
        onVideoPrepared.RemoveAllListeners();
        myRawImage.gameObject.SetActive(true);
        SoundManager.Instance.PauseGame(true);
        myRawImage.texture = myVideoPlayer.texture;
        myVideoPlayer.Play();
        myAudioSource.Play();
    }

    public void OnVideoEnd(VideoPlayer source)
    {
        onVideoEnd.Invoke();
        onVideoEnd.RemoveAllListeners();
    }

}
