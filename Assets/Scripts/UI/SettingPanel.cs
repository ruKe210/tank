using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{


    public Toggle isMusic;
    public Toggle isSound;
    public Slider musicVolume;
    public Slider soundVolume;

    public Button back;
    public Button save;

    private SoundData soundData;
    // Start is called before the first frame update
    void Start()
    {
  
        soundData = DataMgr.Instance.GetSoundData();

        isMusic.isOn = soundData.isMusic;
        isSound.isOn = soundData.isSound;
        musicVolume.value = soundData.musicVolume;

        soundVolume.value = soundData.soundVolume;
        
        
        isMusic.onValueChanged.AddListener((isMusic) =>
        {
            soundData.isMusic = isMusic;
        });
        isSound.onValueChanged.AddListener((isSound) =>
        {
            soundData.isSound = isSound;
        });

        musicVolume.onValueChanged.AddListener((musicVolume) =>
        {
            soundData.musicVolume = musicVolume;
        });
        soundVolume.onValueChanged.AddListener((soundVolume) =>
        {
            soundData.soundVolume = soundVolume;
        });

        save.onClick.AddListener(() =>
        {
            DataMgr.Instance.SaveSoundData(soundData);
            UIMgr.Instance.HidePanel<SettingPanel>();
            UIMgr.Instance.ShowPanel<BeginPanel>();
        });
        back.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<SettingPanel>();
            UIMgr.Instance.ShowPanel<BeginPanel>();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
