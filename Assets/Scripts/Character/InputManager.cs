using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // This script records what buttons we are pressing and reference from PlayerControls, our action mapping in the new input system

    private PlayerInput _playerInput;
    private Vector2 _movementInput; // Stores input information, left or right on stick and keyboard
    private Movement _movement;

    private LightPlayer _lightPlayer;
    private DarkMovement _darkMovement;
    private LightMovement _lightMovement;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        var index = _playerInput.playerIndex;
        if (index == 0) // dark should have index = 0
        {
            _darkMovement = FindObjectOfType<DarkMovement>();
        }
        else //light index = 1
        {
            _lightMovement = FindObjectOfType<LightMovement>();
            _lightPlayer = FindObjectOfType<LightPlayer>();
        }

        var movements = FindObjectsOfType<Movement>();
        _movement = movements.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    private void Start()
    {
        var playerManager = FindObjectOfType<PlayerInputManager>();
        transform.SetParent(playerManager.transform);
    }

    public void Burst(InputAction.CallbackContext context)
    {
        if (context.performed && _lightPlayer != null)
        {
            _lightPlayer.BurstLight(true);
            print("Burst preformed");
        }

        if (context.canceled && _lightPlayer != null)
        {
            _lightPlayer.BurstLight(false);
            print("Burst cenceled");
        }
    }

    public void DropLight(InputAction.CallbackContext context)
    {
        if (context.performed && _lightPlayer != null)
        {
            _lightPlayer.LightDropletSpawn();
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && _darkMovement != null)
        {
            _darkMovement.Jump();
        }
    }

    public void GetInputVector(InputAction.CallbackContext context) // getting Input Vector from controllers
    {
        if (_movement == null)
        {
            return;
        }
        _movementInput = context.ReadValue<Vector2>();
        _movement.SetMoveVector(_movementInput);
    }
}