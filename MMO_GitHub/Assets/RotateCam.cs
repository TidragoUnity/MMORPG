using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;
    public static bool changeCamera = false;
    bool changedCamera = false;

    public GameObject camGam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            if(X > 0)
            {
                X = -2;
            }
            transform.rotation = Quaternion.Euler(X, Y, 0);
            
        }

        if (changeCamera && !changedCamera)
        {
            if(gameObject.transform.root.name == "Player(Clone)")
            {
                Debug.Log("ChangedCamera");
                camGam.SetActive(true);
                Camera cam = camGam.GetComponentInChildren<Camera>();
                Camera oldCam = GameObject.Find("MenuCamera").GetComponent<Camera>();
                oldCam.gameObject.SetActive(false);
                changedCamera = true;
            }

        }
    }
}

