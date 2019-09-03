﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;
    public static bool changeCamera = false;
    bool changedCamera = false;

    public GameObject player;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
        follow();
        if (changeCamera && !changedCamera)
        {
            player = GameObject.Find("Player(Clone)");
            changedCamera = true;
        }
    }

    void follow()
    {
        transform.position = new Vector3(player.transform.position.x , player.transform.position.y +2, player.transform.position.z);

    }
}
