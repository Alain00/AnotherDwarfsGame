using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brother : MonoBehaviour
{
    Transform player;
    public float radius;
    bool Open;
    public GameObject Store;
    FollowPlayer CameraController;
    public PlayerController Player;
    public GameObject CloseText;
    public GameObject ExclamationSign;
    void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>();
        //transform.position =  SetPosInWorld.instance.SetPos(transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraController = GameObject.FindObjectOfType<FollowPlayer>();
        Store.SetActive(false);
        CloseText.SetActive(false);
        ExclamationSign.SetActive(false);
        gameObject.SetActive(false);
    }

    
    void Update()
    {
        if(Vector3.Distance(transform.position , player.position )  < radius ){
            if(Input.GetKeyDown(KeyCode.Space)){
                Open = !Open;
                OpenStore(Open); 
            }   
        }
    }
    void OpenStore( bool Open ){
        if(Open)
        CameraController.Offset += Vector3.up * 15;
        else  CameraController.Offset -= Vector3.up * 15;
        Store.SetActive(Open);
        CloseText.SetActive(Open);
        ExclamationSign.SetActive(Open);
        Player.enabled = !Open;
       
    }
}
