using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class otherPlayers : MonoBehaviour
{
    private NavMeshAgent mNavMeshAgent;

    public Animator anim;
    float timer = 0.0f;
    float waitTime = 2.4f;

    // Start is called before the first frame update
    void Start()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        MoveOtherPlayer();

        if (timer > waitTime)
        {
            anim.SetBool("IsAttackingMob", false);

        }

    }

    private void MoveOtherPlayer()
    {
        mNavMeshAgent.stoppingDistance = 0.8f;
        //legt das Zeil fest wohin es geht

        anim.SetBool("IsWalking", true);
        mNavMeshAgent.speed = 2.0f;
        anim.SetBool("IsRunning", true);
        mNavMeshAgent.speed = 6.0f;

        // stoppt den Agent
        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {

            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            mNavMeshAgent.speed = 0.0f;
        }
        else
        {

        }
    }
    public void AttackAnimation(GameObject target)
    {

        transform.LookAt(target.transform);
        anim.SetBool("IsAttackingMob", true);
        Debug.Log("Is Attacking the mob //" + transform.root.name);
        timer = 0;





    }

}
