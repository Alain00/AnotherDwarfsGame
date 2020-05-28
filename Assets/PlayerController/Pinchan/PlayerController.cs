using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text AmmoField;
    public Image WIconField;

    //ItemsStuff
    public List<Item> RightHand = new List<Item>();
    int itemIndice;
    Item CurrentItem;
    public Image IIconField;
    public LayerMask layerMask;
   
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
        anim.SetInteger("weapon" , 1);
        for(int i = 1 ; i < Weapons.Count ; i++){
            Weapons[i].GetComponentInChildren<MeshRenderer>().enabled = false;
        }


        RightHand.AddRange(GetComponentsInChildren<Item>());
        if(RightHand.Count > 0){
            CurrentItem = RightHand[0];
            IIconField.sprite = CurrentItem.sprite;
        }    
        //Ponerlo en una posicion pegada al suelo
        SetPosInWorld Pos = SetPosInWorld.instance;
        transform.position =  Pos.SetPos(transform.position);

        WIconField.sprite = CurrentGun.sprite;
        

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

        if(CurrentGun == Weapons[0])
            AmmoField.text = CurrentGun.ChargerLeft.ToString() + "/";
        else AmmoField.text = CurrentGun.ChargerLeft.ToString() + "/" + CurrentGun.Ammo.ToString();
    
    
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
       RaycastHit hit;
       
       if(Physics.Raycast(ray.origin , ray.direction , out hit , 1000, layerMask)){
            Vector3 hitPos = hit.point;
            hitPos.y += 2;
            LookDir = Quaternion.LookRotation( (hitPos  ) - CurrentGun.ShotPoint.position);    
       }    
        time = 1f;
        Quaternion toRotation = Quaternion.Lerp( Player.transform.rotation,LookDir , 7 * Time.deltaTime );
        toRotation.x = 0;
        toRotation.z = 0;
        Player.transform.rotation = toRotation;
    }

    void Shot(){
        CurrentGun.Shot(Player.transform , LookDir);
        CoolDown = CurrentGun.FireCadencia;
    }

    void ItemAction(){
        CurrentItem.ItemAction(LookDir);
    }

    void ChangeWeapon(){
       Weapons[indice].GetComponentInChildren<MeshRenderer>().enabled = false;
            
        do{
            indice++;
            if(indice == Weapons.Count)
                indice = 0;
            if(indice < 0)
             indice = Weapons.Count - 1;
        }
        while(Weapons[indice].Comprada == false);    
        CurrentGun = Weapons[indice];            
        Weapons[indice].GetComponentInChildren<MeshRenderer>().enabled = true;
        anim.SetInteger("weapon", indice + 1);
        WIconField.sprite = CurrentGun.sprite;
    }
    public void AddWeapon(Gun ToAdd){
            Weapons.Add(ToAdd);
    }

   void ChangeItemRightHand(){
        RightHand[itemIndice].gameObject.SetActive(false);
        do{
            itemIndice++;
            if(itemIndice == RightHand.Count)
                itemIndice = 0;
            if(itemIndice < 0)
             itemIndice = RightHand.Count - 1;
        }
        while(RightHand[itemIndice].Comprada == false);    
        CurrentItem = RightHand[itemIndice];            
        RightHand[itemIndice].gameObject.SetActive(true);
        IIconField.sprite = CurrentItem.sprite;
    }


    public void AddItemRightHand (Item ToAdd){
        RightHand.Add(ToAdd);
    }
}
