using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr 
{
   private static DataMgr instance = new DataMgr();
   public static DataMgr Instance =>instance;


    void Start()
    {
 
        //instance = this;
    }
    public SoundData GetSoundData()
    {
        return JsonMgr.Instance.LoadData<SoundData>("SoundData");
  
    }

    public void SaveSoundData(SoundData soundData)
    {
        JsonMgr.Instance.SaveData(soundData, "SoundData");
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
}
