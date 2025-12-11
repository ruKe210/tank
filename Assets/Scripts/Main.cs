using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Canvas canvas;
    AudioSource bkmuc;
    SoundData soundData ;
    void Start()
    {
        canvas = GameObject.Instantiate( Resources.Load<Canvas>("UI/Canvas"));
        UIMgr.Instance.canvas = canvas;
        UIMgr.Instance.ShowPanel<BeginPanel>();
        bkmuc = canvas.GetComponent<AudioSource>();
        soundData = DataMgr.Instance.GetSoundData();
        bkmuc.loop = true;
        bkmuc.mute = !soundData.isMusic;
        bkmuc.volume = soundData.musicVolume;
      

        //GameObject bk =  GameObject.Instantiate(Resources.Load<GameObject>("UI/bk"));

        //bk.transform.SetParent(canvas.transform,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
