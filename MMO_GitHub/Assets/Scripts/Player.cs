using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    Vector3 newPosition;
    float x, y, z;


    void Start()
    {
        newPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition();

        }
        SendXYZ();

    }

    private void PlayerInventory()
    {

    }
    private void Equipment()
    {

    }


    void MousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.ToString() == "MoveGrid (UnityEngine.Transform)")
            {
                newPosition = hit.point;
                transform.position = new Vector3(newPosition.x, newPosition.y, 0);
            }
            else
            {
                Debug.Log("Didn't hit the MoveGrid");
                return;
            }

        }

        void MovePlayer(float x, float y, float z)
        {
            player.transform.position = new Vector3(x, y, z);
            ClientHandleData.informationOutput.text = "Trying to send Position to the Server";
        }




    }
    private void SendXYZ()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        ClientTCP.PACKAGE_SendMovement(x, y, z);

    }
}
