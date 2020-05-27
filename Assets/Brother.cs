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
    public GameObject Player;
    void Awake()
    {
        transform.position =  SetPosInWorld.instance.SetPos(transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CameraController = GameObject.FindObjectOfType<FollowPlayer>();
        Store.SetActive(false);
    }

    
    void Update()
    {
        if(player.position.sqrMagnitude < radius * radius){
            if(Input.GetKeyDown(KeyCode.Space) && Open == false){
                OpenStore();
                Open = true;
                
            }   
        }
    }
    void OpenStore(){
        CameraController.Offset += Vector3.up * 15;
        Store.SetActive(true);
        /*GameObject store = Instantiate(Store , transform.position , Quaternion.identity , Camera.main.transform);
        store.transform.localPosition = Vector3.zero;
        store.transform.localEulerAngles = Vector3.zero;
        store.transform.localPosition = transform.forward * 15;*/
        Player.SetActive(false);
    }
}
