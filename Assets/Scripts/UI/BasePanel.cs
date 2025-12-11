using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

public class BasePanel : MonoBehaviour
{


    //private CanvasGroup canvasGroup;
    float alphaSpeed = 10;
    //public bool isShow = true;
    private UnityAction eventCallBack;
    private void Awake()
    {
        //canvasGroup = this.GetComponent<CanvasGroup>();

    }
    void Start()
    {
 
    }


    public virtual void ShowMe()
    {
        //this.canvasGroup.alpha = 1;
    }
    public virtual void HideMe(UnityAction callBack)
    {

        //this.canvasGroup.alpha = 0;
        this.eventCallBack = callBack;
        this.eventCallBack();
    }
    
    void Update()
    {
        
    }
}
