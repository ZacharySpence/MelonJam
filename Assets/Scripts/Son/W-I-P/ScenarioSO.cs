using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/Scenario",order = 0)]
public class ScenarioSO : ScriptableObject
{
    public string ID;
    public string messageToDisplay;
    public List<string> messagesToDisplay;
    public List<OptionSO> optionsList;

     
}
