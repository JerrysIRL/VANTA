using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private NearingTheEnd105 _music;
    [SerializeField] private AtmosphereScript _ambience;
    public static PauseScript Current { get; private set; }//singleton for this class. since this uses dontdestroyonload i want to use this here to prevent duplicates of the pause menu
    public static bool GameIsPaused = false;

    public static bool GameIsPausedShowingControls = false;

    //GameObjects
    public GameObject pauseMenuUI;
    public GameObject Buttons;
    public GameObject Settings;
    //public GameObject controlsMenuUI;

    //First selected button when pausing
    public GameObject pauseFirstButton;
    public GameObject optionFirstButton;
    public GameObject soundFirstButton;
    public GameObject controlsFirstButton;

    //Input Action
    public PlayerControls playerControls;
    private InputAction pauseGame;


    public void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        pauseGame = playerControls.UI.PauseGame;
        pauseGame.Enable();
        pauseGame.performed += PauseGame;

        //showControls = playerControls.UI.ShowControls;
        //showControls.Enable();
        //showControls.performed += ShowControls;
    }

    private void OnDisable()
    {
        pauseGame.Disable();
        //showControls.Disable();
    }
    private void Start()
    {
        //making sure there is only one instance of this class active at a time.
        //this
        if (Current != null && Current != this)
        {
            Destroy(this);
        }

        else
        {
            Current = this;
            //DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    public void PauseGame(InputAction.CallbackContext context)
    {
        {
            if (GameIsPaused)   //Resumes game if game is paused and you press escape

            {
                Resume();
            }

            else Pause(); //Pauses the game and shows pause menu
        }
    }


    public void ShowControls(InputAction.CallbackContext context)
    {
        {
            if (GameIsPausedShowingControls)   //Resumes game if game is paused and you press tab
            {
                pauseMenuUI.SetActive(false); //disable pause menu if showing
                Time.timeScale = 1f;
                //GameIsPausedShowingControls = false;
            }

            else //Pauses the game and show controls
            {
                pauseMenuUI.SetActive(false); // disable pause menu if showing
                Time.timeScale = 0f;
                //GameIsPausedShowingControls = true;
            }
        }
    }

    public void Resume()
    {
        //controlsMenuUI.SetActive(false); //Disable controls menu if showing
        pauseMenuUI.SetActive(false); //If game is not paused resume time
        Buttons.SetActive(true);
        Settings.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }
    public void LoadScene()
    {
        Time.timeScale = 1f;
        _music.StopSound();
        _ambience.StopSound();
        SceneManager.LoadScene(0);
    }

    void Pause() //Pauses the game and shows pause menu
    {
        //controlsMenuUI.SetActive(false); //Disable controls menu if showing
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseButton();
    }

    public void PauseButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void OptionsButton()
    {
        //clear selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set new selected Object
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
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

    public void Restart()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
