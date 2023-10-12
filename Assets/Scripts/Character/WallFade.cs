using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WallFade : MonoBehaviour
{
    private float increaseOpac = 6f;
    private float decreaseOpac = 4f;
    private MeshRenderer _mr;
    private bool _isObstructing;
    private float _level = 0f;


    private void Awake()
    {
        _mr = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isObstructing && _level < 1.2f)
        {
            _level += increaseOpac * Time.fixedDeltaTime;
        }
        else if (!_isObstructing && _level >= 0)
        {
            _level -= decreaseOpac * Time.fixedDeltaTime;
        }
        else
        {
            _isObstructing = false;
        }

        SetOpacityLevel();
    }


    public void SetObstruction()
    {
        _isObstructing = true;
    }

    public void SetOpacityLevel()
    {
        _level = Mathf.Clamp(_level, 0, 1.2f);
        foreach (var mat in _mr.materials)
        {
            mat.SetFloat("_Opacity_Fade", _level);
        }
    }
}