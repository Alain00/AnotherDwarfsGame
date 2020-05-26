using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableItem : MonoBehaviour
{
    public bool hover;
    public bool Focus;
    Vector3 Velocity;
    Vector3 RotVelocity;
    public float SmoothSpeed;
    public Vector3 CamDist;
    public Vector3 Rotation;
    Vector3 StartScale;
    Vector3 StartPos;
    Vector3 StartRot;
  
    Transform ItemMesh;
    void Start()
    {
        StartScale = transform.localScale;
        StartPos = transform.position;
        ItemMesh = GetComponentInChildren<MeshRenderer>().transform;
        StartRot = ItemMesh.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(hover){
            transform.localScale =  StartScale * 1.3f ;
        }
        else transform.localScale = StartScale;

        if(Focus){
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition , CamDist , ref Velocity , SmoothSpeed);
            transform.eulerAngles = Rotation; 
        }
        else {
         ItemMesh.eulerAngles = StartRot; 
         transform.position = Vector3.SmoothDamp(transform.position , StartPos , ref Velocity , SmoothSpeed);
        }
    }
}
