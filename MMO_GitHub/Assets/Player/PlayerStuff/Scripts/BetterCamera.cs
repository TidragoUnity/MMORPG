using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterCamera : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    [SerializeField]
    float speed = 3.5f;
    float X;
    float Y;
    public static bool changeCam = false;
    bool changedCamera = false;
    bool oneTime = false;

    void Update()
    {
       // getTarget();
        changeCamera();
        rotateCamera();
        cameraZoom();
    }

    void getTarget()
    {
        if(target == null)
        {
            try
            {
                target = GameObject.Find("Player").transform;

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
    void changeCamera()
    {
        if (changeCam && !changedCamera)
        {
            Camera oldCam = GameObject.Find("MenuCamera").GetComponent<Camera>();
            oldCam.gameObject.SetActive(false);
            changedCamera = true;            
            cam.tag = "MainCamera";

        }
    }
    void rotateCamera()
    {
        Vector3 vec3 = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);

        
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            if (X > 0)
            {
                X = -2;
            }
            transform.rotation = Quaternion.Euler(X, Y, 0);

        }
        

        
    }
    void cameraZoom()
    {
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           
        if (ScrollWheelChange != 0)
        {                                                                       //If the scrollwheel has changed
            float R = ScrollWheelChange * 15;                                   //The radius from current camera
            float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
            float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right
            PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
            PosY = PosY / 180 * Mathf.PI;                                       //^
            float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
            float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
            float Y = R * Mathf.Cos(PosX);                                      //^
            float CamX = Camera.main.transform.position.x;                      //Get current camera postition for the offset
            float CamY = Camera.main.transform.position.y;                      //^
            float CamZ = Camera.main.transform.position.z;                      //^
            Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
        }
    }
}
