using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDitection : MonoBehaviour
{
    private LightPlayer _light;
    private DarkPlayer _dark;

    [SerializeField] private bool _triggeronDarkOnly;
    [SerializeField] private bool _triggeronLightOnly;
    [Space(10)]
    [SerializeField] private UnityEvent _onactivate;

    public void PreformAction()
    {
        _onactivate?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_light == null)
        {
            _light = other.GetComponent<LightPlayer>();
        }
        if(_dark == null)
        {
            _dark = other.GetComponent<DarkPlayer>();
        }

        if(_light != null && !_triggeronDarkOnly && _triggeronLightOnly)
        {
            print("Light only");
            PreformAction();
        }
        else if(_dark != null && _triggeronDarkOnly && !_triggeronLightOnly)
        {
            print("Dark only");
            PreformAction();
        }
        else if(_light != null && _dark != null)
        {
            PreformAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LightPlayer lightPlayer = other.GetComponent<LightPlayer>();
        DarkPlayer darkPlayer = other.GetComponent<DarkPlayer>();

        if (lightPlayer != null)
        {
            _light = null;
        }
        if (darkPlayer != null)
        {
            _dark = null;
        }
    }
}
