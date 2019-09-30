using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class otherPlayers : MonoBehaviour
{
    static private NavMeshAgent mNavMeshAgent;

    public  Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        MoveOtherPlayer();



    }

    private void MoveOtherPlayer()
    {        
        mNavMeshAgent.stoppingDistance = 0.8f;
        //legt das Zeil fest wohin es geht

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




}
