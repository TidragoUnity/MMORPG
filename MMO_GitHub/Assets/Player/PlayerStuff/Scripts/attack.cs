﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField]
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Attack(selectTarget.currentTarget);
        }

    }


    void Attack(GameObject obj)
    {
        obj.GetComponent<stats>().changeHealth(damage);

    }
}
