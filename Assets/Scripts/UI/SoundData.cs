using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData 
{
    public bool isSound;
    public bool isMusic;
    public float musicVolume;
    public float soundVolume;

    public void init()
    {
        isSound = true;
        isMusic = true;
        musicVolume = 1;
        soundVolume = 1;
    }
    public void init(bool isSound, bool isMusic,float musicVolume,float soundVolume)
    {
        this.isSound = isSound;
        this.isMusic = isMusic;
        this.musicVolume = musicVolume;
        this.soundVolume = soundVolume;
    }

}
