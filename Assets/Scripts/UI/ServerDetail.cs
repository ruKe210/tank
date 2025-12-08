using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerDetail : MonoBehaviour
{
    public Button btn;
    public Image isNew;
    public List<Image> status;
    public Text text;
    private ServerData serverData = null;
    void Start()
    {
        isNew.gameObject.SetActive(false);
        for (int i = 0; i < status.Count; i++)
        {
            status[i].gameObject.SetActive(false);
        }

        StartCoroutine(enumerator());

        btn.onClick.AddListener(() =>
        {
            ServerListMgr.Instance.nowServer = serverData;
            JsonMgr.Instance.SaveData(serverData, "ServerDataNow");
            UIMgr.Instance.HidePanel<ServerSelectPanel>();
            UIMgr.Instance.ShowPanel<ServerConfirmPanel>();

        });
    }

    IEnumerator enumerator ()
    {
        while(this.serverData==null)
        {
            yield return 0;
        }
        text.text = this.serverData.serverNo + "区 " + this.serverData.serverName;
        isNew.gameObject.SetActive(this.serverData.isNew);
        status[this.serverData.status].gameObject.SetActive(true);
        if(this.serverData.status==3)
        {
            btn.interactable=false;
        }
        yield return 0;
    }
    public void init(ServerData serverData)
    {
        this.serverData = serverData;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
