using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverEndSignal : MonoBehaviour
{
    [SerializeField]
    UnityEvent onAnimationEnd = null;

    public void ActicateAnimationEnd()
    {
        onAnimationEnd.Invoke();
    }
}
