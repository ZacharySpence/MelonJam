using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/OptionButton")]
public class OptionSO : ScriptableObject
{
    public int choiceNumber; //have it 0,1,2,3 for the resolve scenario -> can change into an enum
    public string buttonText;
    public string actualText;
    public bool isActive; //wheter to create this option at all
    public string nextScenarioID; //whichever scenario this leads to next
    public List<string> reactionMessages; //so all the messages that pop up after choosing this option

    //Stats like:
        //Attention meter
    public int affectionMeterChange;


}
