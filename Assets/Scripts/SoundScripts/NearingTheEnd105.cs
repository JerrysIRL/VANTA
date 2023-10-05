using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearingTheEnd105 : PlaySoundScript

{
    //[SerializeField] private float volume = 1;
    [SerializeField] private float endPianoSound = 0f;
    [SerializeField] private bool stopSound = false;

    void Update()
    {
        if (stopSound)
        {
            StopSound();
        }
        //ChangeParameterByName("Volume", volume);
        ChangeParameterByName("Nearing the end", endPianoSound);

        float num;
        //_soundInstance.getParameterByName("Nearing the end", out num);
    }

    public void SetIntensity(float intensity)
    {
        endPianoSound= intensity;
    }
}

