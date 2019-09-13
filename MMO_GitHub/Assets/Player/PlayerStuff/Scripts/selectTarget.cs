using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class selectTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject target_;
    public GameObject SelcetedUI;
    public static GameObject currentTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target_ == null)
        {
            if(target == null)
            {
               
            }

        }
        if(target != target_)
        {

            if (target_ == null)
            {
                target.GetComponent<clickable>().isSelected = true ;

                target_ = target;
                currentTarget= target;
            }
            target_.GetComponent<clickable>().isSelected = false;
                target_ = target;
               currentTarget = target;
                target_.GetComponent<clickable>().isSelected = true;        
        }


    }
}
