using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LightMovement : Movement
{
    [SerializeField] private float gravity = -2f;
    private GameObject _otherPlayer;
    private float _ySpeedLight;
    private bool _moving;

    private void Awake()
    {
        _otherPlayer = FindObjectOfType<DarkMovement>().gameObject;
    }

    void Update()
    {
        if (!Controller.isGrounded)
        {
            _ySpeedLight = gravity;
        }
        else
        {
            _ySpeedLight = 0;
        }
        SetTopSpeed();
        SetYSpeed(_ySpeedLight);
        Move(_otherPlayer.transform.position);
        SetAudioBool();
    }

    public bool GetMovingBool() => _moving;
    private void SetAudioBool()
    {
        if (GetMoveVector().magnitude > 0.1)
        {
            _moving = true;
        }
        else
        {
            _moving = false;
        }
        Animator.SetFloat("InputFloat", Direction.magnitude);
    }
}