using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppControls : MonoBehaviour
{
    [SerializeField] GameObject HomeScreen, SettingsScreen, MainMenuScreen,MessageScreen,WinScreen,LoseScreen,RulesScreen;
    [SerializeField] bool isGameStarted;
    [SerializeField] TextMeshProUGUI startResumeGameButtonText;
    public void StartGame()
    {
        MessageScreen.SetActive(true);
        HomeScreen.SetActive(false);
        
        isGameStarted = true;
        
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // If we're in the editor, stop the play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we're in a build, quit the application
        Application.Quit();
#endif
    }

    public void OpenSettings()
    {
        
        HomeScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }

    public void Back()
    {
        MessageScreen.SetActive(false);
        HomeScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
        SettingsScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        RulesScreen.SetActive(false);
    }

    public void OpenMainMenu()
    {
        HomeScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
    public void Win()
    { 
        
        WinScreen.SetActive(true);

    }
    public void Lose()
    {

        LoseScreen.SetActive(true);

    }
    public void Rules()
    { 
        RulesScreen.SetActive(true);
    }
}
