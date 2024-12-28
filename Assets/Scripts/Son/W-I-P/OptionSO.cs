using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/OptionButton")]
public class OptionSO : ScriptableObject
{
    public string buttonText;
    public bool isActive; //whether to create this option at all
    public string nextScenarioID; //whichever scenario this leads to next
    public List<ReactionSO> reactionMessages; //so all the messages that pop up after choosing this option

    public int happinessEVChange;
    public int hotEVChange;
    //Stats like:
        //Attention meter
   


}
