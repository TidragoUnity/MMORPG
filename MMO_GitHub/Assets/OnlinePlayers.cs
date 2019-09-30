using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayers : MonoBehaviour
{
    public GameObject playerP;
    public static GameObject player;
    public static Vector3 vec3;
    public static bool isSpawned;
    // Start is called before the first frame update
    void Start()
    {
        player = playerP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Logout()
    {
        ClientTCP.PACKAGE_Logout();
        Application.Quit();
        
    }


    public static void SpawnPlayer(float x, float y, float z)
    {
        vec3 = new Vector3(x, y, z);
        Instantiate(player, vec3, Quaternion.identity);
 
    }
    public static void SpawnPlayer(float x, float y, float z, string username)
    {
        try
        {
            vec3 = new Vector3(x, y, z);
            player.name = username;
            player.tag = "otherPlayers";
            player.GetComponent<otherPlayers>().enabled=true;
            Instantiate(player, vec3, Quaternion.identity);
            if (isSpawned != true) isSpawned = true;
        }
        catch (System.Exception)
        {
            vec3 = new Vector3(x, y, z);
            player.name = username;
            Instantiate(player, vec3, Quaternion.identity);
            if (isSpawned != true) isSpawned = true;
            throw;
        }

    }
}
