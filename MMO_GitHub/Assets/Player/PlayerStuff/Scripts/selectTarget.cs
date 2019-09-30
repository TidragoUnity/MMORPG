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

    static public float distance;
    public float showDistance;
    public static bool dead = false;

    public bool dead2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        showDistance = distance;
        if(target_ == null)
        {
            if(target == null)
            {
               
            }

        }
        if(target != target_)
        {

            if (target_ == null&& target != null)
            {
                target.GetComponent<clickable>().isSelected = true ;
                target.GetComponent<stats>().UpdateHealthbar();
                target_ = target;
                currentTarget= target;

                attack.nextOperationAtt = false;

                dead = false;
                
            }
            target_.GetComponent<clickable>().isSelected = false;
                target_ = target;
            target.GetComponent<stats>().UpdateHealthbar();
            currentTarget = target;
                target_.GetComponent<clickable>().isSelected = true;
            attack.nextOperationAtt = false;


        }
        if (currentTarget != null)
        {
            Distance(currentTarget.transform.position.x, currentTarget.transform.position.y, currentTarget.transform.position.z);
        }
        if (dead)
        {
            target = null;
            target_ = null;
            currentTarget = null;
            attack.nextOperationAtt = false;

        }


    }


    // Dateted die Lebensanzeige im UI ab
    void UpdateBar()
    {
        GameObject HealthBar = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectedHealthBarPanel/HealthBar");
        HealthBar.GetComponent<stats>().UpdateHealthbar();

    }

    // Brechnet den Abstand zwischen sich und den ausgewählten Objekt
    void Distance(float x1, float y1, float z1)
    {
        float x2 = transform.position.x - x1;
        float y2 = transform.position.y - y1;
        float z2 = transform.position.z - z1;

        distance = Mathf.Sqrt((x2 * x2) + (y2 * y2) + (z2 * z2));

    }
}
