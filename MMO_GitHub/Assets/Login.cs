﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private InputField username;
    [SerializeField] private InputField password;

    public static string Username;

    public void LoginAccount()
    {
        if (username.text == string.Empty) { Debug.Log("Please Enter an username"); return; }
        if (password.text == string.Empty) { Debug.Log("Please Enter a passowrd"); return; }
        Username = username.text;
       

        ClientTCP.PACKAGE_Login(username.text, password.text);
        GameObject LoginButton =GameObject.Find("buttonLogin");
        LoginButton.SetActive(false);
        Debug.Log("Sending Login information to server");
    }
}
