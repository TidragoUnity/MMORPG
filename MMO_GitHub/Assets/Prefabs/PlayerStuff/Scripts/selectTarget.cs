using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject target_;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != target_)
        {
            if(target_ == null)
            {
                target.GetComponent<clickable>().isSelected = true ;
                target_ = target;
            }
            target_.GetComponent<clickable>().isSelected = false;
                target_ = target;
                target_.GetComponent<clickable>().isSelected = true;        
        }

    }
}
