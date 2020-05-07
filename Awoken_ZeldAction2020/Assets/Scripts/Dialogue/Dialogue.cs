using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newDialogue", menuName = "DialogueSystem/New Dialogue ")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct TalkPhase
    {
        public Sprite faceImage;
        [TextArea]
        public string sentence;
    }

    [Header("Dialogue Sentences")]
    public TalkPhase[] talkPhases;
}
