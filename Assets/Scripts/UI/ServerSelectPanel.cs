using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ServerSelectPanel : BasePanel
{

    public Transform general;
    public Transform detail;

    public ServerDetail nowServer;

    public Text serverRange;

    List<ServerData> serverList;
    // Start is called before the first frame update
    void Start()
    {
        serverList = ServerListMgr.Instance.GetServerList();
        nowServer.init(ServerListMgr.Instance.nowServer);

        print(serverList.Count);
        int num = serverList.Count / 10;
        serverRange.text = "服务器 1-10" ;

        for (int i = 0; i < num; i++)
        {
           
            string text = new string("");
            text += (i  * 10+1).ToString() + '-' + ((i + 1) * 10).ToString()+"区"; 

            GameObject serverGeneral = GameObject.Instantiate(Resources.Load("UI/serverGeneral")) as GameObject;
            serverGeneral.GetComponentInChildren<Text>().text = text;
            serverGeneral.GetComponent<ServerGernal>().init(i, detail, serverRange);

            serverGeneral.transform.SetParent(general,false);

        }
        if (serverList.Count % 10 != 0)
        {
            string text = new string("");
            text += (num * 10+1).ToString() + '-' + serverList.Count.ToString() + "区";
            GameObject serverGeneral = GameObject.Instantiate(Resources.Load("UI/serverGeneral")) as GameObject;
            serverGeneral.GetComponentInChildren<Text>().text = text;
            serverGeneral.GetComponent<ServerGernal>().init(num, detail, serverRange);
            serverGeneral.transform.SetParent(general, false);
        }

        for(int i=0;i<10;i++)
        {
            GameObject serverDetail = GameObject.Instantiate(Resources.Load("UI/serverDetail")) as GameObject;
            serverDetail.GetComponent<ServerDetail>().init(serverList[i]);
            serverDetail.transform.transform.SetParent(detail,false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
