using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothTime;
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
        //Tu tranquilo q esto de aca abajo encuentra el transform q hace falta
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider>().gameObject.transform;
    }

    void Update()
    {
       MovePosition.x = player.position.x;
       MovePosition.y = player.position.y + Offset.y;
       MovePosition.z = player.position.z + Offset.z;
      
        if(Input.GetKey(KeyCode.Q)){
            MovePosition -=  cam.transform.right; 
            
        }
         if (Input.GetMouseButtonDown(1))
        {
            Shake();
        }
 
        //cam.transform.position = Vector3.SmoothDamp( cam.transform.position,MovePosition,ref Velocity , smoothTime) ;
        cam.transform.LookAt(player.transform);
        //Al dar click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
     
       
        }

        //Al dar click derecho
       

      

        //ZOOOooomm
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
           cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * ZoomSensivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, MinZoomScale, MaxZoomScale);
    }

   public void Shake(){
       MovePosition += Random.insideUnitSphere;
   }
 
}    