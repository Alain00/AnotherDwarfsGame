using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{
    Transform bullet;
    Rigidbody Rb;
    public float speed;
    public float AutoDestruct =5;
    public GameObject ImpactVFX;

    void Start(){
        bullet = GetComponentInChildren<Transform>();
    }
    void Update()
    {

        AutoDestruct-= Time.deltaTime;
        if(AutoDestruct<= 0){
            Destroy(gameObject);
        }
        transform.position +=bullet.forward * speed * Time.deltaTime;
    }
    void OnTriggerEnter (Collider col) {
        if(col.gameObject.tag != "Player"){
            if(col.gameObject.tag == "Enemy"){
                col.gameObject.GetComponent<Enemy>().Health--;
            }
            Instantiate(ImpactVFX , transform.position , Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
