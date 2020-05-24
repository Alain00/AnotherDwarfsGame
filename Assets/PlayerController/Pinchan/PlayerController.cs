using System;
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
    Animator anim;
    public float TimeMulti;

    private float speed;
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
        anim = GetComponentInChildren<Animator>();
        speed =  anim.GetFloat("speed");
        CoolDown = 0;
        rB = GetComponent<Rigidbody>();
        LookDir = new Quaternion();
        time = 1;
        //Coger todas las armas
        Weapons.AddRange(GetComponentsInChildren<Gun>());
        CurrentGun = Weapons[0];
        anim.SetInteger("Weapon" , 1);
        
        RightHand.AddRange(GetComponentsInChildren<Item>());
        if(RightHand.Count > 0)
            CurrentItem = RightHand[0];
    }

    
    void Update()
    {
        MoveTo.x = Input.GetAxis("Horizontal");
        MoveTo.z = Input.GetAxis("Vertical");
        time -= Time.deltaTime;
        

        if(Input.GetKeyDown(KeyCode.Q)){
            ChangeWeapon();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            ChangeItemRightHand();
        }

        if (MoveTo.x != 0 || MoveTo.z != 0)
        {
            if (time <= 0)
            {
                LookDir = Quaternion.LookRotation((rB.position + MoveTo * movementSpeed) - rB.position);
            }

            speed += Time.deltaTime * TimeMulti;

        }
        else speed -= Time.deltaTime * 2 * TimeMulti;

       
        
        CoolDown -= Time.deltaTime;
        if(Input.GetButton("Fire1") ){
            OnClickLookDir();
            if (CoolDown <= 0)
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

    private void LateUpdate()
    {
        speed = Mathf.Clamp(speed, 0, 1);
        anim.SetFloat("speed",speed);
    }

    void FixedUpdate(){
        Vector3 PosToMove = rB.position +  MoveTo * movementSpeed * Time.fixedDeltaTime ;
        rB.MovePosition(PosToMove );
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

    void ChangeWeapon(){
        Weapons[indice].gameObject.SetActive(false);
            
        do{
            indice++;
            if(indice == Weapons.Count)
                indice = 0;
            if(indice < 0)
             indice = Weapons.Count - 1;
        }
        while(Weapons[indice].Comprada == false);    
        CurrentGun = Weapons[indice];            
        Weapons[indice].gameObject.SetActive(true);
        anim.SetInteger("Weapon", indice + 1);
    }
    public void AddWeapon(Gun ToAdd){
            Weapons.Add(ToAdd);
    }

   void ChangeItemRightHand(){
        RightHand[itemIndice].gameObject.SetActive(false);
        do{
            indice++;
            if(indice == RightHand.Count)
                indice = 0;
            if(indice < 0)
             indice = RightHand.Count - 1;
        }
        while(RightHand[indice].Comprada == false);    
        CurrentItem = RightHand[itemIndice];            
        RightHand[itemIndice].gameObject.SetActive(true);
    }


    public void AddItemRightHand (Item ToAdd){
        RightHand.Add(ToAdd);
    }
}
