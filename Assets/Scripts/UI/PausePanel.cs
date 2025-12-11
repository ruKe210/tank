using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    public Button quit;
    public Button back;

    // Start is called before the first frame update
    void Start()
    {
        quit.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<hpPanel>();
            UIMgr.Instance.HidePanel<PausePanel>();
            SceneManager.LoadScene("BeginScene");

        });
        back.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<PausePanel>();
            UIMgr.Instance.isPause = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
