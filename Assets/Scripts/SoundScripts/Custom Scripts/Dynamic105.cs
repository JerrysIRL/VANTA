using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic105 : PlaySoundScript

{
    [SerializeField] private float volume = 1;
    [SerializeField] private bool mainString = false;
    [SerializeField] private bool secondString = false;
    [SerializeField] private bool vocalsAndMelody = false;
    [SerializeField] private bool stopSound = false;

    void Update()
    {
        if(stopSound)
        {
            StopSound();
        }
        ChangeParameterByName("Volume",volume);
        ChangeParameterByName("Layer1",mainString);
        ChangeParameterByName("Layer2",secondString);
        ChangeParameterByName("Layer3",vocalsAndMelody);
    }
}   

