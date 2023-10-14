using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement tunable properties")] [SerializeField]
    private float topSpeed = 10f;

    [SerializeField] private float distanceThreshold = 1000f;

    [SerializeField] [Tooltip(" 0 for Dark Player" + "\n 1 for Light Player")]
    private int playerIndex;

    [Range(0, 1)] public float moveSmoothTime = 0.25f;
    [Range(0, 1)] public float turnSmoothTime = 0.3f;
    [SerializeField] private float acceleration = 10f;

    protected CharacterController Controller;
    protected Animator Animator;
    protected Vector3 Direction;
    
    private Vector3 _currentMoveVelocity;   
    private Vector3 _moveDampVelocity;
    private Vector2 _moveVectors;
    

    private float _turnSmoothVelocity;
    private float _ySpeed;
    public float _currentTopSpeed;
    private static readonly int CurrentSpeed = Animator.StringToHash("CurrentSpeed");

    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        _moveVectors = Vector2.zero;
        SetTopSpeed();
    }

    public void SetMoveVector(Vector2 vec) // getter from the inputManager for the input vector   
    {
        _moveVectors = new(vec.x, vec.y);
    }

    public Vector2 GetMoveVector() => _moveVectors;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    protected void SetYSpeed(float ySpeed)
    {
        _ySpeed = ySpeed;
    }

    protected void SetTopSpeed(float speed)
    {
        _currentTopSpeed = _currentTopSpeed = Mathf.Lerp(_currentTopSpeed, speed, Time.deltaTime * acceleration);
    }

    protected Vector3 CalculateNextPos(Movement movement)
    {
        Vector3 newPos = transform.position + Direction;
        return newPos;
    }

    protected bool CheckDistance(Vector3 otherPlayer)
    {
        Vector3 newPos = CalculateNextPos(this);
        float dist = Vector3.Distance(newPos, otherPlayer);

        if (dist >= distanceThreshold)
        {
            return false;
        }

        return true;
    }

    protected void SetTopSpeed()
    {
        _currentTopSpeed = Mathf.Lerp(_currentTopSpeed, topSpeed, Time.deltaTime * 10);
    }

    protected void Move(Vector3 otherPlayer)
    {
        Vector3 input = new Vector3(_moveVectors.x, 0, _moveVectors.y);
        Direction = input.normalized;

        if (input.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0f);
        }
        if (!CheckDistance(otherPlayer))
        {
            return;
        }

        _currentMoveVelocity = Vector3.SmoothDamp(_currentMoveVelocity, input * _currentTopSpeed, ref _moveDampVelocity, moveSmoothTime);
        _currentMoveVelocity.y = _ySpeed;
        
        Controller.Move(_currentMoveVelocity * Time.deltaTime);
    }
}