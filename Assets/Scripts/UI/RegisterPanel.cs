using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public InputField account;
    public InputField password;
    private string acc = new string("");
    private string pwd = new string("");

    public Button btnConfirm;
    public Button btnCancel;
    // Start is called before the first frame update
    void Start()
    {
        account.onEndEdit.AddListener((acc) =>
        {
            this.acc = acc;
        });
        password.onEndEdit.AddListener((pwd) =>
        {
            this.pwd = pwd;
        });
        btnConfirm.onClick.AddListener(() =>
        {
            if (acc.Length == 0 || pwd.Length == 0)
            {
                UIMgr.Instance.ShowPanel<TipPanel>();
            }
            else
            {
                UIMgr.Instance.HidePanel<RegisterPanel>();
                UIMgr.Instance.ShowPanel<LoginPanel>();
                LoginData loginData = new LoginData();
                loginData.init(this.acc, this.pwd);
                LoginMgr.Instacne.SaveLoginData(loginData);

                
            }
        });
        btnCancel.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<RegisterPanel>();
            UIMgr.Instance.ShowPanel<LoginPanel>();

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
