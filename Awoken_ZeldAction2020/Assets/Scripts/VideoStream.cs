using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStream : MonoBehaviour
{
    [SerializeField]
    RawImage myRawImage = null;
    [SerializeField]
    VideoPlayer myVideoPlayer = null;
    [SerializeField]
    AudioSource myAudioSource = null;

    private void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {       
        myVideoPlayer.Prepare();
        yield return new WaitUntil(() => myVideoPlayer.isPrepared);
        SoundManager.Instance.PauseGame(true);
        myRawImage.texture = myVideoPlayer.texture;
        myVideoPlayer.Play();
        myAudioSource.Play();
    }

}
