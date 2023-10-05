using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private Canvas _inputSelectCanvas;
    [SerializeField] private GameObject crossMarkP1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //Time.timeScale = 0f;
        _inputSelectCanvas = GetComponentInChildren<Canvas>();
    }

    /*public void Update()
    {
        if (gameObject.transform.childCount == 2)
        {
            _inputSelectCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if (gameObject.transform.childCount == 2)
        {
            crossMarkP1.SetActive(false);
        }
    }*/
}