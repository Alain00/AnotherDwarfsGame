using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 MovePosition; 
    public Transform player; 
    public Vector3 Offset;  
    
    void Start()
    {
        //Tu tranquilo q esto de aca abajo encuentra el transform q hace falta
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Collider>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
       MovePosition.x = player.position.x;
       MovePosition.y = player.position.y + Offset.y;
       MovePosition.z = player.position.z + Offset.z;
       transform.position = MovePosition;
    }
}
