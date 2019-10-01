using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mobKI : MonoBehaviour
{

    private NavMeshAgent mNavMeshAgent;
    public Animator anim;
    GameObject target;
    public float distance;
    [SerializeField]
    int dmg;


    public float timer = 0.0f;
    float waitTime = 4.4f;


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

        if(target == null)
        {
            Detectplayer(transform.position, 10f);
        }

        Move();
        if(timer > waitTime) { anim.SetBool("attack", false); }
        if(timer < waitTime || target == null)
        {
            return;
        }
        attackPlayer();
       


    }

    private void Move()
    {
        if(gameObject == null) { return; }
        //legt das Zeil fest wohin es geht
        if(target == null) { return; }
        mNavMeshAgent.destination = target.transform.position;
        resetAnimation();
        mNavMeshAgent.stoppingDistance = 3f;
        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {

            anim.SetBool("run", false);

        }
        else
        {
            anim.SetBool("run", true);

        }
    }

    void Detectplayer(Vector3 center, float radius)
    {
         Collider[] hitColliders = Physics.OverlapSphere(center, radius);
         int i = 0;
         while (i < hitColliders.Length)
      {
            if(hitColliders[i].tag == "Player")
            {
                target = hitColliders[i].gameObject;
            }
           i++;
      }
    }
    
    void attackPlayer()
    {
        Distance(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        if (distance < 3)
        {
           
                try
                {
                    if (target != null)
                    {
                    transform.LookAt(target.transform);
                    resetAnimation();
                    anim.SetBool("attack", true);
                        target.GetComponent<stats>().takeDMG(dmg);
                        timer = 0;
                    }

                }
                catch (System.Exception)
                {

                    throw;
                }

            
        }

    }
    // Brechnet den Abstand zwischen sich und den ausgewählten Objekt
    void Distance(float x1, float y1, float z1)
    {
        float x2 = transform.position.x - x1;
        float y2 = transform.position.y - y1;
        float z2 = transform.position.z - z1;

        distance = Mathf.Sqrt((x2 * x2) + (y2 * y2) + (z2 * z2));

    }


    void resetAnimation()
    {
        anim.SetBool("dead", false);
        anim.SetBool("run", false);
        anim.SetBool("idle", true);
        anim.SetBool("attack", false);
    }


}
