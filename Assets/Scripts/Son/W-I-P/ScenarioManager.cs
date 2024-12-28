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
    

    [SerializeField] private GameObject textMessageGO;
    [SerializeField] private GameObject playerTextMessageGO;
    [SerializeField] private GameObject optionButtonGO;

    [SerializeField] private Transform phonePanel;
    [SerializeField] private Transform optionPanel;

    [SerializeField] public List<ScenarioSO> scenarioList = new List<ScenarioSO>();
    [SerializeField] public ScenarioSO currentScenario;

    [SerializeField] private List<GameObject> optionButtons; //have max 4 options?
    [SerializeField] private List<GameObject> textMessages; 

    private void Start()
    {
        //0. Find the correct scenario
        StartCoroutine(LoadScenarioCoroutine(FindScenarioWithId("S1")));
    }

    //1.Load the text message for the scenario
    private IEnumerator LoadScenarioCoroutine(ScenarioSO scenario)
    {
        currentScenario = scenario; //just to see current scenario
        yield return StartCoroutine(CreateTextMessageCoroutine(scenario.messageToDisplay));
        CreateOptions(scenario.optionsList);
    }
    private IEnumerator CreateTextMessageCoroutine(string message)
    {
        //1 Create a "writing" animation sprite
        yield return new WaitForSeconds(3f); //delay time
        GameObject msg = Instantiate(textMessageGO, phonePanel);
        textMessages.Add(msg);
        msg.GetComponent<TextMessage>().Setup(message);
    }

    //---To write player text message (different Sprite?)
    private void CreatePlayerTextMessage(string message)
    {
        //
        GameObject msg = Instantiate(playerTextMessageGO, phonePanel);
        textMessages.Add(msg);
        msg.GetComponent<TextMessage>().Setup(message);

    }
    //2. create the options for the scenario
    private void CreateOptions(List<OptionSO> options)
    {
        int index = 0;
        foreach(OptionSO option in options)
        {
            if (option.isActive)
            {

                //Create a listener for the button here to ResolveScenario given the option clicked + replace text
                GameObject btn = optionButtons[index];
                btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = option.buttonText;
                btn.GetComponent<Button>().onClick.AddListener(() => ResolveScenario(option));
                btn.GetComponent<Button>().onClick.AddListener(() => btn.GetComponent<AudioSource>().Play()); //add the play on click sound
                btn.SetActive(true);
                index++;

            }
           
        }
    }

    //3. Resolve scenario once button clicked -> add delays/make into a coroutine
    private void ResolveScenario(OptionSO optionChosen)
    {
        StartCoroutine(ResolveScenarioCoroutine(optionChosen));
    }
    private IEnumerator ResolveScenarioCoroutine(OptionSO optionChosen)
    {
        //3.1 affect attention meter
        if(optionChosen.affectionMeterChange != 0)
        {
            AffectionManager.Instance.ChangeAffection(optionChosen.affectionMeterChange);
        }

        //3.2 Create TextMessage of the player
        CreatePlayerTextMessage(optionChosen.buttonText); //maybe have the actual message be different? (so can have like a drunk scenario?)

        //3.3 clearing option buttons 
        foreach (GameObject button in optionButtons)
        {
            //empty it out and make inactive
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.SetActive(false);
        }

        //3.4 Produce reactionMessages
        foreach (string message in optionChosen.reactionMessages)
        {
            yield return CreateTextMessageCoroutine(message); //waits between each message
        }
        yield return new WaitForSeconds(3f);

        //3.5 Clear Text messages (possibly remove this to have a scroll rect
        foreach (GameObject msg in textMessages)
        {

            Destroy(msg);
        }
        textMessages.Clear();
        
        yield return new WaitForSeconds(5f); //delay between next textMessage (do we need since CTMC has a delay too)

        //3.6 Load next Scenario
        LoadScenarioCoroutine(FindScenarioWithId(optionChosen.nextScenarioID));
       
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
