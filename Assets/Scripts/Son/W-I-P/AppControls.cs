using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppControls : MonoBehaviour
{
    [SerializeField] GameObject HomeScreen, SettingsScreen, MainMenuScreen,MessageScreen;
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
        HomeScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
        SettingsScreen.SetActive(false);
        
    }

    public void OpenMainMenu()
    {
        HomeScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
}
