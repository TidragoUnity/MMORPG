﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSrcipt : MonoBehaviour
{

    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HandleBar();   
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }
    private void HandleBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }

    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax )
    {

        return (value - inMin)*(outMax - outMin) / (inMax- inMin)+ outMin;
    }

    public float MapHealth(float value, float inMin, float inMax,float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    public void ChangeValue(float value)
    {
        fillAmount = value;
    }

}
