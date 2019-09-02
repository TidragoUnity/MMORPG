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


        DontDestroyOnLoad(this);
        UnityThread.initUnityThread();

        ClientHandleData.InitializePacketListener();
        ClientTCP.InitializeClientSocket(ipAddress, port);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {



    }

    /*
    void SMove()
    {
        while (true)
        {
            if (ClientHandleData.LoggedIn){
                player = GameObject.Find("Player(Clone)");
                if (player != null){
                    for (int i = 0; i < 200; i++){
                        if (i == 100)
                        {
                            Debug.Log(Mathf.Round(player.transform.position.x).ToString() + "  " + Mathf.Round(player.transform.position.y).ToString() + "  " + Mathf.Round(player.transform.position.z).ToString());
                            ClientTCP.PACKAGE_SendMovement(Mathf.Round(player.transform.position.x).ToString(), Mathf.Round(player.transform.position.y).ToString(), Mathf.Round(player.transform.position.z).ToString());
                            i = 0;
                        }
                        else
                        {
                            Debug.Log(i); i++; return;
                        }


                    }



                }
            }

        }
    }
    */
}

