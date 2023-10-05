using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayDirectionalSoundScript : MonoBehaviour
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

    void Update()
    {
        UpdateSoundPos();
    }

    public void PlaySound()
    {
        _soundInstance = RuntimeManager.CreateInstance(sound);
        _soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        _soundInstance.start();
        _soundInstance.release();
    }

    private void UpdateSoundPos()
    {
        _soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
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
