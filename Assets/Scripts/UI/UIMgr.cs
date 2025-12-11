using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UIMgr
{
    private static UIMgr instance = new UIMgr();
    public static UIMgr Instance=>instance;

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    public Canvas canvas;
    public bool isPause; 
    private  UIMgr()
    {
        isPause = false;
        //this.canvas = GameObject.Find("Canvas");
        //GameObject.DontDestroyOnLoad(this.canvas);

        //this.ShowPanel<LoginPanel>();
    }

    public void  ShowPanel<T>() where T :BasePanel
    {
        if (!panelDic.ContainsKey(typeof(T).Name))
        {
            string name = typeof(T).Name;
            Debug.Log(typeof(T).Name);
            GameObject panel = GameObject.Instantiate(Resources.Load<GameObject>( "UI/"+typeof(T).Name));
            panel.transform.SetParent(this.canvas.transform, false);

            panelDic.Add(typeof(T).Name, panel.GetComponent<T>());
            panel.GetComponent<T>().ShowMe();
        }
    }
    public void HidePanel<T>() where T : BasePanel
    {
        if(panelDic.ContainsKey(typeof(T).Name))
        {
            T panel = panelDic[typeof(T).Name] as T;
            panel.HideMe(()=>
            {
                GameObject.Destroy(panel.gameObject);
                panelDic.Remove(typeof(T).Name); 
            });
        }
    }

    public T Getpanel<T>() where T : BasePanel
    {
        //T panel;
        if (panelDic.ContainsKey(typeof(T).Name))
            return  panelDic[typeof(T).Name] as T;
        else
            return null;
    }

}
