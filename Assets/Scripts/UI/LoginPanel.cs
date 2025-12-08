using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public InputField account;
    public InputField password;
    private string acc = new  string("");
    private string pwd = new string("");

    public Button btnConfirm;
    public Button btnRegister;

    public Toggle togIsAutoLogin;
    public Toggle togIsRememberMe;

    private LoginData loginData;
    // Start is called before the first frame update
    void Start()
    {
        this.loginData = LoginMgr.Instacne.GetLoginData();
        print(loginData.isAutoLogin);
        print(loginData.isRememberme);

        if (loginData.isRememberme)
        {
            togIsRememberMe.isOn = true;
            this.account.text=this.acc = loginData.account;
            this.password.text=this.pwd = loginData.password;
        }
        else
        {
            togIsRememberMe.isOn = false;

        }
        if (loginData.isAutoLogin)
        {
            togIsAutoLogin.isOn = true;
            if (loginData.account == acc && loginData.password == pwd)
            {
                UIMgr.Instance.HidePanel<LoginPanel>();
                UIMgr.Instance.ShowPanel<ServerConfirmPanel>();
            }
               
        }
        else
        {
            togIsAutoLogin.isOn = false;
        }
      

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
                if(loginData.account==acc&&loginData.password==pwd)
                { 
                    UIMgr.Instance.HidePanel<LoginPanel>();
                    UIMgr.Instance.ShowPanel<ServerConfirmPanel>();
                }
                else
                    UIMgr.Instance.ShowPanel<TipPanel>();

            }
        });
        btnRegister.onClick.AddListener(() =>
        {
            UIMgr.Instance.HidePanel<LoginPanel>();
            UIMgr.Instance.ShowPanel<RegisterPanel>();
        });

        togIsAutoLogin.onValueChanged.AddListener((isAutoLogin) =>
        {
            loginData.isAutoLogin = isAutoLogin;
            LoginMgr.Instacne.SaveLoginData(loginData);
        });
        togIsRememberMe.onValueChanged.AddListener((isRememberme) =>
        {
            loginData.isRememberme = isRememberme;
            LoginMgr.Instacne.SaveLoginData(loginData);
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
