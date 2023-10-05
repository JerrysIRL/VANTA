using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic110 : PlaySoundScript
{
    [SerializeField] private float volume = 1;
    [SerializeField] private bool loopMain = false;
    [SerializeField] private bool loopSecondary = false;
    [SerializeField] private float layer1 = 0;
    [SerializeField] private float layer2 = 0;
    [SerializeField] private bool stopSound = false;

    void Update()
    {
        if(stopSound)
        {
            StopSound();
        }
        ChangeParameterByName("Volume",volume);
        ChangeParameterByName("Loop Main",loopMain);
        ChangeParameterByName("Loop Secondary",loopSecondary);
        ChangeParameterByName("layer1",layer1);
        ChangeParameterByName("layer2",layer2);
    }
}
