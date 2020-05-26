using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverManager : MonoBehaviour
{
    public string InteractableTag;
    Transform LastItem;
    BuyableItem Current;
    public float RotSpeed;
    bool IsFocusing;
    
    public Transform[] Items;
    public string[] Descripcion;

    public GameObject ItemWindow;
    public Text TextField;
    public Animator Animator;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(1) && IsFocusing == true){
            ItemWindow.SetActive(false);
            Current.Focus = false;
            IsFocusing = false;
        }
       
        
        if(Input.GetMouseButton(0)){
            if(LastItem != null){
                if(!Current.Focus){
                    ItemWindow.SetActive(true);
                    Animator.SetBool("OpenWindow",true);
                    for(int i = 0 ; i < Items.Length ; i++)
                        if(Items[i] == LastItem)
                            TextField.text = Descripcion[i];   
                        
                        Current.Focus = true;
                        IsFocusing = true;
                        Current.hover = false;
                }    
            float RotationY = Input.GetAxis("Mouse Y") * RotSpeed * Mathf.Deg2Rad;
            LastItem.GetComponentInChildren<MeshRenderer>().transform.Rotate(Vector3.up , RotationY);
            }
        }
        else if(IsFocusing == false){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray , out hit)){
                if(LastItem != hit.transform && LastItem != null){
                Current =  LastItem.GetComponent<BuyableItem>();
                    LastItem = null;
                    if(Current != null)
                        Current.hover = false; 
                }
                if(hit.transform.CompareTag(InteractableTag)){
                    Current = hit.transform.GetComponent<BuyableItem>();
                    if(!Current.Focus)
                        Current.hover = true;
                    LastItem = hit.transform;
                }
            
            }else {
                    LastItem = null;
                    if(Current != null)
                        Current.hover = false; 
            }
        }
      
    }
}
