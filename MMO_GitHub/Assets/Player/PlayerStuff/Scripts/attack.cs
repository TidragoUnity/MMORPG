using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    bool attAgain;
   
    [SerializeField]
    int damage;

    float timer = 0.0f;
    float waitTime = 2.4f;



    public static Animator anim;

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

                        Attack(selectTarget.currentTarget);
                        anim.SetBool("IsWalking", false);
                        anim.SetBool("IsAttacking", true);
                        timer = 0;
                        attAgain = true;
                        return;
                    }
                    if (attAgain)
                    {
                        Debug.Log("Attack");
                        Debug.Log(selectTarget.dead);
                        Attack(selectTarget.currentTarget);
                        timer = 0;
                        return;
                    }



                }
            }
        }
        else
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
