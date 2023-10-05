using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject Settings;
    public GameObject LoadGame;
    public GameObject ReturnButton;
    public GameObject AudioSliders;
    public GameObject Controls;
    public GameObject PauseMenu;

    [SerializeField] private string _sceneNameToLoad;

    public void ShowSettings()
    {
        LoadGame.SetActive(false);
        Settings.SetActive(true);
        ReturnButton.SetActive(true);
    }

    public void ShowLoadGame()
    {
        Settings.SetActive(false);
        LoadGame.SetActive(true);
        ReturnButton.SetActive(true);
    }

    public void ShowAudio()
    {
        AudioSliders.SetActive(true);
        Controls.SetActive(false);
        //Graphics.SetActive(false);
    }

    public void ShowControls()
    {
        AudioSliders.SetActive(false);
        Controls.SetActive(true);
        //Graphics.SetActive(false);
    }

    public void Return()
    {
        LoadGame.SetActive(false);
        Settings.SetActive(false);
        ReturnButton.SetActive(false);
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(_sceneNameToLoad);
    }



}
