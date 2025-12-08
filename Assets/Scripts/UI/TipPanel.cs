using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    // Start is called before the first frame update
    public Button btnConfirm;
    void Start()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<TipPanel>();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
