using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class clickToMove : MonoBehaviour
{
    int i = 0;
    private string oldX, oldY, oldZ;
    static private NavMeshAgent mNavMeshAgent;
    private float x, y, z;
    public Camera cam;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        if(i > 8)
        {
            //SendXYZ();
            i = 0;
        }
        else
        {
            i++;
        }



    }

    private void Move()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.transform.tag == "floor")
                {
                    mNavMeshAgent.stoppingDistance = 0.8f;

                    //legt das Zeil fest wohin es geht
                    mNavMeshAgent.destination = hit.point;
                    ClientTCP.PACKAGE_SendDestination(hit.point.x, hit.point.y, hit.point.z);


                }
                else 
                {

                }
            }
        }
        anim.SetBool("IsWalking", true);

        // stoppt den Agent
        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {

            anim.SetBool("IsWalking", false);

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

    }

    public static void newDestination(Vector3 vec3)
    {
        mNavMeshAgent.destination = vec3;
        mNavMeshAgent.stoppingDistance = 2.0f;
        ClientTCP.PACKAGE_SendDestination(vec3.x, vec3.y, vec3.z);
    }
}
