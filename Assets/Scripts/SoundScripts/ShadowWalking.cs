using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWalking : PlayDirectionalSoundScript
{
    // If Sound Has custom Parameters Create a copy of this script
    // To add parameters, do it in a simular manner to volume below. You can set Int, Float Or Bool to control the parameter. Then By using "ChangeParameterByName("ParamName",value);" in update the parameter will update from the value.
    // To play sound use "PlaySound();" to stop use "StopSound();"
    [SerializeField] private float volume = 1;
    [SerializeField] private float reverb = 0;
    [SerializeField] private int currentMaterial = 0;
    [SerializeField] private float timeBetweenSteps = 0.5f;
    [SerializeField] private DarkMovement DarkMovement;
    private float _currentTimeBetweenSteps;
   

    void Update()
    {
        _currentTimeBetweenSteps+=Time.deltaTime;
        if(DarkMovement.GetMovingBool() && _currentTimeBetweenSteps >= timeBetweenSteps)
        {
            PlaySound();
            _currentTimeBetweenSteps=0;
        }
        ChangeParameterByName("Volume",volume);
        ChangeParameterByName("Reverb",reverb);
        ChangeParameterByName("Player Moving", DarkMovement.GetMovingBool());
        ChangeParameterByName("WalkingOnMaterial",currentMaterial);
    }
} 

