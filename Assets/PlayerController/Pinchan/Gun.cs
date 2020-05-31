using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Sprite sprite;
     public GameObject Flash;
     public GameObject Bullet;
     public Transform ShotPoint;
     public float FireCadencia;
     public int Ammo;
     public int Charger;
     [SerializeField]
    public int ChargerLeft;

    public int BulletsPerShot;
    public float TimeBThem;
    public float BulletRangeX;
    public float BulletRangeY;
    public float ShakeDuration;
    public float ShakeStrenght;
    public bool Comprada;
    public GameObject ammoItem;
    public AudioSource ReloadSound;
    

    public void Start(){
        DontDestroyOnLoad(this);
        /*ChargerLeft = Charger;
        Ammo -= Charger;*/
        Reload();
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.R)){
            Reload();
            ReloadSound.Play();
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
                                                LookDir.eulerAngles.x
                                                ,LookDir.eulerAngles.y + Random.Range(-BulletRangeY,BulletRangeY)
                                                ,LookDir.z
                                                ));
                    
                    GameObject CurrentBullet = Instantiate(Bullet , ShotPoint.position , LookDir);
                    GameObject CurrentVFX = Instantiate( Flash , ShotPoint.position , CurrentBullet.transform.rotation , Player.transform);
                    Destroy(CurrentVFX,3);
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
       
        int diff = Ammo - Charger;
        if (diff >= 0){
            int diffCharger = Charger - ChargerLeft;
            if (diffCharger == 0) return;
            Ammo -= diffCharger;
            ChargerLeft = Charger;
            /*if(Ammo < 0){
                ChargerLeft-= Ammo;
                Ammo = 0;
            }*/
        }else{
            Ammo = 0;
            ChargerLeft = Ammo;
        }
        Ammo = Mathf.Max(Ammo, 0);
        ChargerLeft = Mathf.Max(ChargerLeft, 0);
    }

    void OutOfAmmo(){
        Debug.Log("Out of Ammo");
    }
}
