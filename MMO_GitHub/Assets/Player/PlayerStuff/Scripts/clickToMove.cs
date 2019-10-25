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

    static GameObject staticTarget;
    GameObject oldTarget;
    static Vector3 targetPos;
    public Animator anim;

    static bool follow;
    float time;
    float waitTime =4;

    // Start is called before the first frame update
    void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        Move();
  
        if(time > waitTime)
        {
            SendXYZ();
            followTarget();
            time = 0;
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
                    if(oldTarget != null)
                    {
                        targetPos = oldTarget.transform.position;
                    }


                }
                else 
                {

                }
            }
        }
        anim.SetBool("IsWalking", true);
        mNavMeshAgent.speed = 2.0f;
        anim.SetBool("IsRunning", true);
        mNavMeshAgent.speed = 6.0f;
        follow = true;
        // stoppt den Agent
        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {

            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            mNavMeshAgent.speed = 0.0f;



            follow = false;
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

    public static void newDestination(GameObject target)
    {
        mNavMeshAgent.destination = target.transform.position;
        mNavMeshAgent.stoppingDistance = 2.5f;
        ClientTCP.PACKAGE_SendDestination(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        staticTarget = target;
        targetPos = target.transform.position;
        follow = true;
    }
    void followTarget()
    {
        if(follow != true) { return; }
        if(selectTarget.currentTarget == null) { return; }
        if (staticTarget == null){ return; }
        if(staticTarget.transform.position != targetPos)
        {

            newDestination(staticTarget);
           
        }

    }

}
