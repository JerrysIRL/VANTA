using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushableObjectSound : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isMoving;

    [SerializeField] private UnityEvent _onMoveStart;
    [SerializeField] private UnityEvent _onMoveStop;

    private void Start()
    {
        _rigidbody= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            if (!_isMoving)
            {
                _onMoveStart.Invoke();
            }
            _isMoving= true;
        }
        else
        {
            if (_isMoving) 
            {
                _onMoveStop.Invoke();
            }
            _isMoving= false;
        }
    }
}
