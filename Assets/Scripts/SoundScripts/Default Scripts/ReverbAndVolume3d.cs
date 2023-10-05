using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbAndVolume3d : PlayDirectionalSoundScript
{
    // If Sound Has custom Parameters Create a copy of this script
    // To add parameters, do it in a simular manner to volume below. You can set Int, Float Or Bool to control the parameter. Then By using "ChangeParameterByName("ParamName",value);" in update the parameter will update from the value.
    // To play sound use "PlaySound();" to stop use "StopSound();"
    public float volume = 1;
    public float reverb = 0;
    [SerializeField] private bool stopSound = false;

    void Update()
    {
        if(stopSound)
        {
            StopSound();
        }
        ChangeParameterByName("Volume",volume);
        ChangeParameterByName("Reverb",reverb);
    }
} 

