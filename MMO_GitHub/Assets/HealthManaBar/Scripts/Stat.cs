using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private BarSrcipt bar;
    private float maxVal;
    private float currentVal;

    public float CurrentVal
    {
        get { return currentVal; }
        set
        {
            currentVal = value;
            bar.Value = currentVal;
        }

    }

    public float MaxVal {
        get { return maxVal; }

        set { bar.MaxValue = value;
             maxVal = value;
        }
    }
}

    


