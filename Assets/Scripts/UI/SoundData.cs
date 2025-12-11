using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData 
{
    public bool isSound= true;
    public bool isMusic= true;
    public float musicVolume=1;
    public float soundVolume=1;

    public float mouseSensitivity=1;
    public void init()
    {
        isSound = true;
        isMusic = true;
        musicVolume = 1;
        soundVolume = 1;
        mouseSensitivity = 5;
    }
    public void init(bool isSound, bool isMusic,float musicVolume,float soundVolume, float mouseSensitivity)
    {
        this.isSound = isSound;
        this.isMusic = isMusic;
        this.musicVolume = musicVolume;
        this.soundVolume = soundVolume;
        this.mouseSensitivity = mouseSensitivity;
    }

}
