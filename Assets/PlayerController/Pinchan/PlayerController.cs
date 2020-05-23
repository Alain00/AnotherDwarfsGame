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

    //ItemsStuff

    public List <Gun> Weapons = new List<Gun>();

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
    }

    
    void Update()
    {
        MoveTo.x = Input.GetAxisRaw("Horizontal");
        MoveTo.z = Input.GetAxisRaw("Vertical");
        bool firing = Input.GetButton("Fire1");
        time -= Time.deltaTime;
        

        if(Input.GetKeyDown(KeyCode.Q)){
            ChangeWeapon(1);
        }
        
        if (firing){
            CalculateFireRotation();
        }else{
            if(MoveTo.x != 0 || MoveTo.z != 0 )
                if(time <= 0){
                    LookDir = Quaternion.LookRotation( (rB.position + MoveTo * movementSpeed) - rB.position );
                }
        }
        
        CoolDown -= Time.deltaTime;
        if(firing && CoolDown <= 0){
            Shot();
        }  

        LookDir.x = 0;
        LookDir.z = 0;
        Player.transform.rotation = Quaternion.Lerp( Player.transform.rotation,LookDir , (7f * Time.deltaTime));     
    }

    void FixedUpdate(){
        Vector3 PosToMove = rB.position +  MoveTo * movementSpeed * Time.fixedDeltaTime ;
        rB.MovePosition(PosToMove );
    }

    void Shot(){
        time = 1f;
        // Player.transform.rotation = Quaternion.Lerp( Player.transform.rotation,LookDir , 7 * Time.deltaTime );
        CurrentGun.Shot(Player.transform , LookDir);
        CoolDown = CurrentGun.FireCadencia;
    }

    void CalculateFireRotation(){
        Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
        float HitDist;
        Plane plane = new Plane(Vector3.up , Player.transform.position);
        RaycastHit hit;
       
        if (Physics.Raycast(ray.origin , ray.direction , out hit , 1000 )){
            LookDir = Quaternion.LookRotation(hit.point - Player.transform.position);
        } 
    }

    void ChangeWeapon( int oper ){
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
}
