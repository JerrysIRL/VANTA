using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LightReciever))]
public class ShadowPressurePlate : MonoBehaviour
{
    [SerializeField] private UnityEvent _steppedOn;
    [SerializeField] private UnityEvent _steppedOff;

    private LightReciever _reciever;
    DarkPlayer _player;
    private bool _wasLit;
    private bool _triggered;

    private void Start()
    {
        _reciever = GetComponent<LightReciever>();  
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DarkPlayer dark = other.GetComponent<DarkPlayer>();
        if(dark != null && _reciever.Lit)
        {
            _steppedOn?.Invoke();
            print("enter trigger");
            _triggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<DarkPlayer>() && _reciever.Lit && _triggered == false)
        {
            print("triggered");
            _triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DarkPlayer dark = other.GetComponent<DarkPlayer>();
        if (dark != null)
        {
            _steppedOff?.Invoke();
            print("exit");
            _triggered = false;
        }
    }

    



}
