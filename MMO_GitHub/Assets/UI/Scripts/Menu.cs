using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject otherInterfaces;
    public GameObject clientManager;
    private string ipAddress = "192.168.178.25";
    private int port = 5555;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        GameObject menuPanel = GameObject.Find("MenuPanel");

        otherInterfaces.SetActive(true);
        menuPanel.SetActive(false);

        UnityThread.initUnityThread();
        ClientHandleData.InitializePacketListener();
        ClientTCP.InitializeClientSocket(ipAddress, port);
    }


}
