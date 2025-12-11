using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
        UIMgr.Instance.ShowPanel<BeginPanel>();
        //GameObject bk =  GameObject.Instantiate(Resources.Load<GameObject>("UI/bk"));

        //bk.transform.SetParent(canvas.transform,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
