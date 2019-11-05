using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class mobKI : MonoBehaviour
{

    private NavMeshAgent mNavMeshAgent;
    public Animator anim;
    GameObject target;
    GameObject player;
    public float distance;
    public float dist;
    [SerializeField]
    int dmg;
    TextMeshProUGUI holderPro;
    Text holder;
    public Text mobUi;
    public TextMeshProUGUI mobUiPro;

    public Transform nameTag;
    public float timer = 0.0f;
    float waitTime = 4.4f;
    public float UiRange = 60f;

    void Start()
    {
        player = GameObject.Find("Player");

        CreateUI();

        mNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }


    void Update()
    {

        activateUI();
        followUI();
        timer += Time.deltaTime;

        if (target == null)
        {
            Detectplayer(transform.position, 10f);
        }

        Move();
        if (timer > waitTime) { anim.SetBool("attack", false); }
        if (timer < waitTime || target == null)
        {
            return;
        }
        attackPlayer();




    }

    private void Move()
    {
        if (gameObject == null) { return; }
        //legt das Zeil fest wohin es geht
        if (target == null) { return; }
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
            if (hitColliders[i].tag == "Player")
            {
                target = hitColliders[i].gameObject;
                ClientTCP.PACKAGE_MobMove(GetComponent<stats>().type, GetComponent<stats>().MobID);
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
                    //    target.GetComponent<stats>().takeDMG(dmg);
                    ClientTCP.PACKAGE_DealDamage(dmg, target.name);
                    Debug.Log(target.name);
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
        outOfRange();
    }
    void outOfRange()
    {
        if (distance > 50f)
        {
            target = null;
            mNavMeshAgent.SetDestination(transform.position);
            anim.SetBool("run", false);

        }
    }

    void resetAnimation()
    {
        anim.SetBool("dead", false);
        anim.SetBool("run", false);
        anim.SetBool("idle", true);
        anim.SetBool("attack", false);
    }

    public void selectTarget(string username)
    {
        GameObject newtarget = GameObject.Find(username);
        target = newtarget;
        Debug.Log("I have a new target!!!");
    }


    private void CreateUI()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(nameTag.transform.position);
        //holder = (Instantiate(mobUi, pos, Quaternion.identity));
        //holder.transform.parent = GameObject.Find("Canvas/OtherInterfaces/MobUi").transform;
        //holder.text = transform.name.Replace("(Clone)", "");
        holderPro = (Instantiate(mobUiPro, pos, Quaternion.identity));
        holderPro.transform.parent = GameObject.Find("Canvas/OtherInterfaces/MobUi").transform;
        holderPro.text = transform.name.Replace("(Clone)", "");


    }
    void followUI()
    {
        //holder.transform.position = Camera.main.WorldToScreenPoint(nameTag.transform.position);
        holderPro.transform.position = Camera.main.WorldToScreenPoint(nameTag.transform.position);


    }

    void activateUI()
    {
        float x2 = transform.position.x - player.transform.position.x;
        float y2 = transform.position.y - player.transform.position.y;
        float z2 = transform.position.z - player.transform.position.z;

        dist = Mathf.Sqrt((x2 * x2) + (y2 * y2) + (z2 * z2));
        if (dist < UiRange )
        {
            holderPro.gameObject.SetActive(true);
            Debug.Log("InRangeUI" + dist);
        }
        else
        {
            holderPro.gameObject.SetActive(false);
            Debug.Log("OutRangeUI" + dist);

        }
    }
    public void deleteNameTag()
    {
        GameObject.Destroy(holderPro);
    }
}
