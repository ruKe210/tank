using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button start;
    public Button quit;
    public Button setting;
    public Button rank;
    void Start()
    {
        UIMgr.Instance.isPause = false;
        start.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<BeginPanel>();
            //UIMgr.Instance.ShowPanel<hpPanel>();
            SceneManager.LoadScene("GameScene");

        });

       
        quit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        setting.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<BeginPanel>();
            UIMgr.Instance.ShowPanel<SettingPanel>();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
