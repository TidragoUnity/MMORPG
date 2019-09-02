using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class clickToMove : MonoBehaviour
{
    private string oldX, oldY, oldZ;
    private NavMeshAgent mNavMeshAgent;
    private float x, y, z;


    // Start is called before the first frame update
    void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        SendXYZ();

    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 100))
            {
                mNavMeshAgent.destination = hit.point;
            }
        }

        if(mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
          //  SendPosition();
        }
        else
        {

        }


    }
    private void SendPosition()
    {
        /*

        if (OnlinePlayers.isSpawned)
        {
            GameObject player = GameObject.Find("Player(Clone)");
            string x = Mathf.Round(player.transform.position.x).ToString();
            string y = Mathf.Round(player.transform.position.y).ToString();
            string z = Mathf.Round(player.transform.position.z).ToString();



            if(!string.Equals(oldX,x)||!string.Equals(oldY,y)||!string.Equals(oldZ, z))
            {
                Debug.Log(x + "  " + y + "  " + z);
                ClientTCP.PACKAGE_SendMovement(x, y, z);
                oldX = x;
                oldY = y;
                oldZ = z;          
            }
            else
            {
                return;
            }

            /*
            if(string.Equals(oldX, x))
            {
                return;
            }
            else
            {
                ClientTCP.PACKAGE_SendMovement(x, y, z);
                oldX = x;



            }
            if (string.Equals(oldY, y))
            {
                return;
            }
            else
            {
                    ClientTCP.PACKAGE_SendMovement(x, y, z);
                    oldY = y;


            }
            if (string.Equals(oldZ, z))
            {

                return;


            }
            else
            {
                ClientTCP.PACKAGE_SendMovement(x, y, z);
                oldZ = z;

            }
            Debug.Log(x + "  " + y + "  " + z);
            

        }
    */
        
    }
    private void SendXYZ()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        ClientTCP.PACKAGE_SendMovement(x, y, z);
        Debug.Log(x + " " + y + "  " + z);
    }
}
