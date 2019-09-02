using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] private string ipAddress;
    [SerializeField] private int port;
    private void Awake()
    {

        // Wurde in Menu eingebaut
        
        DontDestroyOnLoad(this);
        UnityThread.initUnityThread();

        ClientHandleData.InitializePacketListener();
        ClientTCP.InitializeClientSocket(ipAddress, port);
        
    }

    public string GetIpAddress()
    {
        return ipAddress;
    }
    public int GetPort()
    {
        return port;
    }
    
}

