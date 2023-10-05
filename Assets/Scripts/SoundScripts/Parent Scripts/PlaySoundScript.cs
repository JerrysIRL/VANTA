using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlaySoundScript : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference sound;
    [SerializeField] private bool playOnAwake = false;
    private FMOD.Studio.EventInstance _soundInstance;
    

    void Awake ()
    {
        if(playOnAwake)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        _soundInstance = RuntimeManager.CreateInstance(sound);
        _soundInstance.start();
        _soundInstance.release();
    }

// Idea is to inherit from script in order to setup parameters for the sound
    public void ChangeParameterByName(string paramName, float value)
    {
       _soundInstance.setParameterByName(paramName,value); 
    }

    public void ChangeParameterByName(string paramName, bool value)
    {
       _soundInstance.setParameterByName(paramName,BoolToInt(value)); 
    }

    public void ChangeParameterByName(string paramName, int value)
    {
       _soundInstance.setParameterByName(paramName,value); 
    }

    public void StopSound()
    {
        _soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _soundInstance.release();
    }

    private int BoolToInt(bool value)
    {
       if(value)
       {
        return(1);
       }
       else
       {
        return(0);
       }
    }
    

}
