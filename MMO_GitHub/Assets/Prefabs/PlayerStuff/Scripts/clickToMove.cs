using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class clickToMove : MonoBehaviour
{
    int i = 0;
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
        if(i > 30)
        {
            SendXYZ();
            i = 0;
        }
        else
        {
            i++;
        }



    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.transform.tag == "enemy")
                {

                }else
                {
                    mNavMeshAgent.destination = hit.point;

                }
            }
        }

        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
            //  SendPosition();
        }
        else
        {

        }
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
