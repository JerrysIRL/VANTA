using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement tunable properties")] 
    [SerializeField] private float topSpeed = 10f;
    [SerializeField][Tooltip(" 0 for Dark Player" + "\n 1 for Light Player")] 
    private int playerIndex;

    [Range(0, 1)] public float moveSmoothTime = 0.25f; 
    [Range(0, 1)] public float turnSmoothTime = 0.3f;
    
    protected CharacterController Controller;
    protected Animator _animator;

    private Vector3 _currentMoveVelocity;
    private Vector3 _moveDampVelocity;
    private Vector2 _moveVectors;
    private Vector3 _direction;
    
    private float _turnSmoothVelocity;
    private float _ySpeed;
    public float _currentTopSpeed;
    private static readonly int CurrentSpeed = Animator.StringToHash("CurrentSpeed");

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        _moveVectors = Vector2.zero;
        SetTopSpeed();
    }
    
    public void SetMoveVector(Vector2 vec )  // getter from the inputManager for the input vector   
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
        _currentTopSpeed = speed;
    }

    protected void SetTopSpeed()
    {
        _currentTopSpeed = topSpeed;
    }
    
    protected void Move()
    {
        _direction = new Vector3(_moveVectors.x, 0, _moveVectors.y).normalized; //!
        if (_direction.magnitude >= 0.1f) //!
        {
            _currentMoveVelocity = Vector3.SmoothDamp(
                _currentMoveVelocity, 
                _direction * _currentTopSpeed, 
                ref _moveDampVelocity,
                moveSmoothTime);
            
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y, 
                targetAngle, ref 
                _turnSmoothVelocity,
                turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0, angle, 0f);
        }
        else
        {
            _currentMoveVelocity = Vector3.zero;
        }
        _currentMoveVelocity.y = _ySpeed; //!
        Controller.Move(_currentMoveVelocity * Time.deltaTime); // !
        _animator.SetFloat(CurrentSpeed, _direction.magnitude);
        
    }
}