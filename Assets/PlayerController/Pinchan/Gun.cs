using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
     public GameObject Flash;
     public GameObject Bullet;
     public Transform ShotPoint;
     public float FireCadencia;
     public int Ammo;
     public int Charger;
     [SerializeField]
     int ChargerLeft;

    public int BulletsPerShot;
    public float TimeBThem;
    public float BulletRangeX;
    public float BulletRangeY;
    public float ShakeDuration;
    public float ShakeStrenght;
    public void Start(){
        ChargerLeft = Charger;
        Ammo -= Charger;
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            Reload();
        }
    }
    public void Shot(Transform Player , Quaternion LookDir){
       
        StartCoroutine( BulletInstansiate(Player,LookDir));
    }
    IEnumerator BulletInstansiate(Transform Player , Quaternion LookDir){
        int i = 0;
        while( i < BulletsPerShot ){
            if(ChargerLeft > 0){ 
                    CameraShake shaker = CameraShake.instance;
                    shaker.duration = ShakeDuration;
                    shaker.power = ShakeStrenght;
                    shaker.ShouldShake = true;
                    ChargerLeft--;   
                    LookDir = Quaternion.Euler(new Vector3(
                                                LookDir.eulerAngles.x + Random.Range(-BulletRangeX,BulletRangeX)
                                                ,LookDir.eulerAngles.y + Random.Range(-BulletRangeY,BulletRangeY)
                                                ,LookDir.z
                                                ));
                    GameObject CurrentBullet = Instantiate(Bullet , ShotPoint.position , LookDir);
                    GameObject CurrentVFX = Instantiate( Flash , ShotPoint.position , CurrentBullet.transform.rotation , Player.transform);
                    Destroy(CurrentVFX,5);
                    i++;
                    if(TimeBThem > 0)
                    yield return new WaitForSeconds(TimeBThem);
                }else  {
                    OutOfAmmo();
                    break;
                }
        }
    }

    void Reload(){
         Ammo -= Charger;
         ChargerLeft = Charger;
         if(Ammo < 0){
            ChargerLeft-= Ammo;
            Ammo = 0;
        }
    }

    void OutOfAmmo(){
        Debug.Log("Out of Ammo");
    }
}
