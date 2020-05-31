using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirillaController : MonoBehaviour
{
    public Transform Mirilla;
    public PlayerController PController;
    LayerMask layerMask;
    void Start()
    {
        Cursor.visible = false;
        PController = GetComponent<PlayerController>();
        layerMask = PController.layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       
       if(Physics.Raycast(ray.origin , ray.direction , out hit , 1000, layerMask)){
            Vector3 hitPos = hit.point;
            hitPos.y += 2;
            Mirilla.position = hitPos;
       }    
       
        
        //Cursor.SetCursor(Mirilla , Vector2.zero , CursorMode.ForceSoftware);
    }
}
