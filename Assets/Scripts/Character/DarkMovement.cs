
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMovement : Movement
{
    [Header("Dark properties")] [SerializeField]
    private Transform groundCheckTransform;

    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = -5f;
    [SerializeField] private float inAirTopSpeed;
    [SerializeField] private LayerMask _groundCheckIgnore;
    private GameObject _otherPlayer;
    [SerializeField, Tooltip("Radius for tiny sphere at characters feet.")]
    private float groundSphereRadius = 0.12f;
    
    private bool _moving;
    private float _ySpeedDark = 0;
    
    private static readonly int JumpBool = Animator.StringToHash("JumpBool");
    private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");

    private void Awake()
    {
        _otherPlayer = FindObjectOfType<LightMovement>().gameObject;
    }

    void Update()
    {
        ControlTopSpeed();
        ApplyGravity();
        SetYSpeed(_ySpeedDark);
        Move(_otherPlayer.transform.position);
        SetAudioBool();
    }
   

    public void Jump()
    {
        if (Controller.isGrounded)
        {
            _animator.SetTrigger(JumpTrigger);
            _ySpeedDark = jumpHeight;
        }
        
    }

    private bool GroundCheck() => Physics.OverlapSphere(groundCheckTransform.position, groundSphereRadius, _groundCheckIgnore).Length > 1;
    
    public bool GetMovingBool() => _moving;


    private void ControlTopSpeed()
    {
        if (GroundCheck())
        {
            _animator.SetBool(JumpBool, false);
            SetTopSpeed();
        }
        else
        {
            SetTopSpeed(inAirTopSpeed);
            _animator.SetBool(JumpBool, true);
        }
    }

    private void ApplyGravity()
    {
        if (!Controller.isGrounded)
        {
            _ySpeedDark += gravity * Time.deltaTime;
        }
    }
    
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