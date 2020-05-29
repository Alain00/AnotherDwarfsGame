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
    public Brother bro;
    bool Once;
    void Awake(){
         bro = GameObject.FindObjectOfType<Brother>();
    }
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
            bro.gameObject.SetActive(false);
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
        bro.transform.position = transform.position + transform.up * 5 + transform.right * 5;
        bro.gameObject.SetActive(true);
        bro.transform.position = SetPosInWorld.instance.SetPos(bro.transform.position);
    }
}
