using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Item
{

  Rigidbody RB;  
  public float Radius;
  public bool CanExplode;
  public float ExplodeTime;
  public GameObject Explosion;
  public float ShakeDuration;
  public float ShakeStrenght;
  public float damage = 20;
  public GameObject GranadeToInstantiate;
  public Transform Player;
  void Start(){
     
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
      //Init new Granade
      GameObject Current =  Instantiate(GranadeToInstantiate , transform.position + Player.forward , Dir );
      RB = Current.GetComponent<Rigidbody>();
      Granade CurrentGranade =  Current.GetComponent<Granade>();
      RB.AddForce(Current.transform.forward * 6 + Current.transform.up * 3 , ForceMode.Impulse);
      CurrentGranade.CanExplode = true;
  }

  void Explode(){
      
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
          Destructible destructible =  Enemies[i].GetComponent<Destructible>();
          if(destructible != null){
              destructible.OnDamage(damage);
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
