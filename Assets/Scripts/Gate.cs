using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private Transform _leftTargetPos;
    [SerializeField] private Transform _rightTargetPos;
    [SerializeField] private bool _isMoving;
    [SerializeField] private float _speed;

    [SerializeField] private float _distance;

    [SerializeField] private GameObject _glow;

    [SerializeField] private UnityEvent _onOpen;

    private bool _isOpening;

    private Vector3 _leftStartPos;
    private Vector3 _rightStartPos;

    private bool _isActive;
    private bool _forcedOpen;

    private void Start()
    {
        _leftStartPos = _leftDoor.transform.position;
        _rightStartPos = _rightDoor.transform.position;
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        if (_isOpening)
        {
            if (_glow != null)
            {
                _glow.SetActive(false);
            }

            OpenGate();
        }
        else
        {
            CloseGate();
        }
    }
    [ContextMenu("OPEN")]
    public void ToggleGate()
    {
        print("Toggle gate");
        if (_forcedOpen)
        {
            return;
        }

        _isOpening = !_isOpening;
        _onOpen.Invoke();

        if (!_isActive)
        {
            _isActive = true;
        }
    }

    private void OpenGate()
    {
        _leftDoor.transform.position = Vector3.MoveTowards(_leftDoor.transform.position, _leftTargetPos.position, _speed * Time.fixedDeltaTime);
        _rightDoor.transform.position = Vector3.MoveTowards(_rightDoor.transform.position, _rightTargetPos.position, _speed * Time.fixedDeltaTime);

        if (_leftDoor.transform.position == _rightDoor.transform.position + new Vector3(_leftDoor.transform.position.x, _leftDoor.transform.position.y, _leftDoor.transform.position.z + _distance) && _rightDoor.transform.position == new Vector3(_rightDoor.transform.position.x, _rightDoor.transform.position.y, _rightDoor.transform.position.z + _distance))
        {
            _isActive = false;
        }
    }

    private void CloseGate()
    {
        _leftDoor.transform.position = Vector3.MoveTowards(_leftDoor.transform.position, _leftStartPos, _speed * Time.fixedDeltaTime);
        _rightDoor.transform.position = Vector3.MoveTowards(_rightDoor.transform.position, _rightStartPos, _speed * Time.fixedDeltaTime);

        if(_leftDoor.transform.position == _leftStartPos && _rightDoor.transform.position == _rightStartPos)
        {
            if(_glow != null)
            {
                _glow.SetActive(true);
            }

            _isActive = false;
        }
    }

    public void ForceOpen()
    {
        _forcedOpen = true;
        _isActive = true;
        _isOpening = true;
        OpenGate();
    }
}
