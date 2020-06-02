using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public PlayerInventory inventory;
    public string InteractableTag;
    Transform LastItem;
    public BuyableItem Current;
    public float RotSpeed;
    public bool IsFocusing;
    
    public Transform[] Items;
    public string[] Descripcion;
    public int[] Price;
    int indice;

    public GameObject ItemWindow;
    public Text TextField;
    public Text PriceField;
    public Animator Animator;
    public AudioClip HoverSFX;
    public AudioClip OnClickSFX;
    public AudioSource audioSource;
    public Camera UICamera;
    
    void Awake(){

        //ItemWindow = GameObject.Find("StoreCanvas");
       // Animator   = ItemWindow.GetComponent<Animator>();
        inventory  = GameObject.FindObjectOfType<PlayerInventory>();
        //TextField  = GameObject.Find("TextField").GetComponent<Text>();
       // PriceField = GameObject.Find("Price").GetComponent<Text>();
        //ItemWindow.SetActive(false);
       
    }

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
                        if(Items[i] == LastItem){
                            TextField.text = Descripcion[i];
                            PriceField.text = Price[i].ToString(); 
                            indice = i; 
                        }     
                        
                        Current.Focus = true;
                        IsFocusing = true;
                        Current.hover = false;
                        audioSource.clip = OnClickSFX;
                        audioSource.Play();
                }    
            float RotationY = Input.GetAxis("Mouse Y") * RotSpeed * Mathf.Deg2Rad;
            LastItem.GetComponentInChildren<MeshRenderer>().transform.Rotate(Vector3.up , RotationY);
            }
        }
        else if(IsFocusing == false){
            Ray ray = UICamera.ScreenPointToRay(Input.mousePosition);
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
                    if(audioSource.clip != HoverSFX){
                        audioSource.clip = HoverSFX;
                        audioSource.Play();
                    }    
                }
            
            }else {
                    LastItem = null;
                    if(Current != null)
                        Current.hover = false; 
                    audioSource.clip = null;
            }
        }
      
    }
   
    public void OnBuyButton(){
       int CurrentMoney = inventory.Money;
       if(CurrentMoney >= int.Parse(PriceField.text)){
           CurrentMoney -= int.Parse(PriceField.text);
           GameObject Equiped = GameObject.Find("Equiped"+ Current.gameObject.name);
           if(!Current.Gun){
               PlayerController controller = GameObject.FindObjectOfType<PlayerController>();
               if(controller.CurrentItem == null)
                    controller.CurrentItem = Equiped.GetComponent<Item>();
               Equiped.GetComponent<Item>().Ammo ++;
               Equiped.GetComponent<Item>().Comprada = true;
           }
           else {
               Equiped.GetComponent<Gun>().Ammo += Current.AmmoCant;
               Equiped.GetComponent<Gun>().Comprada = true;
           }
       }
       inventory.Money = CurrentMoney;
       
    }
}
