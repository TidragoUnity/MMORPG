using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public Text textOutput;

    [SerializeField] private InputField username;
    [SerializeField] private InputField password;
    [SerializeField] private InputField repeatPassword;

   public void RegisterAccount()
    {

        if(username.text == string.Empty) { Debug.Log("Please Enter an username");            textOutput.text = "Please Enter an username"; return; }
        if(password.text == string.Empty) { Debug.Log("Please Enter a passowrd");             textOutput.text = "Please Enter a passowrd";  return; }
        if(password.text != repeatPassword.text) { Debug.Log("Your Password does not match"); textOutput.text = "Your Password does not match"; return; }

        ClientTCP.PACKAGE_NewAccount(username.text, password.text);
        Debug.Log("Sending Account information to server");
    }

    void ClearText()
    {

    }
}
