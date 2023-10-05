using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private FMOD.Studio.Bus _master;
    private FMOD.Studio.Bus _music;
    private FMOD.Studio.Bus _sfx;
    private FMOD.Studio.Bus _ui;
    private FMOD.Studio.Bus _ambiance;

    //public float masterVolume;
    //public float musicVolume;
    //public float sfxVolume;
    //public float uiVolume;
    //public float ambianceVolume;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider masterSlider;

    [SerializeField] AudioSettingObject audioSettingObject;

    void Awake()
    {
        _master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        _music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        _sfx = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        //_ui = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX/UI");
        //_ambiance = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music/Ambiance");
    }

    private void Start()
    {
        Load();
    }


    void Update()
    {
        _master.setVolume (audioSettingObject.masterVolume);
        _music.setVolume(audioSettingObject.musicVolume);
        _sfx.setVolume(audioSettingObject.sfxVolume);

        Load();
    }


    public void SetMasterVolume()
    {
        audioSettingObject.masterVolume = masterSlider.value;
    }
    public void SetMusicVolume()
    {
        audioSettingObject.musicVolume = musicSlider.value;
    }
    public void SetSFXVolume()
    {
         audioSettingObject.sfxVolume = sfxSlider.value;
    }

    //public void Save()
    //{
    //    audioSettingObject.masterVolume = masterSlider.value;
    //    audioSettingObject.musicVolume = musicSlider.value;
    //    audioSettingObject.sfxVolume = sfxSlider.value;
    //}

    public void Load()
    {
        masterSlider.value = audioSettingObject.masterVolume;
        musicSlider.value = audioSettingObject.musicVolume;
        sfxSlider.value = audioSettingObject.sfxVolume;
    }

    
}

