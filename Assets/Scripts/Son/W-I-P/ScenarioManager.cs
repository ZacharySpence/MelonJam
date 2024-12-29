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

    [Header("Debugging")]
    [SerializeField] float delayBetweenMessages = 1f;
    [SerializeField] float delayToNextScenario = 5f;
    [SerializeField] ReactionSO chosenReaction = null;
    private void Start()
    {
        //0. Find the correct scenario
        StartCoroutine(LoadScenarioCoroutine(FindScenarioWithId("Sc1.0")));
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
        yield return new WaitForSeconds(delayBetweenMessages); //delay time
        GameObject msg = Instantiate(textMessageGO, phonePanel).transform.GetChild(0).gameObject;
        textMessages.Add(msg);
        msg.GetComponent<TextMessage>().Setup(message);
    }

    //---To write player text message (different Sprite?)
    private IEnumerator CreatePlayerTextMessage(string message)
    {
        //
        yield return new WaitForSeconds(delayBetweenMessages);
        GameObject msg = Instantiate(playerTextMessageGO, phonePanel).transform.GetChild(0).gameObject;
        
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
        foreach(GameObject btn in optionButtons)
        {
            btn.GetComponent<Button>().onClick.RemoveAllListeners(); //Remove listeners!
        }
        StartCoroutine(ResolveScenarioCoroutine(optionChosen));
    }
    private IEnumerator ResolveScenarioCoroutine(OptionSO optionChosen)
    {
        Debug.Log("3.1 Affecting Emotional Values");
        //3.1 affect emotional values
        if(optionChosen.hotEVChange != 0)
        {
           EmotionValueManager.Instance.ChangeEmotionValues(optionChosen.hotEVChange,false);
        }
        if (optionChosen.happinessEVChange != 0)
        {
            EmotionValueManager.Instance.ChangeEmotionValues(optionChosen.happinessEVChange,true);
        }

        Debug.Log("3.2 Creating player text messages");
        //3.2 Create TextMessage of the player
        foreach(string msg in optionChosen.actualText)
        {
            yield return CreatePlayerTextMessage(msg); //maybe have the actual message be different? (so can have like a drunk scenario?)
        }


        Debug.Log("3.3 Clearing out buttons");
        //3.3 clearing option buttons 
        foreach (GameObject button in optionButtons)
        {
            //empty it out and make inactive
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            
            button.SetActive(false);
        }

      

        Debug.Log("3.4 Choosing reaction message");
        //3.4 Produce reactionMessages 
        foreach (ReactionSO message in optionChosen.reactionMessages)
        {
            List<string> msgs = new List<string>();
            //Find the correct reaction based off Happiness/Angriness
            if(message.emotionNeeded == EmotionValueManager.Instance.currentAHEmotion || message.emotionNeeded == AHEmotion.Any)
            {
                chosenReaction = message;
                Debug.Log("Chosen Reaction Changed to:" + chosenReaction.name);
                if (EmotionValueManager.Instance.currentHCEmotion == HCEmotion.Hot)
                {
                    msgs = message.hotTextMessages;

                }
                else if (EmotionValueManager.Instance.currentHCEmotion == HCEmotion.Cold)
                {
                    msgs = message.coldTextMessages;
                }
                else
                {
                    msgs = message.neutralTextMessages;
                }

                foreach (string msg in msgs)
                {
                    yield return CreateTextMessageCoroutine(msg); //waits between each message
                }
                break; //should only have one case in each option so no need to get 2 sets of messages!
            }
            
           
            
        }
        yield return new WaitForSeconds(1f);

        Debug.Log("3.5 Clearing Text Messages");
        //3.5 Clear Text messages (possibly remove this to have a scroll rect
        foreach(GameObject text in textMessages)
        {
            Destroy(text);
            
        }
        textMessages.Clear();
        yield return new WaitForSeconds(delayToNextScenario); //delay between next textMessage (do we need since CTMC has a delay too)
        Debug.Log("3.6 Load next Scenario");
        //3.6 Load next Scenario
        Debug.Log(chosenReaction.name);
        Debug.Log(optionChosen.name);
        yield return LoadScenarioCoroutine(FindScenarioWithId(chosenReaction.nextScenarioID));
       
    }


    //Finding scenario via ID
    public ScenarioSO FindScenarioWithId(string id)
    {
       
       foreach(ScenarioSO scenario in scenarioList)
        {
            Debug.Log(id+":"+scenario.ID);
            if (scenario.ID == id)
            {
                return scenario;
            }
        }
        return null;
    }
}
