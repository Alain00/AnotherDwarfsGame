using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public GameObject Bullet;
    public GameObject Player;
    public GameObject Weapon;
    public GameObject Flash;
    public float FireCadencia;
    float CoolDown;
    Vector3 MoveTo;
    Rigidbody rB;
    Quaternion LookDir;
    void Start()
    {
        CoolDown = FireCadencia;
        rB = GetComponent<Rigidbody>();
        LookDir = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo.x = Input.GetAxisRaw("Horizontal");
        MoveTo.z = Input.GetAxisRaw("Vertical");
        CoolDown -= Time.deltaTime;
        if(Input.GetButton("Fire1") && CoolDown <= 0){
            Shot();
        }  
            LookDir.x = 0;
            LookDir.z = 0;
            Player.transform.rotation =  LookDir;     
    }

    void FixedUpdate(){
        Vector3 PosToMove = rB.position +  MoveTo * movementSpeed * Time.fixedDeltaTime ;
        rB.MovePosition(PosToMove );
        if(MoveTo.x != 0 || MoveTo.z != 0)
        LookDir = Quaternion.LookRotation( (rB.position + MoveTo * movementSpeed) - rB.position );
    }

    void Shot(){

      
       Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
       float HitDist;
       Plane plane = new Plane(Vector3.up , Player.transform.position);
       /*
        if(Physics.Raycast(ray.origin , ray.direction , out hit , 1000))
            LookDir = Quaternion.LookRotation(hit.point - player.position);
       else 
            LookDir = Quaternion.LookRotation(ray.GetPoint(1000) - player.position);
*/
        if(plane.Raycast(ray , out HitDist )){
           Vector3 TargetPos = ray.GetPoint(HitDist);
           LookDir = Quaternion.LookRotation(TargetPos - Player.transform.position);
        }
        
       
           

        Player.transform.rotation = LookDir;
        CoolDown = FireCadencia;
        Quaternion BulletRot = Player.transform.rotation;

        GameObject CurrentBullet = Instantiate(Bullet , Weapon.transform.position , BulletRot);
        //Physics.IgnoreCollision(CurrentBullet.GetComponentInChildren<Collider>() , GetComponentInChildren<Collider>() , true) ;
    }
}
