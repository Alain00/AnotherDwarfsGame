using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float movementspeed;
    Vector3 MousePos;
    Vector3 MovePosition;
    Camera cam;
    public float ZoomSensivity;
    public float MinZoomScale;
    public float MaxZoomScale;
    Vector3 Velocity;
    public Vector3 Offset;
   

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
       MovePosition.x = player.position.x;
       MovePosition.y = player.position.y + Offset.y;
       MovePosition.z = player.position.z + Offset.z;
      
         
        cam.transform.position = Vector3.SmoothDamp(transform.position , MovePosition , ref Velocity , movementspeed);
    
        //Al dar click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
     
       
        }

        //Al dar click derecho
        if (Input.GetMouseButtonDown(1))
        {
        }

        //ZOOOooomm
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
           cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * ZoomSensivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, MinZoomScale, MaxZoomScale);
    }

    //Mover la camara
    void LateUpdate()
    {
       
    }
 
}    