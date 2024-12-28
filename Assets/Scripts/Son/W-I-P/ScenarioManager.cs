using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{ 
    public static ScenarioManager Instance;

   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this); }  
    }
    [SerializeField] int affectionMeter;

    [SerializeField] private GameObject textMessageGO;
    [SerializeField] private GameObject optionButtonGO;

    [SerializeField] private Transform phonePanel;
    [SerializeField] private Transform optionPanel;

    [SerializeField] public List<ScenarioSO> scenarioList = new List<ScenarioSO>();
    [SerializeField] public ScenarioSO currentScenario;

    [SerializeField] private List<GameObject> optionButtons;
    [SerializeField] private List<GameObject> textMessages; 

    private void Start()
    {
        //0. Find the correct scenario
        LoadScenario(FindScenarioWithId("S1"));
    }

    //1.Load the text message for the scenario
    private void LoadScenario(ScenarioSO scenario)
    {
        currentScenario = scenario; //just to see current scenario
        CreateTextMessage(scenario.messageToDisplay);
        CreateOptions(scenario.optionsList);
    }
    private void CreateTextMessage(string message)
    {
        GameObject msg = Instantiate(textMessageGO, phonePanel);
        textMessages.Add(msg);
        msg.GetComponent<TextMessage>().Setup(message);

    }
    //2. create the options for the scenario
    private void CreateOptions(List<OptionSO> options)
    {
        foreach(OptionSO option in options)
        {
            if (option.isActive)
            {
                GameObject btn = Instantiate(optionButtonGO, optionPanel);
                //Create a listener for the button here to ResolveScenario given the option clicked + replace text
                optionButtons.Add(btn);
                btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = option.buttonText;
                btn.GetComponent<Button>().onClick.AddListener(() => ResolveScenario(option));

            }
           
        }
    }

    //3. Resolve scenario once button clicked -> add delays/make into a coroutine
    private void ResolveScenario(OptionSO optionChosen)
    {
        //3.1 affect attention meter
        affectionMeter += optionChosen.affectionMeterChange;
        //3.2 Produce reactionMessages
        foreach(string message in optionChosen.reactionMessages)
        {
            CreateTextMessage(message);
        }
        //3.3 Clean buttons/text messages (change this)
        foreach(GameObject button in optionButtons)
        {
            
            Destroy(button);
        }
        foreach(GameObject msg in textMessages)
        {
            
            Destroy(msg);
        }
        optionButtons.Clear();
        textMessages.Clear();

        //3.4 Load next Scenario
        LoadScenario(FindScenarioWithId(optionChosen.nextScenarioID));
    }


    //Finding scenario via ID
    public ScenarioSO FindScenarioWithId(string id)
    {
       foreach(ScenarioSO scenario in scenarioList)
        {
            if(scenario.ID == id)
            {
                return scenario;
            }
        }
        return null;
    }
}
