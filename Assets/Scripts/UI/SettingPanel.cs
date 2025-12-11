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

    public Slider mouseSensitivity;

    Canvas canvas;
    AudioSource bkmuc;



    // Start is called before the first frame update
    void Start()
    {
        canvas = UIMgr.Instance.canvas;
        bkmuc = canvas.GetComponent<AudioSource>();
        soundData = DataMgr.Instance.GetSoundData();

        isMusic.isOn = soundData.isMusic;
        isSound.isOn = soundData.isSound;
        musicVolume.value = soundData.musicVolume;

        soundVolume.value = soundData.soundVolume;
        mouseSensitivity.value = soundData.mouseSensitivity;

        isMusic.onValueChanged.AddListener((isMusic) =>
        {
            soundData.isMusic = isMusic;
            
            bkmuc.mute = !isMusic;
        });
        isSound.onValueChanged.AddListener((isSound) =>
        {
            soundData.isSound = isSound;
        });

        musicVolume.onValueChanged.AddListener((musicVolume) =>
        {
            soundData.musicVolume = musicVolume;
            bkmuc.volume = musicVolume;
        });
        soundVolume.onValueChanged.AddListener((soundVolume) =>
        {
            soundData.soundVolume = soundVolume;
        });

        mouseSensitivity.onValueChanged.AddListener((mouseSensitivity) =>
        {
            soundData.mouseSensitivity = mouseSensitivity;
        });

        save.onClick.AddListener(() =>
        {
            DataMgr.Instance.SaveSoundData(soundData);
            UIMgr.Instance.HidePanel<SettingPanel>();
            UIMgr.Instance.ShowPanel<BeginPanel>();
        });
        back.onClick.AddListener(() =>
        {
            soundData = DataMgr.Instance.GetSoundData();
            bkmuc.mute = !soundData.isMusic;
            bkmuc.volume = soundData.musicVolume;
            UIMgr.Instance.HidePanel<SettingPanel>();
            UIMgr.Instance.ShowPanel<BeginPanel>();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
