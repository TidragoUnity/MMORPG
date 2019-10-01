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

    private void OnMouseUpAsButton()
    {
        GameObject player = GameObject.Find("Player(Clone)");

        player.GetComponent<selectTarget>().target = this.gameObject;
        Image Icon = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar/Icon").GetComponent<Image>();

        Icon.GetComponent<Image>().sprite = icon;
        Text targetName = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar/Text").GetComponent<Text>();

        
        targetName.text = this.gameObject.name;
        targetName.text = targetName.text.Replace("(Clone)", "");

    }



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

        }
        else
        {
            Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
            {
                Material m = r.material;
                m.color = Color.white;
            }

        }
    }
    
}
