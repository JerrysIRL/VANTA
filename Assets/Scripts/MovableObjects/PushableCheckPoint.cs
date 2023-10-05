using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCheckPoint : MonoBehaviour
{
    private Vector3 _resetPosition;
    void Start()
    {
        _resetPosition = transform.position;
    }

    public void ResetBox()
    {
        transform.position = _resetPosition;
    }

}
