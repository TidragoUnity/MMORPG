using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selectObject();
    }

    void selectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if( Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.root.gameObject;
            selectObject(hitObject);
            Debug.Log(hitObject.name);
        }
        else
        {

        }

       void selectObject(GameObject obj){
            if (selectedObject != null)
            {
                if (obj == selectedObject)
                    return;
                ClearSelection();
            }



            selectedObject = obj;

        }
    }
    void ClearSelection()
    {
        selectedObject = null;
    }
}
