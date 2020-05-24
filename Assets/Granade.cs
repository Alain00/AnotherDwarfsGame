using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Item
{

  public Rigidbody RB;  
  public float Ammo;
  public float Radius;
  public bool CanExplode;
  public float ExplodeTime;
  public GameObject Explosion;
  public float ShakeDuration;
  public float ShakeStrenght;
  void Start(){
      RB = GetComponent<Rigidbody>();
  }
  public override void ItemAction(Quaternion Dir){

      if(Ammo > 0){
        ThrowGranade(Dir);
      }
      else{
          Debug.Log("OUT OF AMMO");
      }
  }
  public void Update(){
      if(CanExplode){
          ExplodeTime-= Time.deltaTime;
          if(ExplodeTime <= 0){
              Explode();
          }
      }
  }
  void ThrowGranade(Quaternion Dir){
      
      GameObject Current =  Instantiate(gameObject , transform.position + transform.forward , Dir );
      Granade CurrentGranade =  Current.GetComponent<Granade>();
      Current.GetComponent<Collider>().enabled = true;
      CurrentGranade.RB.isKinematic = false;  
      CurrentGranade.RB.AddForce(Current.transform.forward * 5 + Current.transform.up * 3, ForceMode.Impulse);
      CurrentGranade.CanExplode = true;
      //RB.AddForce();
  }

  void Explode(){
      //Init new Granade
       GameObject CurExplosion = Instantiate(Explosion , transform.position + Vector3.up , Quaternion.identity);
       Transform player = GameObject.FindGameObjectWithTag("Player").transform;
       float Distance = Vector3.Distance(transform.position , player.position);
       if( Distance< 30){
            CameraShake shaker = CameraShake.instance;
             shaker.duration = ShakeDuration;
             shaker.power = ShakeStrenght - Distance/100;
             shaker.ShouldShake = true;
        }
      //Destroy nearby enemies
      Collider[] Enemies;
      Enemies =  Physics.OverlapSphere(transform.position , Radius );  
      for(int i = 0 ; i < Enemies.Length ; i++){
          Enemy CurEnemy =  Enemies[i].GetComponent<Enemy>();
          if(CurEnemy != null){
              CurEnemy.Health -= 3;
          }
      }
      //Destroy trash
      Destroy(CurExplosion, 5);
      Destroy(gameObject);
  }
    public void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position,Radius);
        Gizmos.color = Color.red;
    }

}
