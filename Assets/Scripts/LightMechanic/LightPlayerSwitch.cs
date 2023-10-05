using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LightReciever))]
public class LightPlayerSwitch : MonoBehaviour
{
    private LightReciever _lightReciever;
    private bool _wasLit;

    [SerializeField] private GameObject _unlitModel;
    [SerializeField] private GameObject _litModel;

    [SerializeField] private UnityEvent _onactivate;

    private void Start()
    {
        _lightReciever = GetComponent<LightReciever>();
    }

    private void Update()
    {
        if(_lightReciever.Lit && _wasLit == false) 
        {
            _onactivate.Invoke();
            _litModel.SetActive(true);
            _unlitModel.SetActive(false);
        }

        if(!_lightReciever.Lit && _wasLit == true)
        {
            _onactivate.Invoke();
            _litModel.SetActive(false);
            _unlitModel.SetActive(true);


        }

        _wasLit = _lightReciever.Lit;
    }


}
