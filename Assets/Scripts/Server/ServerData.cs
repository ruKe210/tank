using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ServerListMgr
{
    private static ServerListMgr instance = new ServerListMgr();
    public static ServerListMgr Instance => instance;

    public ServerData nowServer;

    public List<ServerData> serverList = new List<ServerData>();
    ServerListMgr()
    {
        serverList = JsonMgr.Instance.LoadData<List<ServerData>>("ServerData");
        nowServer = JsonMgr.Instance.LoadData<ServerData>("ServerDataNow");
    }
    public List<ServerData> GetServerList()
    {
        return this.serverList;
    }
}
public class ServerData 
{
    public int serverNo;
    public string serverName;
    public int status;
    public bool isNew;
}
