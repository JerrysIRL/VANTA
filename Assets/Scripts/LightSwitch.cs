using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitch : LightElement
{
    [SerializeField] private UnityEvent _onactivate;
    [SerializeField] private UnityEvent _onActivate;

    public override bool EnterLight()
    {
        _onactivate?.Invoke();
        return true;
    }

    public override void ExitLight()
    {

    }
}
