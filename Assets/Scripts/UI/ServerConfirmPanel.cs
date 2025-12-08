using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerConfirmPanel : BasePanel
{
    public Button enterGame;
    
    public Button changeServer;

    public Text serverNo;
    public Text serverName;
    void Start()
    {
        serverNo.text = ServerListMgr.Instance.nowServer.serverNo.ToString();
        serverName.text = ServerListMgr.Instance.nowServer.serverName;

        enterGame.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<ServerConfirmPanel>();
            SceneManager.LoadScene("GameScene");
        });
        changeServer.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<ServerConfirmPanel>();
            UIMgr.Instance.ShowPanel<ServerSelectPanel>();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
