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
    [HideInInspector]
    public Vector3 StartPos;
    [HideInInspector]
    public Vector3 StartRot;
    public bool Gun;
    public int AmmoCant;
  
    Transform ItemMesh;
    void Awake()
    {
        StartScale = transform.localScale;
        StartPos = transform.localPosition;
        ItemMesh = GetComponentInChildren<MeshRenderer>().transform;
        StartRot = transform.localEulerAngles;
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
            Quaternion QRotation = Quaternion.Euler(Rotation);
            ItemMesh.localRotation =  Quaternion.Slerp(ItemMesh.localRotation , QRotation ,SmoothSpeed) ; 
        }
        else { 
         transform.localPosition = Vector3.SmoothDamp(transform.localPosition , StartPos , ref Velocity , SmoothSpeed);
         Quaternion QRotation = Quaternion.Euler(StartRot);
         ItemMesh.localRotation =  Quaternion.Slerp(ItemMesh.localRotation , Quaternion.identity ,SmoothSpeed) ;    
         transform.localRotation = Quaternion.Slerp(transform.localRotation , QRotation ,SmoothSpeed) ;    
        }
    }
}
