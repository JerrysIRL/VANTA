using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public CheckPoint LightPlayerCheckPoint;
    public CheckPoint ShadowPlayerCheckPoint;
    private PushableCheckPoint[] _pushableCheckPoints;
    private DarkPlayer _darkPlayer;
    private LightPlayer _lightPlayer;
    private bool _fade;
    private float _fadeAlpha;

    [SerializeField] private Image _fadeImage;
    [SerializeField] private int _outroSceneIndex;
    [SerializeField] private float _fadeStep;
    [SerializeField] private AtmosphereScript _ambience;
    [SerializeField] private NearingTheEnd105 _music;

    [ContextMenu("Reset Level")]
    public void ResetLevel()
    {
        print(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        _pushableCheckPoints = FindObjectsOfType<PushableCheckPoint>();
        _darkPlayer = FindObjectOfType<DarkPlayer>();
        _lightPlayer = FindObjectOfType<LightPlayer>();
        _fadeImage.canvasRenderer.SetAlpha(0f);
        _fadeAlpha = 0f;
    }
    

    public void GameOver()
    {
       _fade= true;
    }

    private void LoadMainMenu()
    {
        //Load main menu here
        print("Load main menu");
    }

    public void LoadLastCheckpoint()
    {
        Time.timeScale= 1.0f;

        for (int i = 0; i < _pushableCheckPoints.Length; i++)
        {
            _pushableCheckPoints[i].ResetBox();
        }
        _lightPlayer.KillPlayer(true);
        _darkPlayer.KillPlayer(true);
    }

    public GameObject GetPlayer(Type player)
    {
        if (player == (typeof(LightPlayer)))
        {
            return _lightPlayer.gameObject;
        }
        else if (player == (typeof(DarkPlayer)))
        {
            return _darkPlayer.gameObject;
        }
        return null;
    }

    private void FixedUpdate()
    {
        if (_fade)
        {
            _fadeAlpha += _fadeStep * Time.fixedDeltaTime;
            _fadeImage.canvasRenderer.SetAlpha(_fadeAlpha);

            if (_fadeImage.canvasRenderer.GetAlpha() > 0.9f)
            {
                _music.StopSound();
                _ambience.StopSound();
                SceneManager.LoadScene(_outroSceneIndex);
            }
        }

    }
}