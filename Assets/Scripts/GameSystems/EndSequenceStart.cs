using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequenceStart : MonoBehaviour
{
    [SerializeField] Goal _goal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _goal.StartEndSequence();        
        }
    }
}
