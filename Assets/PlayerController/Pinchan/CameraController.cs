 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public float smoothTime;
    Vector3 MousePos;
    
    Camera cam;
    public float ZoomSensivity;
    public float MinZoomScale;
    public float MaxZoomScale;
    Vector3 Velocity; 
    FollowPlayer Holder;

    void Start()
    {
        Holder = GetComponentInParent<FollowPlayer>();
        cam = Camera.main;
        
    }

    void Update()
    {

        transform.position = Vector3.SmoothDamp(transform.position , Holder.transform.position , ref Velocity , smoothTime);
        //cam.transform.LookAt(player.transform);
        //ZOOOooomm
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
           cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * ZoomSensivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, MinZoomScale, MaxZoomScale);
    }
 
}    