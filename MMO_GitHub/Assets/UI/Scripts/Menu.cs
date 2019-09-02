using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject otherInterfaces;
    private void Awake()
    {
        DontDestroyOnLoad(this);

    }
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

        ClientManager manager = new ClientManager();
        string ipAddress = manager.GetIpAddress();
        int port = manager.GetPort();

        UnityThread.initUnityThread(); 

        ClientHandleData.InitializePacketListener();
        ClientTCP.InitializeClientSocket(ipAddress, port);
    }


}
