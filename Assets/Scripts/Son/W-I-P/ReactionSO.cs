using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/ReactionSO")]
public class ReactionSO: ScriptableObject
{
    public AHEmotion emotionNeeded;
    public List<string> coldTextMessages;
    public List<string> hotTextMessages;
    public List<string> neutralTextMessages;
}