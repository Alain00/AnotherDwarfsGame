using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIsland : MonoBehaviour
{
    public GameObject ship;
    public float landPosOffset = 30;
    public float airPosOffset = 20;

    Vector3 Destiny;
    Vector3 StartPos;
    public bool leave;
    Vector3 velocity;
    public float SmoothSpeed;
    public Brother bro;
    bool Once;

    public AudioSource IgnitionSFX;
    void Awake(){
         bro = GameObject.FindObjectOfType<Brother>();
    }
    void Start()
    {
        Destiny = SetPosInWorld.instance.SetPos(ship.transform.position);
        ship.transform.position = Destiny + transform.right * landPosOffset;
        StartPos = ship.transform.position;
        //gameObject.SetActive(false);
    }

   
    void Update()
    {
        if(leave){
            ship.transform.position = Vector3.SmoothDamp(ship.transform.position , StartPos + transform.right * airPosOffset , ref velocity , SmoothSpeed );
            
        }
        else{   
            ship.transform.position = Vector3.SmoothDamp(ship.transform.position , Destiny , ref velocity , SmoothSpeed );
            
        }
    }

    public void Leave(){
        IgnitionSFX.Play();
        Once = false;
        bro.gameObject.SetActive(false);
        leave = true;
        Invoke("Restart", SmoothSpeed);
    }

    public void ComeBack(){
        IgnitionSFX.Play();
        ship.SetActive(true);
        leave = false;
        if(!Once){
            Invoke("Arrive", SmoothSpeed);
            Once = true;
        }
    }

    public void Restart(){
        ship.SetActive(false);
        
    }
    public void Arrive(){
        if (leave) return;
        bro.transform.position = ship.transform.position + ship.transform.up * 5 + ship.transform.right * 5;
        bro.gameObject.SetActive(true);
        bro.transform.position = SetPosInWorld.instance.SetPos(bro.transform.position);
    }
}
