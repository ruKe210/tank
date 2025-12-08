using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData 
{
    public string account;
    public string password;
    public bool isAutoLogin;
    public bool isRememberme;

    public void init()
    {
        this.isAutoLogin = false;
        this.isRememberme = false;
    }
    public void init(string account, string password)
    {
        this.account = account;
        this.password = password;
        this.isAutoLogin = false;
        this.isRememberme = false;
    }
    public void init(string account,string password,bool isAutoLogin,bool isRememberme)
    {
        this.account = account;
        this.password = password;
        this.isAutoLogin = isAutoLogin;
        this.isRememberme = isRememberme;
    }
}
