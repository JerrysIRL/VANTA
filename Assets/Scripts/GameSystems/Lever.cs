using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interact
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _glowEffect;


    public void MoveLever()
    {
        _gameObject.transform.Rotate(new Vector3(0,180,70));
        if(_glowEffect != null)
        {
            Destroy(_glowEffect);
        }
    }
}
