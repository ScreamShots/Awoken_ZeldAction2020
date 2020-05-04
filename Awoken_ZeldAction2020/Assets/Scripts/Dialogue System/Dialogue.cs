using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newDialogue", menuName = "DialogueSystem/New Dialogue ")]
public class Dialogue : ScriptableObject
{
    public Sprite faceImage;
    [TextArea]
    public string[] talkPhases;

}
