using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("MoveSection")]
    [SerializeField] private GameObject _obstacleToMove;
    [SerializeField] private Transform _posToMove;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMoving = false;
    private Vector3 _start;
    private bool _movingBack;

    private void Start()
    {
        if(_obstacleToMove == null)
        {
            Debug.LogError("No Object to move in" + this.transform.name);
        }
        _start = transform.position;
    }
    private void Update()
    {
        if (_isMoving)
        {
            MoveObject();
        }
    }
    /// <summary>
    /// Will play particle and destory the Object
    /// </summary>
    public void DestroyObject()
    {
        //Play Particle and wait till its done then destory
        
        Destroy(gameObject);
    }

    /// <summary>
    /// Move the object to _PosToMove if triggered again then it will return to start pos
    /// </summary>
    [ContextMenu("Move")]
    public void MoveObject()
    {
        if (!_isMoving)
        {
            _isMoving = true;
        }

        if (!_movingBack)
        {
            _obstacleToMove.transform.position = Vector3.MoveTowards(_obstacleToMove.transform.position, _posToMove.position, _speed);
            if(_obstacleToMove.transform.position == _posToMove.position)
            {
                _isMoving = false;
                _movingBack = true;
            }
        }

        if (_movingBack)
        {
            _obstacleToMove.transform.position = Vector3.MoveTowards(_obstacleToMove.transform.position, _start, _speed);
            if(_obstacleToMove.transform.position == _start)
            {
                _isMoving = false;
                _movingBack = false;
            }
        }
    }
}
