using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptables/Test")]
public class TestSO : ScriptableObject
{
    public string buttonText;
    public List<string> actualText;
    public bool isActive; //whether to create this option at all

    public List<ReactionSO> reactionMessages; //so all the messages that pop up after choosing this option

    public int happinessEVChange;
    public int hotEVChange;
    public List<string> test1;
    public string test2;
    //Stats like:
    //Attention meter


}
