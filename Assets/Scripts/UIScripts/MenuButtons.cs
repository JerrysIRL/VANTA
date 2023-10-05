using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{

    public GameObject optionFirstButton, levelSelectFirstButton, mainMenuFirstButton,soundFirstButton,controlsFirstButton;


    public void Awake()
    {
        mainMenuButton();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int buildIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }

    public void mainMenuButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void SettingsButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
    }

    public void LevelSelectButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(levelSelectFirstButton);
    }

    public void AudioButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(soundFirstButton);
    }

    public void ControlsButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
    }
}
