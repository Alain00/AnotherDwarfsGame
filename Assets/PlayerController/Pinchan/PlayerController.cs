using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
       
    public GameObject Player;
    
    
    //Movement
    Vector3 MoveTo;
    Rigidbody rB;
    public float movementSpeed;
    //RotationStuff
    Quaternion LookDir;
    float time;
    //WeaponsStuff
    int indice;
    Gun CurrentGun;
    float CoolDown;
    public List <Gun> Weapons = new List<Gun>();

    //ItemsStuff
    public List<Item> RightHand = new List<Item>();
    int itemIndice;
    Item CurrentItem;
   
    void Start()
    {
        CoolDown = 0;
        rB = GetComponent<Rigidbody>();
        LookDir = new Quaternion();
        time = 1;
        //Coger todas las armas
        Weapons.AddRange(GetComponentsInChildren<Gun>());
        for(int i = 0 ; i < Weapons.Count ; i++){
            if(!Weapons[i].gameObject.activeSelf){
                Weapons.Remove(Weapons[i]);
                i--;
            }    
        }
        CurrentGun = Weapons[0];
        
        RightHand.AddRange(GetComponentsInChildren<Item>());
        CurrentItem = RightHand[0];
    }

    
    void Update()
    {
        MoveTo.x = Input.GetAxisRaw("Horizontal");
        MoveTo.z = Input.GetAxisRaw("Vertical");
        time -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Q)){
            ChangeWeapon(1);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            ChangeItemRightHand(1);
        }
        
        CoolDown -= Time.deltaTime;
        if(Input.GetButton("Fire1") && CoolDown <= 0){
            OnClickLookDir();
            Shot();
        }
        else if (Input.GetButtonDown("Fire2")){
            OnClickLookDir();
            ItemAction();
        }  
            LookDir.x = 0;
            LookDir.z = 0;
            Player.transform.rotation = Quaternion.Lerp( Player.transform.rotation,LookDir , (7f * Time.deltaTime));     
    }

    void FixedUpdate(){
        Vector3 PosToMove = rB.position +  MoveTo * movementSpeed * Time.fixedDeltaTime ;
        rB.MovePosition(PosToMove );
        if(MoveTo.x != 0 || MoveTo.z != 0 )
         if(time <= 0)
        LookDir = Quaternion.LookRotation( (rB.position + MoveTo * movementSpeed) - rB.position );
    }

    void OnClickLookDir(){
       Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
       Plane plane = new Plane(Vector3.up , Player.transform.position);
       RaycastHit hit;
       
        if(Physics.Raycast(ray.origin , ray.direction , out hit , 1000 ))
            LookDir = Quaternion.LookRotation(hit.point - Player.transform.position);        
        time = 1f;
        Player.transform.rotation = Quaternion.Lerp( Player.transform.rotation,LookDir , 7 * Time.deltaTime );
    }

    void Shot(){
        CurrentGun.Shot(Player.transform , LookDir);
        CoolDown = CurrentGun.FireCadencia;
    }

    void ItemAction(){
        CurrentItem.ItemAction(LookDir);
    }

    void ChangeWeapon(int oper){
        Weapons[indice].gameObject.SetActive(false);
        if(oper == 1)
            indice++;
        else indice--;
        if(indice == Weapons.Count)
            indice = 0;
        if(indice < 0)
            indice = Weapons.Count - 1;

        CurrentGun = Weapons[indice];            
        Weapons[indice].gameObject.SetActive(true);
    }
    public void AddWeapon(Gun ToAdd){
            Weapons.Add(ToAdd);
    }

   void ChangeItemRightHand(int oper){
        RightHand[itemIndice].gameObject.SetActive(false);
        if(oper == 1)
            itemIndice++;
        else itemIndice--;
        if(itemIndice == RightHand.Count)
            itemIndice = 0;
        if(itemIndice < 0)
            itemIndice = RightHand.Count - 1;

        CurrentItem = RightHand[itemIndice];            
        RightHand[itemIndice].gameObject.SetActive(true);
    }


    public void AddItemRightHand (Item ToAdd){
        RightHand.Add(ToAdd);
    }
}
