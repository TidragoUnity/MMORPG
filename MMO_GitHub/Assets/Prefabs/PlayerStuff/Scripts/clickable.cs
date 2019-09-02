using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickable : MonoBehaviour
{

    public bool isSelected = false;
    void Start()
    {

    }

    private void OnMouseDown()
    {
        GameObject player = GameObject.Find("Player(Clone)");

        player.GetComponent<selectTarget>().target = this.gameObject;
    }
    // Start is called before the first frame update



    // Update is called once per frame
    void Update()
    {
        if (isSelected == true)
        {
            Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
            foreach(Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.red;
            }
                //highlight this gameobject
        }
        else
        {
            Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.white;
            }
            //if it still highlighted remove it and go on;
        }
    }
    
}
