using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class IconScript:MonoBehaviour
{
     public Sprite[] Icons;

    private void Update()
    {
       
    }


    public void changeIcon(int i)
    {
        this.GetComponent<Image>().sprite = Icons[i];
    }
}
