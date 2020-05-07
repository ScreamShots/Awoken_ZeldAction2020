using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newDialogue", menuName = "DialogueSystem/New Dialogue ")]
public class Dialogue : ScriptableObject
{
    [Header("Dialogue Face (Optional)")]
    public Sprite faceImage;

    [Space]

    [Header("Dialogue Sentences")]
    [TextArea]
    public string[] talkPhases;

}
