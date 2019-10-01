using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

        
public class ClientHandleData
{
    private static ByteBuffer playerBuffer;
    public delegate void Packet_(byte[] data);
    public static Dictionary<int, Packet_> packetListener;
    private static int pLength;

    static private bool spawnedYou;
    public static Text informationOutput;
    private static Text playerGold;
    private static Text playerExp;
    private static Text playerLevel;
    private static Text playerHonor;
    public static bool LoggedIn;






    public static void InitializePacketListener()
    {
        packetListener = new Dictionary<int, Packet_>();
        packetListener.Add((int)ServerPackages.SWelcomeMsg, HandleWelcomeMsg);
        packetListener.Add((int)ServerPackages.SAlertMsg, HandleAlertMsg);
        packetListener.Add((int)ServerPackages.SLoggedIn, HandleLoggedIn);
        packetListener.Add((int)ServerPackages.SPlayerData, HandlePlayerData);
        packetListener.Add((int)ServerPackages.SIngame, HandleIngame);
        packetListener.Add((int)ServerPackages.SSpawnForOnlines, HandleForOnlines);
        packetListener.Add((int)ServerPackages.SSpawnTheOthers, HandleTheOthers);
        packetListener.Add((int)ServerPackages.SDespawn, HandleDespawn);
        packetListener.Add((int)ServerPackages.SUpdatePosition, HandleUpdatePosition);
        packetListener.Add((int)ServerPackages.SSendDrops, HandleDrops);
        packetListener.Add((int)ServerPackages.SUpdateDestination, HandleUpdateDestination);

        informationOutput = GameObject.Find("InformationOutput").GetComponent<Text>();
        playerGold        = GameObject.Find("Canvas/OtherInterfaces/PlayerData/Gold").GetComponent<Text>();
        playerExp         = GameObject.Find("Canvas/OtherInterfaces/PlayerData/Exp").GetComponent<Text>();
        playerLevel       = GameObject.Find("Canvas/OtherInterfaces/PlayerData/Level").GetComponent<Text>();
        playerHonor       = GameObject.Find("Canvas/OtherInterfaces/PlayerData/Honor").GetComponent<Text>();

    }


    /* ByteBuffer buffer = new ByteBuffer();
     buffer.WriteBytes(data);
     int packageID = buffer.ReadInteger();
     */
    #region HandleData
    public static void HandleData(byte[] data)
    {

        byte[] buffer = (byte[])data.Clone();

        if (playerBuffer == null)
        {
            playerBuffer = new ByteBuffer();
        }

        playerBuffer.WriteBytes(buffer);

        if (playerBuffer.Count() == 0)
        {
            playerBuffer.Clear();
            return;
        }

        if (playerBuffer.Length() >= 4)
        {
            pLength = playerBuffer.ReadInteger(false);
            if (pLength <= 0)
            {
                playerBuffer.Clear();
                return;
            }
        }
        while (pLength > 0 & pLength <= playerBuffer.Length() - 4)
        {
            if (pLength <= playerBuffer.Length() - 4)
            {
                playerBuffer.ReadInteger();
                data = playerBuffer.ReadBytes(pLength);
                HandleDataPackages(data);
            }

            pLength = 0;
            if (playerBuffer.Length() >= 4)
            {
                pLength = playerBuffer.ReadInteger(false);
                if (pLength <= 0)
                {
                    playerBuffer.Clear();
                    return;
                }
            }

            if (pLength <= 1)
            {
                playerBuffer.Clear();
            }
        }
    }

    private static void HandleDataPackages(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();

        if (packetListener.TryGetValue(packageID, out Packet_ packet))
        {
            packet.Invoke( data);
        }
    }
    private static void HandleWelcomeMsg(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        string msg = buffer.ReadString();

        Debug.Log(msg);
        ClientTCP.PACKAGE_ThankYou();
    }
    private static void HandleAlertMsg(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        string msg = buffer.ReadString();

        Debug.Log(msg);

        informationOutput.text = msg;


    }
    private static void HandleLoggedIn(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();

        GameObject loginSystem =GameObject.Find("LoginSystem");
        loginSystem.SetActive(false);
        LoggedIn = true;



    }
    private static void HandlePlayerData(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();

        string gold  = buffer.ReadString();
        string exp   = buffer.ReadString();
        string level = buffer.ReadString();
        string honor = buffer.ReadString();

        playerGold.text  = "Gold: " + gold; ;
        playerExp.text   = "Experience: " + exp;
        playerLevel.text = "Level: " + level;
        playerHonor.text = "Honor: " + honor;
        Debug.Log("gold: " + gold + "exp: " + exp + "level: " + level + "honor: " + honor);
    }
    private static void HandleIngame(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();

        string x = buffer.ReadString();

        string y = buffer.ReadString();
        string z = buffer.ReadString();
        OnlinePlayers.SpawnPlayer(float.Parse(x), float.Parse(y), float.Parse(z), "Player");
        //CameraController.changeCamera = true;
        RotateCam.changeCamera = true;
        GameObject player = GameObject.Find("Player(Clone)");
        player.GetComponent<clickToMove>().enabled = true;
        player.GetComponent<attack>().enabled = true;
        player.GetComponent<selectTarget>().enabled = true;
        player.GetComponent<otherPlayers>().enabled = false;
        player.tag = "Player";
        buffer.Dispose();
        spawnedYou = true;
        GameObject bars = GameObject.Find("Canvas/OtherInterfaces/Bars");
        bars.SetActive(true);
        Text uName = GameObject.Find("Canvas/OtherInterfaces/Bars/HealthBar/HealthBarPanel/HealthBar/Text").GetComponent<Text>();
        uName.text = Login.Username;

    }
    private static void HandleForOnlines(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        string x = buffer.ReadString();
        string y = buffer.ReadString();
        string z = buffer.ReadString();
        string username = buffer.ReadString();
        OnlinePlayers.SpawnPlayer(float.Parse(x), float.Parse(y), float.Parse(z), username);
        buffer.Dispose();
    }
    private static void HandleTheOthers(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        string x = buffer.ReadString();
        string y = buffer.ReadString();
        string z = buffer.ReadString();
        string username = buffer.ReadString();

        OnlinePlayers.SpawnPlayer(float.Parse(x), float.Parse(y), float.Parse(z),username);
        buffer.Dispose();

    }
    private static void HandleDespawn(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        string username = buffer.ReadString();
        Debug.Log("Trying to Despawn..." + username + "(Clone)");

        GameObject offlineUser = GameObject.Find(username + ("(Clone)"));
        GameObject.Destroy(offlineUser);
    }
    private static void HandleUpdatePosition(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        string username = buffer.ReadString();
        float x = buffer.ReadFloat ();
        float y = buffer.ReadFloat();
        float z = buffer.ReadFloat();
        if (spawnedYou)
        {
            try
            {
                GameObject user = GameObject.Find(username + ("(Clone)"));
                user.transform.position = new Vector3(x, y, z);
                buffer.Dispose();
            }
            catch (Exception)
            {

                throw;
            }


        }
        buffer.Dispose();
    }
    private static void HandleDrops(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        int gold = buffer.ReadInteger();
        int xp = buffer.ReadInteger();
        int level = buffer.ReadInteger();
        int honor = buffer.ReadInteger();
        buffer.Dispose();
    
        playerGold.text = "Gold: " + gold; ;
        playerExp.text = "Experience: " + xp;
        playerLevel.text = "Level: " + level;
        playerHonor.text = "Honor: " + honor;
        Debug.Log("gold: " + gold + "exp: " + xp + "level: " + level + "honor: " + honor);

    }
    private static void HandleUpdateDestination(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();



        float x = buffer.ReadFloat();
        float y = buffer.ReadFloat();
        float z = buffer.ReadFloat();

        string username = buffer.ReadString();

        Vector3 pos = new Vector3(x, y, z);

        GameObject player = GameObject.Find(username+"(Clone)");
        NavMeshAgent magent =player.GetComponent<NavMeshAgent>();
        magent.destination = pos;

        buffer.Dispose();



    }

    #endregion


}
