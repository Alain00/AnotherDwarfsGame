using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Transform crosshair;
    PlayerController controller;
    // Start is called before the first frame update
    void Start(){
        if (crosshair == null) crosshair = this.transform;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Cursor.visible = false;
    }

    void Update(){
        transform.position = controller.lastSeenPoint;
        if (Input.GetKeyDown(KeyCode.F5)){
            Cursor.visible = !Cursor.visible;
        }
    }
}
