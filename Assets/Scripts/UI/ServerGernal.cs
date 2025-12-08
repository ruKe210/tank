using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static Unity.VisualScripting.Metadata;

public class ServerGernal : MonoBehaviour
{

    public Button serverGernal;
    private int num;
    private List<ServerData> serverList;
    private Transform detail;
    List<Transform> children= new List<Transform>();

    Text serverRange;
    // Start is called before the first frame update
    void Start()
    {
        serverList = ServerListMgr.Instance.GetServerList();

        serverGernal.onClick.AddListener(() =>
        {
            print(detail.childCount);
            for (int i = 0; i < detail.childCount; i++)
            {
                children.Add(detail.GetChild(i));
            }


            for (int i = 0; i < detail.childCount; i++)
            {
                Destroy(children[i].gameObject);
                
            }
            children= new List<Transform>();


            for (int i=num*10;i<num*10+10&&i<serverList.Count;i++)
            {
                
                GameObject serverDetail = GameObject.Instantiate(Resources.Load("UI/serverDetail")) as GameObject;
                serverDetail.GetComponent<ServerDetail>().init(serverList[i]);
                serverDetail.transform.transform.SetParent(detail, false);

            }

            serverRange.text = "服务器 " + (num*10 + 1).ToString() + "-" + (num*10 + 10).ToString();

        });

    }
    public void init(int num,Transform detail,Text serverRange)
    {
        this.num = num;
        this.detail = detail;
        this.serverRange = serverRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
