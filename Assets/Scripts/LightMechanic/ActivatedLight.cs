using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActivatedLight : MonoBehaviour
{
    private bool _turnedOn;
    private Light _light;
    [SerializeField] private float _range = 7;
    

    private void Awake()
    {
        _light = GetComponent<Light>();
        _light.range = _range;
    }

    public void ToggleLight()
    {
        _turnedOn = !_turnedOn;
    }

    private void Update()
    {
        if (_turnedOn)
        {
            _light.range = Mathf.Lerp(_light.range, _range ,Time.deltaTime);
        }
        else
        {
            _light.range = Mathf.Lerp(_light.range, 0 ,Time.deltaTime);
        }
    }
}
