﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    bool attAgain;
    public static bool nextOperationAtt;
    [SerializeField]
    int damage;

    float timer = 0.0f;
    float waitTime = 2.4f;





    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim =GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (selectTarget.distance < 2.5f)
        {
            if (timer > waitTime)
            {
                if(selectTarget.dead == true) { attAgain = false; anim.SetBool("IsAttacking", false); }else
                {
                    if (Input.GetKeyDown("1"))
                    {

                        Transform target = selectTarget.currentTarget.transform;
                        transform.LookAt(target);


                        Attack(selectTarget.currentTarget);
                        anim.SetBool("IsWalking", false);
                        anim.SetBool("IsAttacking", true);
                        timer = 0;
                        attAgain = true;
                        return;
                    }
                    if (attAgain)
                    {
                        //schaut auf das andere objekt
                        Transform target = selectTarget.currentTarget.transform;
                        transform.LookAt(target);
                        //greift das ausgewählte objekt an
                        Attack(selectTarget.currentTarget);
                        //setzt den Timer zurück
                        timer = 0;
                        return;
                    }
                    if (nextOperationAtt)
                    {
                        //schaut auf das andere objekt
                        Transform target = selectTarget.currentTarget.transform;
                        transform.LookAt(target);
                        //change animation
                        anim.SetBool("IsWalking", false);
                        anim.SetBool("IsAttacking", true);
                        //attacks the target
                        Attack(selectTarget.currentTarget);
                        attAgain = true;
                        //resets the timer
                        timer = 0;
                        nextOperationAtt = false;
                        return;
                    }
                }
            }
        }
        else if (Input.GetKeyDown("1"))
        {
            //Legt das neue Ziel fest
            Transform target = selectTarget.currentTarget.transform;
            clickToMove.newDestination(target.transform.position);
            nextOperationAtt = true;

        }else
        {
            attAgain = false;
            anim.SetBool("IsAttacking", false);
        }






    }



    void Attack(GameObject obj)
    {
        try
        {
            if(obj != null)
            {
                obj.GetComponent<stats>().changeHealth(damage);

            }

        }
        catch (System.Exception)
        {

            throw;
        }

    }
}
