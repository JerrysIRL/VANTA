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
    private float _ySpeedLight;
    private bool _moving;

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

        SetYSpeed(_ySpeedLight);
        Move();
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
    }
}