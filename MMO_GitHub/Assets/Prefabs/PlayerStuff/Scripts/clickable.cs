using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickable : MonoBehaviour
{
    public Sprite icon;

    public bool isSelected = false;
    void Start()
    {

    }

    private void OnMouseDown()
    {
        GameObject player = GameObject.Find("Player(Clone)");

        player.GetComponent<selectTarget>().target = this.gameObject;
        Image Icon = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectedHealthBarPanel/HealthBar/Icon").GetComponent<Image>();
      
        Icon.GetComponent<Image>().sprite = icon;
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
          //  Image Icon = GameObject.Find("SelectedHealthBarPanel/HealthBar/Mask/Icon").GetComponent<Image>();
          //  Icon.GetComponent<Image>().sprite = icon;
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
           // GameObject Icon = GameObject.Find("SelectedHeathBarPanel/HealthBar/Mask/Icon");
         //   Icon.GetComponent<Image>().sprite = null;
            //if it still highlighted remove it and go on;
        }
    }
    
}
