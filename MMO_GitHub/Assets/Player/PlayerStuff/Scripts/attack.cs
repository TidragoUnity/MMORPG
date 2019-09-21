using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    bool pressed;
    [SerializeField]
    int damage;

    float timer = 0.0f;
    float waitTime = 1.9f;

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
        if (clickToMove.InAttackRange)
        {
            if (timer > waitTime)
            {
                if (Input.GetKeyDown("1"))
                {

                    Attack(selectTarget.currentTarget);
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsAttacking", true);
                    timer = 0;


                    return;
                }
            }
        }

        anim.SetBool("IsAttacking", false);


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
