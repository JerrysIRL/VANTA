using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    [SerializeField] private UnityEvent _onactivate;
    private bool _activated;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightElement"))
        {
            if (!_activated)
            {
                _activated = true;
                _onactivate.Invoke();

            }
        }
    }

    public void InteractAction()
    {
        _onactivate?.Invoke();
    }
}
