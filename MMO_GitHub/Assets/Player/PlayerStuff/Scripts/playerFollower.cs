using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollower : MonoBehaviour
{
    GameObject player;

    void Update()
    {
        GetPlayer();
        follow();
    }

    void GetPlayer()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
    }
    void follow()
    {
        if(player != null)
        {
            transform.position = player.transform.position;

        }
    }
}
