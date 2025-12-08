using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr 
{
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instacne => instance;

    LoginData loginData;
    private LoginMgr()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
    }
    public LoginData GetLoginData()
    {
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        return this.loginData;
    }
    public void SaveLoginData(LoginData loginData)
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
}
