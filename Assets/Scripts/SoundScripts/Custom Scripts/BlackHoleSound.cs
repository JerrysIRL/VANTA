using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSound : PlayDirectionalSoundScript
{
    // If Sound Has custom Parameters Create a copy of this script
    // To add parameters, do it in a simular manner to volume below. You can set Int, Float Or Bool to control the parameter. Then By using "ChangeParameterByName("ParamName",value);" in update the parameter will update from the value.
    // To play sound use "PlaySound();" to stop use "StopSound();"
    [SerializeField] private bool stopSound = false;
    [SerializeField] private float volume = 1;
    [SerializeField] private float minDistance = 0;
    [SerializeField] private float maxDistance = 10;
    
    void Update()
    {
        if(stopSound)
        {
            StopSound();
        }
        ChangeParameterByName("Volume",volume);
        ChangeParameterByName("Min Distance",minDistance);
        ChangeParameterByName("Max Distance",maxDistance);
    }
}   

