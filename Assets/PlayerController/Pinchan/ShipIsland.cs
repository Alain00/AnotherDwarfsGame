using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIsland : MonoBehaviour
{
    Vector3 Destiny;
    Vector3 StartPos;
    public bool leave;
    Vector3 velocity;
    public float SmoothSpeed;
    public GameObject Brother;
    bool Once;
    void Start()
    {
        Destiny = SetPosInWorld.instance.SetPos(transform.position);
        transform.position = Destiny + transform.right * 30;
        StartPos = transform.position;
        gameObject.SetActive(false);
    }

   
    void Update()
    {
        if(leave){
            transform.position = Vector3.SmoothDamp(transform.position , StartPos + transform.right * 20 , ref velocity , SmoothSpeed );
            Brother.SetActive(false);
            Invoke("Restart", SmoothSpeed * 3f);
        }
        else{   
            transform.position = Vector3.SmoothDamp(transform.position , Destiny , ref velocity , SmoothSpeed );
            if(!Once){
            Invoke("Arrive", SmoothSpeed * 3f);
             Once = true;
            }
        }
    }
    public void Restart(){
        gameObject.SetActive(false);
        
    }
    public void Arrive(){
        Brother.transform.position = transform.position + transform.up * 5 + transform.right * 5;
        Brother.SetActive(true);
        Brother.transform.position = SetPosInWorld.instance.SetPos(Brother.transform.position);
    }
}
