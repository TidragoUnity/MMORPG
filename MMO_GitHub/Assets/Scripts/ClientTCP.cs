using System;
using System.Net.Sockets;
using UnityEngine;

public class ClientTCP 
{

    private static TcpClient clientSocket;
    private static NetworkStream myStream;
    private static byte[] receiveBuffer;

    public static void InitializeClientSocket(string adress, int port)
    {
        clientSocket = new TcpClient();
        clientSocket.ReceiveBufferSize = 4096;
        clientSocket.SendBufferSize = 4096;
        receiveBuffer = new byte[2 * 4096];
        clientSocket.BeginConnect(adress, port, new AsyncCallback(ClientConnectCallback), clientSocket);

    }


    private static void ClientConnectCallback(IAsyncResult result)
    {
        clientSocket.EndConnect(result);
        if(clientSocket.Connected == false)
        {
            return;
        }        
        else
        {
            myStream = clientSocket.GetStream();
            myStream.BeginRead(receiveBuffer, 0, 496 * 2, ReceiveCallback, null);
        }
    }

    private static void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int readBytes = myStream.EndRead(result);
            if (readBytes <= 0)
            {
                return;
            }
            byte[] newBytes = new byte[readBytes];
            Buffer.BlockCopy(receiveBuffer, 0, newBytes, 0, readBytes);
            UnityThread.executeInUpdate(() =>
            {
                ClientHandleData.HandleData(newBytes);
            });
            myStream.BeginRead(receiveBuffer, 0, 4096*2, ReceiveCallback, null);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public static void SendData(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
        buffer.WriteBytes(data);
        myStream.Write(buffer.ToArray(), 0, buffer.ToArray().Length);
        buffer.Dispose();

    }

    public static void PACKAGE_ThankYou()
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CThankYou);
        buffer.WriteString("Thank you! I`m glad to be connected to the server!");
        SendData(buffer.ToArray());
    }
    public static void PACKAGE_NewAccount(string username, string password)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CNewAccount);
        buffer.WriteString(username);
        buffer.WriteString(password);
        SendData(buffer.ToArray());
    }
    public static void PACKAGE_Login(string username, string password)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CLogin);
        buffer.WriteString(username);
        buffer.WriteString(password);
        SendData(buffer.ToArray());
    }
    public static void PACKAGE_Logout()
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CLogout);

        SendData(buffer.ToArray());
    }
    
    public static void PACKAGE_SendMovement(float x, float y, float z)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CSendMovement);
        buffer.WriteFloat(x);
        buffer.WriteFloat(y);
        buffer.WriteFloat(z);

        SendData(buffer.ToArray());


    }
    
    public static void PACKAGE_SendDrops(string mobName)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CMobDropName);

        buffer.WriteString(mobName);


        SendData(buffer.ToArray());
    }

    public static void PACKAGE_SendDestination(float x, float y, float z)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CSendDestination);
        buffer.WriteFloat(x);
        buffer.WriteFloat(y);
        buffer.WriteFloat(z);
        SendData(buffer.ToArray());

    }

    public static void PACKAGE_SDealDamage(int damage, string username)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CDealDamage);
        buffer.WriteInteger(damage);
        buffer.WriteString(username);
        SendData(buffer.ToArray());
        buffer.Dispose();
    }
    public static void PACKAGE_SDealDamageTo(int damage, int mobNameID, int mobID)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInteger((int)ClientPackages.CDealDamageTo);
        buffer.WriteInteger(damage);
        buffer.WriteInteger(mobNameID);
        buffer.WriteInteger(mobID);
        SendData(buffer.ToArray());
        buffer.Dispose();
    }

}
