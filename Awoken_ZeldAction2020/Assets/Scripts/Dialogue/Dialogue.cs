using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newDialogue", menuName = "DialogueSystem/New Dialogue ")]
public class Dialogue : ScriptableObject
{
    public enum DialogueUIPos {Up, Down};
    [System.Serializable]
    public struct TalkPhase
    {
        public Sprite faceImage;
        [TextArea]
        public string sentence;
    }

    public DialogueUIPos displayPos = DialogueUIPos.Down;
    [Space]
    [Header("Dialogue Sentences")]
    public TalkPhase[] talkPhases;
}
